using System.Net.Sockets;
using System;


if (args.Length < 4)
{
    Console.WriteLine("Missing required arguments. Expected: <input_dir> <repo_dir> <lang> <openai_api_key>");
    return;
}

string inputDir = args[0];
string repoDir = args[1];
string language = args[2];
string openaiApiKey = args[3];

if (string.IsNullOrEmpty(inputDir))
{
    Console.WriteLine("Input directory argument is missing.");
    return;
}

if (!Directory.Exists(inputDir))
{
    Console.WriteLine($"Input directory does not exist: {inputDir}");
    return;
}

if (!Directory.Exists(repoDir))
{
    Console.WriteLine($"Repository directory does not exist: {repoDir}");
    return;
}

if (string.IsNullOrEmpty(language))
{
    Console.WriteLine("Language environment variable is missing.");
    return;
}

if (string.IsNullOrEmpty(openaiApiKey))
{
    Console.WriteLine("OpenAI API key is missing.");
    return;
}

// Configure Git to treat the repository directory as safe
ConfigureGitSafeDirectory(repoDir);

// Log environment variables and paths
Console.WriteLine($"Language: {language}");
Console.WriteLine($"OpenAI API Key: {(string.IsNullOrEmpty(openaiApiKey) ? "Missing" : "Present")}");
Console.WriteLine($"Input Directory: {inputDir}");
Console.WriteLine($"Repository Directory: {repoDir}");
Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");

// Print the Git log for debugging
PrintGitLog(repoDir);

// List the contents of the directories for debugging
Console.WriteLine("Contents of input directory:");
foreach (var file in Directory.GetFiles(inputDir))
{
    Console.WriteLine(file);
}

Console.WriteLine("Contents of repository directory:");
foreach (var file in Directory.GetFiles(repoDir))
{
    Console.WriteLine(file);
}
//// Get files changed in the last commit
//var changedFiles = GetChangedFiles(repoDir).Where(f => f.StartsWith(inputDir)).ToList();

try
{
	 var newFiles = GetNewFiles(repoDir).Where(f => f.StartsWith(inputDir.Replace(Path.DirectorySeparatorChar, '/'))).ToList();

	foreach (var file in newFiles)
	{
		string normalizedFile = Path.Combine(repoDir, file.Replace('/', Path.DirectorySeparatorChar));
		string content = await File.ReadAllTextAsync(normalizedFile);
		string translatedContent = await TranslateText(content, language, openaiApiKey);
		string outputDir = GetOutputDir(repoDir, normalizedFile, language);
		Directory.CreateDirectory(outputDir);
		string outputFile = Path.Combine(outputDir, Path.GetFileName(normalizedFile));
		await File.WriteAllTextAsync(outputFile, translatedContent);
		Console.WriteLine($"Wrote file {outputFile} in {language} language;");
	}
}
catch (Exception ex)
{
	Console.WriteLine($"Error during processing: {ex.Message}");
	Console.WriteLine(ex.StackTrace);
}

void PrintGitLog(string repoDir)
{
    using var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            WorkingDirectory = repoDir,
            FileName = "git",
            Arguments = "log --oneline -5",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        }
    };
    process.Start();
    while (!process.StandardOutput.EndOfStream)
    {
        var line = process.StandardOutput.ReadLine();
        Console.WriteLine(line);
    }
    process.WaitForExit();
}

async Task<string> TranslateText(string text, string targetLanguage, string apiKey)
{
	string prompt = $"Translate the following text to {targetLanguage}:\n\n{text}";

	// Create the JSON request body using JObject
	var requestBody = new JObject
	{
		["model"] = "gpt-3.5-turbo",
		["messages"] = new JArray
		  {
				new JObject
				{
					 ["role"] = "system",
					 ["content"] = "You are a helpful assistant."
				},
				new JObject
				{
					 ["role"] = "user",
					 ["content"] = prompt
				}
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

List<string> GetChangedFiles(string repoDir)
{
	var result = new List<string>();
	using (var process = new System.Diagnostics.Process())
	{
		process.StartInfo.WorkingDirectory = repoDir;
		process.StartInfo.FileName = "git";
		process.StartInfo.Arguments = "diff --name-only HEAD~1 HEAD";
		process.StartInfo.RedirectStandardOutput = true;
		process.Start();
		while (!process.StandardOutput.EndOfStream)
		{
			var line = process.StandardOutput.ReadLine();
			if (!string.IsNullOrWhiteSpace(line))
			{
				result.Add(Path.Combine(repoDir, line.Replace('/', Path.DirectorySeparatorChar)));
			}
		}
		process.WaitForExit();
	}
	return result;
}

List<string> GetNewFiles(string repoDir)
{
	var result = new List<string>();
	using var process = new Process
	{
		StartInfo = new ProcessStartInfo
		{
			WorkingDirectory = repoDir,
			FileName = "git",
			Arguments = "diff --name-status HEAD~1 HEAD",
			RedirectStandardOutput = true
		}
	};
	process.Start();
	while (!process.StandardOutput.EndOfStream)
	{
		var line = process.StandardOutput.ReadLine();
		if (!string.IsNullOrWhiteSpace(line) && line.StartsWith("A\t"))
		{
			var filePath = line.Substring(2).Trim();
			result.Add(Path.Combine(repoDir, filePath.Replace('/', Path.DirectorySeparatorChar)));
		}
	}
	process.WaitForExit();

	if (result.Count == 0)
    {
        Console.WriteLine("No new files found in the last commit.");
    }

	return result;
}

string GetOutputDir(string repoDir, string filePath, string language)
{
	var relativePath = Path.GetRelativePath(repoDir, filePath);
	var parts = relativePath.Split(Path.DirectorySeparatorChar).Skip(1).ToArray();
	parts = new[] { "i18n", language, "docusaurus-plugin-content-docs", "current" }.Concat(parts).ToArray();
	return Path.Combine(repoDir, Path.Combine(parts.Take(parts.Length - 1).ToArray()));
}

void ConfigureGitSafeDirectory(string repoDir)
{
    using var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            WorkingDirectory = repoDir,
            FileName = "git",
            Arguments = "config --global --add safe.directory /github/workspace",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        }
    };
    process.Start();
    while (!process.StandardOutput.EndOfStream)
    {
        var line = process.StandardOutput.ReadLine();
        Console.WriteLine(line);
    }
    process.WaitForExit();
}

//docker run --rm -e OPENAI_API_KEY= -v "D:\Work\Documentation\FluentisDoc-GH:/app/FluentisDoc-GH" translate-action "/app/FluentisDoc-GH/docs/crm/chance" "/app/FluentisDoc-GH" "en,ro,hr,pt"
//docker run --rm -e OPENAI_API_KEY= -v "D:\Work\Documentation\FluentisDoc-GH:/app/FluentisDoc-GH" translate-action "/app/FluentisDoc-GH/docs/crm/chance" "/app/FluentisDoc-GH" "en-US,ro-RO"
//docker run --rm -e OPENAI_API_KEY= -e lang="en-US" -v "D:\Work\Documentation\FluentisDoc-GH:/app/FluentisDoc-GH" translate-action "/app/FluentisDoc-GH/docs/crm/chance" "/app/FluentisDoc-GH"


