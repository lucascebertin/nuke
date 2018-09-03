// Copyright 2018 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

// Generated with Nuke.CodeGeneration, Version: Local.
// Generated from https://github.com/lucascebertin/nuke/blob/master/build/specifications/Coverlet.json.

using JetBrains.Annotations;
using Nuke.Common.Tooling;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Nuke.Common.Tools.Coverlet
{
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class CoverletTasks
    {
        /// <summary><p>Path to the Coverlet executable.</p></summary>
        public static string CoverletPath => ToolPathResolver.GetPathExecutable("coverlet");
        /// <summary><p><c>Coverlet</c> is a cross platform code coverage library for .NET Core, with support for line, branch and method coverage.The <c>dotnet test</c> command is used to execute unit tests in a given project. Unit tests are console application projects that have dependencies on the unit test framework (for example, MSTest, NUnit, or xUnit) and the dotnet test runner for the unit testing framework. These are packaged as NuGet packages and are restored as ordinary dependencies for the project.</p></summary>
        public static IReadOnlyCollection<Output> Coverlet(string arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool logOutput = true, Func<string, string> outputFilter = null)
        {
            var process = ProcessTasks.StartProcess(CoverletPath, arguments, workingDirectory, environmentVariables, timeout, logOutput, null, outputFilter);
            process.AssertZeroExitCode();
            return process.Output;
        }
        /// <summary><p><c>Coverlet</c> is a cross platform code coverage library for .NET Core, with support for line, branch and method coverage.The <c>dotnet test</c> command is used to execute unit tests in a given project. Unit tests are console application projects that have dependencies on the unit test framework (for example, MSTest, NUnit, or xUnit) and the dotnet test runner for the unit testing framework. These are packaged as NuGet packages and are restored as ordinary dependencies for the project.</p><p>For more details, visit the <a href="https://github.com/tonerdo/coverlet/">official website</a>.</p></summary>
        public static IReadOnlyCollection<Output> Coverlet(Configure<CoverletSettings> configurator = null)
        {
            var toolSettings = configurator.InvokeSafe(new CoverletSettings());
            var process = ProcessTasks.StartProcess(toolSettings);
            process.AssertZeroExitCode();
            return process.Output;
        }
    }
    #region CoverletSettings
    /// <summary><p>Used within <see cref="CoverletTasks"/>.</p></summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    [Serializable]
    public partial class CoverletSettings : ToolSettings
    {
        /// <summary><p>Path to the Coverlet executable.</p></summary>
        public override string ToolPath => base.ToolPath ?? CoverletTasks.CoverletPath;
        /// <summary><p>Path to the test assembly.</p></summary>
        public virtual string Assembly { get; internal set; }
        /// <summary><p>Path to the test runner application.</p></summary>
        public virtual string Target { get; internal set; }
        /// <summary><p>Arguments to be passed to the test runner.</p></summary>
        public virtual string TargetArgs { get; internal set; }
        /// <summary><p>Output of the generated coverage report</p></summary>
        public virtual string Output { get; internal set; }
        /// <summary><p>Format of the generated coverage report.</p></summary>
        public virtual string Format { get; internal set; }
        /// <summary><p>Exits with error if the coverage % is below value.</p></summary>
        public virtual string Threshold { get; internal set; }
        /// <summary><p>Coverage type to apply the threshold to.</p></summary>
        public virtual string ThresholdType { get; internal set; }
        /// <summary><p>Filter expressions to exclude specific modules and types.</p></summary>
        public virtual string Exclude { get; internal set; }
        /// <summary><p>Filter expressions to include specific modules and types.</p></summary>
        public virtual string Include { get; internal set; }
        /// <summary><p>Glob patterns specifying source files to exclude.</p></summary>
        public virtual string ExcludeByFile { get; internal set; }
        protected override Arguments ConfigureArguments(Arguments arguments)
        {
            arguments
              .Add("{value}", Assembly)
              .Add("--target {value}", Target)
              .Add("--targetargs {value}", TargetArgs)
              .Add("--output {value}", Output)
              .Add("--format {value}", Format)
              .Add("--threshold {value}", Threshold)
              .Add("--threshold-type {value}", ThresholdType)
              .Add("--exclude {value}", Exclude)
              .Add("--include {value}", Include)
              .Add("--exclude-by-file {value}", ExcludeByFile);
            return base.ConfigureArguments(arguments);
        }
    }
    #endregion
    #region CoverletSettingsExtensions
    /// <summary><p>Used within <see cref="CoverletTasks"/>.</p></summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class CoverletSettingsExtensions
    {
        #region Assembly
        /// <summary><p><em>Sets <see cref="CoverletSettings.Assembly"/>.</em></p><p>Path to the test assembly.</p></summary>
        [Pure]
        public static CoverletSettings SetAssembly(this CoverletSettings toolSettings, string assembly)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Assembly = assembly;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="CoverletSettings.Assembly"/>.</em></p><p>Path to the test assembly.</p></summary>
        [Pure]
        public static CoverletSettings ResetAssemly(this CoverletSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Assembly = null;
            return toolSettings;
        }
        #endregion
        #region Target
        /// <summary><p><em>Sets <see cref="CoverletSettings.Target"/>.</em></p><p>Path to the test runner application.</p></summary>
        [Pure]
        public static CoverletSettings SetTarget(this CoverletSettings toolSettings, string target)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Target = target;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="CoverletSettings.Target"/>.</em></p><p>Path to the test runner application.</p></summary>
        [Pure]
        public static CoverletSettings ResetTarget(this CoverletSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Target = null;
            return toolSettings;
        }
        #endregion
        #region TargetArgs
        /// <summary><p><em>Sets <see cref="CoverletSettings.TargetArgs"/>.</em></p><p>Arguments to be passed to the test runner.</p></summary>
        [Pure]
        public static CoverletSettings SetTargetArgs(this CoverletSettings toolSettings, string targetArgs)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.TargetArgs = targetArgs;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="CoverletSettings.TargetArgs"/>.</em></p><p>Arguments to be passed to the test runner.</p></summary>
        [Pure]
        public static CoverletSettings ResetTargetArgs(this CoverletSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.TargetArgs = null;
            return toolSettings;
        }
        #endregion
        #region Output
        /// <summary><p><em>Sets <see cref="CoverletSettings.Output"/>.</em></p><p>Output of the generated coverage report</p></summary>
        [Pure]
        public static CoverletSettings SetOutput(this CoverletSettings toolSettings, string output)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Output = output;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="CoverletSettings.Output"/>.</em></p><p>Output of the generated coverage report</p></summary>
        [Pure]
        public static CoverletSettings ResetOutput(this CoverletSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Output = null;
            return toolSettings;
        }
        #endregion
        #region Format
        /// <summary><p><em>Sets <see cref="CoverletSettings.Format"/>.</em></p><p>Format of the generated coverage report.</p></summary>
        [Pure]
        public static CoverletSettings SetFormat(this CoverletSettings toolSettings, string format)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Format = format;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="CoverletSettings.Format"/>.</em></p><p>Format of the generated coverage report.</p></summary>
        [Pure]
        public static CoverletSettings ResetFormat(this CoverletSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Format = null;
            return toolSettings;
        }
        #endregion
        #region Threshold
        /// <summary><p><em>Sets <see cref="CoverletSettings.Threshold"/>.</em></p><p>Exits with error if the coverage % is below value.</p></summary>
        [Pure]
        public static CoverletSettings SetThreshold(this CoverletSettings toolSettings, string threshold)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Threshold = threshold;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="CoverletSettings.Threshold"/>.</em></p><p>Exits with error if the coverage % is below value.</p></summary>
        [Pure]
        public static CoverletSettings ResetThreshold(this CoverletSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Threshold = null;
            return toolSettings;
        }
        #endregion
        #region ThresholdType
        /// <summary><p><em>Sets <see cref="CoverletSettings.ThresholdType"/>.</em></p><p>Coverage type to apply the threshold to.</p></summary>
        [Pure]
        public static CoverletSettings SetThresholdType(this CoverletSettings toolSettings, string thresholdType)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ThresholdType = thresholdType;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="CoverletSettings.ThresholdType"/>.</em></p><p>Coverage type to apply the threshold to.</p></summary>
        [Pure]
        public static CoverletSettings ResetThresholdType(this CoverletSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ThresholdType = null;
            return toolSettings;
        }
        #endregion
        #region Exclude
        /// <summary><p><em>Sets <see cref="CoverletSettings.Exclude"/>.</em></p><p>Filter expressions to exclude specific modules and types.</p></summary>
        [Pure]
        public static CoverletSettings SetExclude(this CoverletSettings toolSettings, string exclude)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Exclude = exclude;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="CoverletSettings.Exclude"/>.</em></p><p>Filter expressions to exclude specific modules and types.</p></summary>
        [Pure]
        public static CoverletSettings ResetExclude(this CoverletSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Exclude = null;
            return toolSettings;
        }
        #endregion
        #region Include
        /// <summary><p><em>Sets <see cref="CoverletSettings.Include"/>.</em></p><p>Filter expressions to include specific modules and types.</p></summary>
        [Pure]
        public static CoverletSettings SetInclude(this CoverletSettings toolSettings, string include)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Include = include;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="CoverletSettings.Include"/>.</em></p><p>Filter expressions to include specific modules and types.</p></summary>
        [Pure]
        public static CoverletSettings ResetInclude(this CoverletSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Include = null;
            return toolSettings;
        }
        #endregion
        #region ExcludeByFile
        /// <summary><p><em>Sets <see cref="CoverletSettings.ExcludeByFile"/>.</em></p><p>Glob patterns specifying source files to exclude.</p></summary>
        [Pure]
        public static CoverletSettings SetExcludeByFile(this CoverletSettings toolSettings, string excludeByFile)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ExcludeByFile = excludeByFile;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="CoverletSettings.ExcludeByFile"/>.</em></p><p>Glob patterns specifying source files to exclude.</p></summary>
        [Pure]
        public static CoverletSettings ResetExcludeByFile(this CoverletSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ExcludeByFile = null;
            return toolSettings;
        }
        #endregion
    }
    #endregion
}
