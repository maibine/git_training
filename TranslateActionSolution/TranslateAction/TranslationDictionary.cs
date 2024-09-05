using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TranslateAction
{
	internal class TranslationDictionary
	{
		public Dictionary<string, (string ro, string en)> Translations { get; private set; } = new Dictionary<string, (string ro, string en)>();

		public void LoadFromExcel(int skipRows = 0)
		{
			string resourceName = "TranslateAction.Resources.Dict-ro-it-en_20240723.xlsx";

			// Get the current assembly
			Assembly assembly = Assembly.GetExecutingAssembly();

			// Load the embedded file into a stream
			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (var workbook = new XLWorkbook(stream))
			{
				var worksheet = workbook.Worksheet(1); // Assuming data is in the first worksheet
				foreach (var row in worksheet.RangeUsed().RowsUsed().Skip(skipRows))
				{
					var it = row.Cell(2).GetString().Trim().ToLowerInvariant();
					var ro = row.Cell(1).GetString().Trim();
					var en = row.Cell(3).GetString().Trim();

					if (!Translations.ContainsKey(it))
					{
						Translations.Add(it, (ro, en));
					}
				}
			}
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
