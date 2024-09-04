using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslateAction
{
	internal partial class TranslateService
	{
		public static async Task<string> TranslateMarkdownAsync(string text, string targetLanguage, string apiKey)
		{
			if (!sampleItTranslations.ContainsKey(targetLanguage))
			{
				var message = $"No {targetLanguage} sample translation found in sampleItTranslations!";
				Console.WriteLine(message);
				throw new InvalidOperationException(message);
			}

			string assistantSampleResponse = sampleItTranslations[targetLanguage];

			string systemPrompt = $"You are a helpful assistant " +
						$"that translates to {targetLanguage}." +
						"Your task is to translate a Markdown file, while preserving the original formatting, including " +
						"inline elements like links and images. " +
						"Make sure to ignore HTML tags and code blocks, but translate code comments. " +
						"Be cautious when translating Markdown links, Markdown images, and Markdown headings. " +
						"For italic(*italicized text*) and bold(**bold text**) text you HAVE to let the original text in parenthesis beside the translated text. " +
						"For ex: '**original text**'  becomes '**translated text (original text)**'. " +
						"Also for 'title: original text' you should do similar. It becomes: 'title:translated text (original text)'";

			// Create the JSON request body using JObject
			var requestBody = new JObject
			{
				["model"] = "gpt-4o-mini",
				["messages"] = new JArray
			  {
					new JObject
					{
						 ["role"] = "system",
						 ["content"] = systemPrompt
					},
					new JObject
					{
						 ["role"] = "user",
						 ["content"] = sampleIt
					},
					new JObject
					{
						 ["role"] = "assistant",
						 ["content"] = assistantSampleResponse
					},
					new JObject
					{
						 ["role"] = "user",
						 ["content"] = text
					},
			  }
			};

			string requestBodyJson = requestBody.ToString();
			Console.WriteLine($"Request Body JSON: {requestBodyJson}");

			var content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");


			try
			{
				var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
				var responseString = await response.Content.ReadAsStringAsync();
				var responseObject = JObject.Parse(responseString);

				// Add detailed logging
				Console.WriteLine($"Response JSON: {responseString}");

				var messageContent = responseObject["choices"]?[0]?["message"]?["content"]?.ToString().Trim();
				if (messageContent == null)
				{
					throw new Exception("Failed to extract 'content' from OpenAI response.");
				}

				return messageContent;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error during translation: {ex.Message}");
				Console.WriteLine(ex.StackTrace);
				throw;
			}

		
			//Random random = new Random();
			//var mseconds = random.Next(1, 7) * 1000;
			//await Task.Delay( mseconds );
			//return text;
		}

		public static async Task<string> TranslateJsonAsync(string targetLanguage,
			string text, string apiKey, string source_language = "italian")
		{
			string systemPrompt = $"You are a helpful assistant " +
					$"that translates {source_language} to {targetLanguage}." +
					"Your task is to process a json file and translate only the \"label\" key value.";
			string userPrompt = $"Instructions: Translate the following {source_language} " +
					$"json to {targetLanguage} " +
					$"while maintaining the original formatting: \"{text}\".\r\nformat: Return only the translated content, not including the original text.";

			// Create the JSON request body using JObject
			var requestBody = new JObject
			{
				["model"] = "gpt-4o-mini",
				["messages"] = new JArray
			  {
					new JObject
					{
						 ["role"] = "system",
						 ["content"] = systemPrompt
					},
					new JObject
					{
						 ["role"] = "user",
						 ["content"] = userPrompt
					},
			  }
			};

			string requestBodyJson = requestBody.ToString();
			Console.WriteLine($"Request Body JSON: {requestBodyJson}");

			var content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");


			try
			{
				var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
				var responseString = await response.Content.ReadAsStringAsync();
				var responseObject = JObject.Parse(responseString);

				// Add detailed logging
				Console.WriteLine($"Response JSON: {responseString}");

				var messageContent = responseObject["choices"]?[0]?["message"]?["content"]?.ToString().Trim();
				if (messageContent == null)
				{
					throw new Exception("Failed to extract 'content' from OpenAI response.");
				}

				return messageContent;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error during translation: {ex.Message}");
				Console.WriteLine(ex.StackTrace);
				throw;
			}

		}

	}
}
