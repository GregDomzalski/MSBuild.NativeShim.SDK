using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;

namespace GeNet.Sdk.NativeShim;

public class CMakeConfigureTask : ToolTask
{
    /// <summary>
    /// Fully qualified path to CMake
    /// </summary>
    [Required]
    public string CMakeToolPath { get; set; } = null!;

    /// <summary>
    /// The -B parameter. Where to store all of the CMake build files.
    /// </summary>
    [Required]
    public string BuildDirectory { get; set; } = null!;

    /// <summary>
    /// The -S parameter. Where the CMakeLists.txt file lives.
    /// </summary>
    [Required]
    public string SourceDirectory { get; set; } = null!;

    /// <summary>
    /// The -D key / value pairs
    /// </summary>
    public ITaskItem[]? Defines { get; set; }

    /// <summary>
    /// The -G generator value. Expected 'Unix Makefiles', 'Visual Studio', and 'XCode'
    /// </summary>
    [Required]
    public string Generator { get; set; } = null!;

    /// <summary>
    /// The -A parameter
    /// </summary>
    public string? PlatformName { get; set; }

    /// <summary>
    /// The --toolchain parameter
    /// </summary>
    public string? ToolchainFilePath { get; set; }

    /// <summary>
    /// The --install-prefix parameter
    /// </summary>
    public string? InstallPrefixDirectory { get; set; }

    public string? LogLevel { get; set; }

    public bool DebugOutput { get; set; }

    public bool Trace { get; set; }

    private new bool UseCommandProcessor { get; set; }

    protected override string ToolName => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "cmake.exe" : "cmake";

    protected override string GenerateFullPathToTool() => ToolExe;

    protected override MessageImportance StandardErrorLoggingImportance => MessageImportance.High;

    public CMakeConfigureTask()
    {
        EnvironmentVariables = ["PATH=/usr/bin"];
    }

    // From CMake documentation: https://cmake.org/cmake/help/latest/manual/cmake.1.html#generate-a-project-buildsystem
    // Generate a Project BuildSystem
    // cmake [<options>] -B path-to-build -S path-to-source
    // -D <var>:<type>=<value>
    // -U <globbing_expr> remove entries from CMake cache
    // -G <generator-name>
    // -T <toolset-spec>
    // -A <platform-name>
    // --toolchain <path-to-file> # Cross compiling toolchain file
    // --install-prefix <directory> # Absolute path to install directory
    // --log_level=<level>
    // --debug-output
    // --trace
    protected override string GenerateCommandLineCommands()
    {
        var cmakeDefines = string.Empty;

        if (Defines is not null)
        {
            cmakeDefines = string.Join(" ",
                Defines.Select(d => $"-D{d.ItemSpec}={d.GetMetadata("Value")}").ToArray());
        }

        List<string> cmakeArguments =
        [
            $"-S {SourceDirectory}",
            $"-B {BuildDirectory}",
            $"-G \"{Generator}\""
        ];

        if (!string.IsNullOrEmpty(cmakeDefines))
        {
            cmakeArguments.Add(cmakeDefines);
        }

        if (!string.IsNullOrEmpty(PlatformName))
        {
            cmakeArguments.Add($"-A {PlatformName}");
        }

        if (!string.IsNullOrEmpty(ToolchainFilePath))
        {
            cmakeArguments.Add($"--toolchain {ToolchainFilePath}");
        }

        if (!string.IsNullOrEmpty(InstallPrefixDirectory))
        {
            cmakeArguments.Add($"--install-prefix {InstallPrefixDirectory}");
        }

        if (!string.IsNullOrEmpty(LogLevel))
        {
            cmakeArguments.Add($"--log-level={LogLevel}");
        }

        if (DebugOutput)
        {
            cmakeArguments.Add("--debug-output");
        }

        if (Trace)
        {
            cmakeArguments.Add("--trace");
        }

        return string.Join(" ", cmakeArguments);
    }
}
