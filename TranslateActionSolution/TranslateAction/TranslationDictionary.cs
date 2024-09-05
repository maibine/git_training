using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TranslateAction
{
	internal class TranslationDictionary
	{
		public Dictionary<string, (string ro, string en)> Translations { get; private set; } = new Dictionary<string, (string ro, string en)>();
		
		public void LoadFromCsv()
		{
			Translations.Clear();
			//using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			// Specify the namespace and the resource file name
			string resourceName = "TranslateAction.Resources.Dict-ro-it-en_20240723.csv";

			// Get the current assembly
			Assembly assembly = Assembly.GetExecutingAssembly();

			// Load the embedded file into a stream
			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				if (stream != null)
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						bool isFirstLine = true;
						StringBuilder currentLine = new StringBuilder();

						while (!reader.EndOfStream)
						{
							string line = reader.ReadLine();

							// Skip the header line
							if (isFirstLine)
							{
								isFirstLine = false;
								continue;
							}

							// Append current line to handle multi-line fields
							if (currentLine.Length > 0)
							{
								currentLine.Append("\n");
							}
							currentLine.Append(line);

							// Check if line ends a complete CSV row (balanced quotes)
							if (IsCompleteCsvLine(currentLine.ToString()))
							{
								// Parse the complete CSV line
								var fields = ParseCsvLine(currentLine.ToString());

								// Validate that there are exactly 3 fields
								if (fields.Count == 3)
								{
									string ro = fields[0].Trim(); // Romanian translation
									string it = fields[1].Trim(); // Italian translation (used as the key)
									string en = fields[2].Trim(); // English translation

									if (!string.IsNullOrWhiteSpace(it))
									{
										// Add to dictionary with Italian as the key
										it = it.ToLowerInvariant();
										Translations[it] = (ro, en);
									}
								}
								else
								{
									Console.WriteLine($"Warning: Invalid line with {fields.Count} fields. Line: {currentLine}");
								}

								// Clear current line for next record
								currentLine.Clear();
							}
						}
					}

					//// Print the dictionary for verification
					//foreach (var item in Translations)
					//{
					//	Console.WriteLine($"Key (IT): {item.Key}, RO: {item.Value.ro}, EN: {item.Value.en}");
					//}
				}
				else
				{
					Console.WriteLine("Error: Embedded resource not found.");
				}
			}
		}

		// Simple CSV line parser to handle quoted commas and multi-line fields
		private static List<string> ParseCsvLine(string line)
		{
			var result = new List<string>();
			bool inQuotes = false;
			var currentField = new StringBuilder();

			for (int i = 0; i < line.Length; i++)
			{
				char c = line[i];

				if (c == '\"')
				{
					inQuotes = !inQuotes; // Toggle inQuotes flag
				}
				else if (c == ',' && !inQuotes)
				{
					// End of field
					result.Add(currentField.ToString().Trim());
					currentField.Clear();
				}
				else
				{
					currentField.Append(c);
				}
			}

			// Add the last field
			result.Add(currentField.ToString().Trim());

			return result;
		}

		// Helper function to check if a CSV line is complete (all quotes are balanced)
		private static bool IsCompleteCsvLine(string line)
		{
			int quoteCount = 0;
			foreach (char c in line)
			{
				if (c == '\"')
					quoteCount++;
			}

			// Line is complete if the number of quotes is even
			return quoteCount % 2 == 0;
		}
		
		public string Translate(string text, string targetLanguage)
		{
			text = text.Trim().ToLowerInvariant();
			if (Translations.TryGetValue(text, out var translation))
			{
				return targetLanguage.ToLower() switch
				{
					"ro-ro" => translation.ro,
					"ro" => translation.ro,
					"en-us" => translation.en,
					"en" => translation.en,
					_ => text,
				};
			}
			return "";
		}

		public string ReplaceTranslations(string text, string targetLanguage)
		{
			// Regex to find emphasized text with Italian keys
			var regexBold = new Regex(@"\*\*(.*?)\*\*\s*\((.*?)\)");
			regexBold = new Regex(@"\*\*(.*?)\*\*");
			var regexItalic = new Regex(@"(?<!\*)\*(?=[^\*]*\([^\)]*\))[^\*]+\*(?!\*)");



			var matches = regexBold.Matches(text);
			var matchesItalic = regexItalic.Matches(text);

			IEnumerable<string> combined = matches.Cast<Match>().Concat(matchesItalic.Cast<Match>()).Where(x => x.Success).Select(x => x.Value);

			foreach (string fullMatch in combined)
			{
				var contentWithKey = fullMatch.Trim('*');
				string boldOrItalic = fullMatch.StartsWith("**") ? "**" : "*";

				// Extract the translation part and the Italian key part
				var parts = contentWithKey.Split(new[] { " (" }, StringSplitOptions.None);
				if (parts.Length == 2)
				{
					var oldTranslation = parts[0].Trim();
					var italianKey = parts[1].Trim(' ', ')');

					// Check if there is a translation for the Italian key in the target language
					var newTranslation = Translate(italianKey, targetLanguage);

					if (!string.IsNullOrEmpty(newTranslation) /*&& oldTranslation != newTranslation*/)
					{
						// Replace the old translation with the new one and remove the Italian key
						text = text.Replace(fullMatch, $"{boldOrItalic}{newTranslation}{boldOrItalic}");

						// Replace any other occurrences of the old translation in the text
						//text = text.Replace(oldTranslation, newTranslation);
					}
				}
			}

			// Replace title translation if exists
			var titleRegex = new Regex(@"title: (.*?) \((.*?)\)");
			var titleMatch = titleRegex.Match(text);

			if (titleMatch.Success)
			{
				var oldTitleTranslation = titleMatch.Groups[1].Value.Trim();
				var italianTitleKey = titleMatch.Groups[2].Value.Trim();

				var newTitleTranslation = Translate(italianTitleKey, targetLanguage);

				if (!string.IsNullOrEmpty(newTitleTranslation) && oldTitleTranslation != newTitleTranslation)
				{
					text = text.Replace(titleMatch.Value, $"title: {newTitleTranslation}");
					text = text.Replace(oldTitleTranslation, newTitleTranslation);
				}
			}

			return text;
		}
	}
}
