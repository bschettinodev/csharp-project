using System.Diagnostics;

var command = args.FirstOrDefault();
var subCommand = args.Skip(1).FirstOrDefault();

var repositoryRoot = FindRepositoryRoot();

var apiProjectPath = Path.Combine(repositoryRoot, "apps", "backend", "src", "api");
var cliProjectPath = Path.Combine(repositoryRoot, "tools", "FinanceTracker.Cli");
var solutionPath = Path.Combine(repositoryRoot, "FinanceTracker.sln");

if (command is null)
{
    PrintHelp();
    return;
}

switch (command.ToLower())
{
    case "run":
        RunCommand("dotnet", "run", apiProjectPath);
        break;

    case "migrate":
        RunCommand("dotnet", "ef database update", apiProjectPath);
        break;

    case "reset":
        RunCommand("dotnet", "ef database drop -f", apiProjectPath);
        RunCommand("dotnet", "ef database update", apiProjectPath);
        break;

    case "dev":
        RunCommand("dotnet", "ef database drop -f", apiProjectPath);
        RunCommand("dotnet", "ef database update", apiProjectPath);
        RunCommand("dotnet", "run", apiProjectPath);
        break;

    case "build":
        HandleBuild(subCommand, solutionPath, apiProjectPath, cliProjectPath);
        break;

    default:
        PrintHelp();
        break;
}

static void HandleBuild(string? subCommand, string solutionPath, string apiPath, string cliPath)
{
    switch (subCommand?.ToLower())
    {
        case null:
        case "all":
            RunCommand("dotnet", $"build \"{solutionPath}\"", Directory.GetCurrentDirectory());
            break;

        case "api":
            RunCommand("dotnet", "build", apiPath);
            break;

        case "cli":
            RunCommand("dotnet", "build", cliPath);
            break;

        default:
            Console.WriteLine("Invalid build target.");
            Console.WriteLine("Use: dotnet finance build [all|api|cli]");
            break;
    }
}

static string FindRepositoryRoot()
{
    var currentDirectory = Directory.GetCurrentDirectory();

    while (currentDirectory is not null)
    {
        if (Directory.Exists(Path.Combine(currentDirectory, ".git")))
        {
            return currentDirectory;
        }

        currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
    }

    throw new InvalidOperationException("Repository root not found.");
}

static void RunCommand(string fileName, string arguments, string workingDirectory)
{
    Console.WriteLine();
    Console.WriteLine($"Running: {fileName} {arguments}");
    Console.WriteLine($"Working directory: {workingDirectory}");
    Console.WriteLine();

    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            UseShellExecute = false,
        },
    };

    process.Start();
    process.WaitForExit();

    if (process.ExitCode != 0)
    {
        Environment.Exit(process.ExitCode);
    }
}

static void PrintHelp()
{
    Console.WriteLine("Finance Tracker CLI");
    Console.WriteLine();
    Console.WriteLine("Usage:");
    Console.WriteLine("  dotnet finance run");
    Console.WriteLine("  dotnet finance migrate");
    Console.WriteLine("  dotnet finance reset");
    Console.WriteLine("  dotnet finance dev");
    Console.WriteLine();
    Console.WriteLine("Build:");
    Console.WriteLine("  dotnet finance build");
    Console.WriteLine("  dotnet finance build all");
    Console.WriteLine("  dotnet finance build api");
    Console.WriteLine("  dotnet finance build cli");
}
