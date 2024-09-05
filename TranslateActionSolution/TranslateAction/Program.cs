using System.Net.Sockets;
using System;
using TranslateAction;


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

var dictionary = new TranslationDictionary();
dictionary.LoadFromCsv();
Console.WriteLine($"Dictionary loaded {dictionary.Translations.Count} rows.");

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

		string translatedContent = normalizedFile.EndsWith(".json") ? await TranslateService.TranslateJsonAsync(language, content, openaiApiKey)
			: await TranslateService.TranslateMarkdownAsync(content, language, openaiApiKey);

		if (dictionary != null && !file.EndsWith(".json"))
		{
			translatedContent = dictionary.ReplaceTranslations(translatedContent, language);
		}

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


