﻿using Fody;
using NUnit.Framework;

public class TempFileTests : BasicTests
{
    private static readonly TestResult testResult;
    public override TestResult TestResult => testResult;

    static TempFileTests()
    {
        testResult = WeavingHelper.CreateIsolatedAssemblyCopy("ExeToProcess.exe",
            "<Costura CreateTemporaryAssemblies='true' />",
            new[] {"AssemblyToReference.dll", "AssemblyToReferencePreEmbedded.dll", "ExeToReference.exe"}, "TempFile");
    }

    [Test]
#if NETCORE
    [Explicit("Somehow this only succeeds when ran manually for .NET Core")]
#endif
    public void ExecutableRunsSuccessfully()
    {
        var output = RunHelper.RunExecutable(TestResult.AssemblyPath);
        Assert.That(output, Is.EqualTo("Run-OK"));
    }
}
