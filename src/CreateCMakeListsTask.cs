using System;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.Text;
using System.IO;

namespace GeNet.Sdk.NativeShim;

public class CreateCMakeListsTask : Task
{
    [Required]
    public string? CMakeListsFilePath { get; set; }

    [Required]
    public string? ProjectName { get; set; }

    [Required]
    public string? ProjectRoot { get; set; }

    public string? ProjectVersion { get; set; }

    public ITaskItem[]? Packages { get; set; }

    [Required]
    public ITaskItem[]? CompileItems { get; set; }

    public override bool Execute()
    {
        if (string.IsNullOrWhiteSpace(CMakeListsFilePath))
        {
            LogError("NSSDK0001: The {0} parameter must be specified for the {1} task.",
                nameof(CMakeListsFilePath),
                nameof(CreateCMakeListsTask));

            return false;
        }

        if (string.IsNullOrWhiteSpace(ProjectName))
        {
            LogError("NSSDK0001: The {0} parameter must be specified for the {1} task.",
                nameof(ProjectName),
                nameof(CreateCMakeListsTask));

            return false;
        }

        if (string.IsNullOrWhiteSpace(ProjectRoot))
        {
            LogError("NSSDK0001: The {0} parameter must be specified for the {1} task.",
                nameof(ProjectRoot),
                nameof(CreateCMakeListsTask));

            return false;
        }

        ProjectVersion ??= "1.0.0";
        Packages ??= Array.Empty<ITaskItem>();
        CompileItems ??= Array.Empty<ITaskItem>();

        StringBuilder sb = new();

        sb.AppendLine("cmake_minimum_required(VERSION 3.15)");
        sb.AppendLine();
        sb.AppendLine($"project({ProjectName} VERSION {ProjectVersion})");
        sb.AppendLine();

        // Find packages
        foreach (var package in Packages)
        {
            sb.AppendLine($"find_package({package.ItemSpec} CONFIG REQUIRED)");
        }
        sb.AppendLine();

        // CompileItems
        sb.AppendLine("add_library(");
        sb.AppendLine($"    {ProjectName}");
        sb.AppendLine("    SHARED");
        foreach (var compileItem in CompileItems)
        {
            sb.Append("        ");
            sb.AppendLine(Path.Combine(ProjectRoot!, compileItem.ItemSpec));
        }
        sb.AppendLine(")");
        sb.AppendLine();

        // Target link libraries
        sb.AppendLine("target_link_libraries(");
        sb.AppendLine($"    {ProjectName}");
        sb.AppendLine("    PRIVATE");
        foreach (var package in Packages)
        {
            sb.Append("        ");
            sb.AppendLine($"{package.ItemSpec}::{package.ItemSpec}");
        }
        sb.AppendLine(")");
        sb.AppendLine();

        // Install
        sb.AppendLine("install(");
        sb.AppendLine($"    TARGETS {ProjectName}");
        sb.AppendLine("    ARCHIVE DESTINATION ${CMAKE_INSTALL_PREFIX}");
        sb.AppendLine(")");
        sb.AppendLine();

        File.WriteAllText(CMakeListsFilePath!, sb.ToString());

        return true;
    }

    private void LogError(string message, params object[] messageArgs)
    {
        string code = Log.ExtractMessageCode(message, out _);
        Log.LogError(subcategory: null, errorCode: code, helpKeyword: null, file: null, lineNumber: 0, columnNumber: 0, endLineNumber: 0, endColumnNumber: 0, helpLink: null, message: message, messageArgs: messageArgs);
    }
}
