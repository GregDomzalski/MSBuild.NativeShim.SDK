using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace GeNet.Sdk.NativeShim;

public class CreateVcpkgManifestTask : Task
{
    public string? ManifestOutputPath { get; set; }
    public ITaskItem[]? References { get; set; }

    public override bool Execute()
    {
        if (string.IsNullOrWhiteSpace(ManifestOutputPath))
        {
            LogError("NSSDK0001: The {0} parameter must be specified for the {1} task.",
                nameof(ManifestOutputPath),
                nameof(CreateVcpkgManifestTask));

            return false;
        }

        Directory.CreateDirectory(Path.GetDirectoryName(ManifestOutputPath)!);

        References ??= Array.Empty<ITaskItem>();

        StringBuilder sb = new();

        sb.AppendLine("{");
        sb.AppendLine("    \"$schema\": \"https://raw.githubusercontent.com/microsoft/vcpkg/master/scripts/vcpkg.schema.json\",");
        sb.AppendLine("    \"dependencies\": [");

        var referenceLines = string.Join($",{Environment.NewLine}", References.Select(r => $"        \"{r.ItemSpec}\""));
        sb.AppendLine(referenceLines);

        sb.AppendLine("    ]");
        sb.AppendLine("}");

        File.WriteAllText(ManifestOutputPath, sb.ToString());
        return true;
    }

    private void LogError(string message, params object[] messageArgs)
    {
        string code = Log.ExtractMessageCode(message, out _);
        Log.LogError(subcategory: null, errorCode: code, helpKeyword: null, file: null, lineNumber: 0, columnNumber: 0, endLineNumber: 0, endColumnNumber: 0, helpLink: null, message: message, messageArgs: messageArgs);
    }
}
