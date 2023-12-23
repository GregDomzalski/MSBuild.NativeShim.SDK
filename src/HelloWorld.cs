using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System;

namespace GeNet.Sdk.NativeShim;

public class HelloWorld : ToolTask
{
    protected override string ToolName => "HelloWorld";

    protected override string GenerateFullPathToTool()
    {
        Console.WriteLine("Hello, World!");
        return "";
    }
}