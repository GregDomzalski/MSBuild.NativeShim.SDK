using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace GeNet.Sdk.NativeShim;

public class CMakeBuildTask : ToolTask
{
    public string InstallPrefix { get; set; }
    public string Generator { get; set; }
    public string BuildType { get; set; }
    public string TargetRuntimeIdentifier { get; set; }
    public string ListsFile { get; set; }
    public string BuildDirectory { get; set; }

    [Required]
    public string CMakeBinDirectory { get; set; }

    protected override string ToolName => "CMakeBuild";

    protected override string GenerateFullPathToTool()
    {
        return $"{CMakeBinDirectory}\\cmake.exe";
    }

    protected override string GenerateCommandLineCommands()
    {
        string platformArg = TargetRuntimeIdentifier switch
        {
            "osx-arm" => "-DCMAKE_OSX_ARCHITECTURES=arm64",
            "osx-x64" => "-DCMAKE_OSX_ARCHITECTURES=x86_64",
            "win-arm64" => "-A arm64",
            "win-x86" => "-A Win32",
            "win-x64" => "-A x64",
            _ => ""
        };

        string[] cmakeArguments =
        {
            $"-DCMAKE_INSTALL_PREFIX=\"{InstallPrefix}\"",
            $"-G \"{Generator}\"",
            $"-DCMAKE_BUILD_TYPE={BuildType}",
            platformArg,
            $"-S {ListsFile}",
            $"-B {BuildDirectory}"
        };

        return string.Join(" ", cmakeArguments);
    }
}