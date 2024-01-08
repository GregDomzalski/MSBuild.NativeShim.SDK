using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace GeNet.Sdk.NativeShim;

public class CMakeBuildTask : ToolTask
{
    /// <summary>
    /// Fully qualified path to CMake
    /// </summary>
    [Required]
    public string CMakeToolPath { get; set; } = null!;

    /// <summary>
    /// The --build parameter to cmake.
    /// </summary>
    [Required]
    public string BuildDirectory { get; set; } = null!;

    /// <summary>
    /// The --config param
    /// </summary>
    public string? Configuration { get; set; }

    private new bool UseCommandProcessor { get; set; }

    protected override string ToolName => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "cmake.exe" : "cmake";

    protected override string GenerateFullPathToTool() => ToolExe;

    protected override MessageImportance StandardErrorLoggingImportance => MessageImportance.High;

    public CMakeBuildTask()
    {
        EnvironmentVariables = ["PATH=/usr/bin"];
    }

    // https://cmake.org/cmake/help/latest/manual/cmake.1.html#build-a-project
    protected override string GenerateCommandLineCommands()
    {
        List<string> cmakeArguments =
        [
            $"--build {BuildDirectory}"
        ];

        if (!string.IsNullOrWhiteSpace(Configuration))
        {
            cmakeArguments.Add($"--config {Configuration}");
        }

        return string.Join(" ", cmakeArguments);
    }
}
