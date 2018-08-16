﻿// Copyright 2018 Maintainers and Contributors of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Nuke.Common.OutputSinks;
using Nuke.Common.Utilities;

namespace Nuke.Common.Execution
{
    internal static class BuildExecutor
    {
        public const string DefaultTarget = "default";

        public static int Execute<T>(Expression<Func<T, Target>> defaultTargetExpression)
            where T : NukeBuild
        {
            Logger.Log(FigletTransform.GetText("NUKE"));
            Logger.Log($"Version: {typeof(BuildExecutor).GetTypeInfo().Assembly.GetInformationalText()}");
            Logger.Log($"Host: {EnvironmentInfo.HostType}");
            Logger.Log();

            var executionList = default(IReadOnlyCollection<TargetDefinition>);
            try
            {
                var build = CreateBuildInstance(defaultTargetExpression);
                InjectionService.InjectValues(build);
                HandleEarlyExits(build);

                executionList = TargetDefinitionLoader.GetExecutingTargets(build, build.InvokedTargets);
                RequirementService.ValidateRequirements(executionList, build);
                Execute(executionList);

                return 0;
            }
            catch (AggregateException exception)
            {
                foreach (var innerException in exception.Flatten().InnerExceptions)
                    OutputSink.Error(innerException.Message, innerException.StackTrace);
                return -exception.Message.GetHashCode();
            }
            catch (TargetInvocationException exception)
            {
                var innerException = exception.InnerException.NotNull();
                OutputSink.Error(innerException.Message, innerException.StackTrace);
                return -exception.Message.GetHashCode();
            }
            catch (Exception exception)
            {
                OutputSink.Error(exception.Message, exception.StackTrace);
                return -exception.Message.GetHashCode();
            }
            finally
            {
                if (executionList != null)
                    OutputSink.WriteSummary(executionList);
            }
        }

        internal static void Execute(IEnumerable<TargetDefinition> executionList)
        {
            foreach (var target in executionList)
            {
                if (target.Factory == null)
                {
                    target.Status = ExecutionStatus.Absent;
                    continue;
                }

                if (target.Skip || target.DependencyBehavior == DependencyBehavior.Execute && target.Conditions.Any(x => !x()))
                {
                    target.Status = ExecutionStatus.Skipped;
                    continue;
                }

                using (Logger.Block(target.Name))
                {
                    var stopwatch = Stopwatch.StartNew();
                    try
                    {
                        target.Actions.ForEach(x => x());
                        target.Duration = stopwatch.Elapsed;

                        target.Status = ExecutionStatus.Executed;
                    }
                    catch
                    {
                        target.Status = ExecutionStatus.Failed;
                        throw;
                    }
                    finally
                    {
                        target.Duration = stopwatch.Elapsed;
                    }
                }
            }
        }

        private static void HandleEarlyExits<T>(T build)
            where T : NukeBuild
        {
            if (build.Help)
            {
                Logger.Log(HelpTextService.GetTargetsText(build));
                Logger.Log(HelpTextService.GetParametersText(build));
            }

            if (build.Graph)
                GraphService.ShowGraph(build);

            if (build.Help || build.Graph)
                Environment.Exit(exitCode: 0);
        }

        private static T CreateBuildInstance<T>(Expression<Func<T, Target>> defaultTargetExpression)
            where T : NukeBuild
        {
            var constructors = typeof(T).GetConstructors();
            ControlFlow.Assert(constructors.Length == 1 && constructors.Single().GetParameters().Length == 0,
                $"Type '{typeof(T).Name}' must declare a single parameterless constructor.");

            var build = Activator.CreateInstance<T>();
            build.TargetDefinitions = build.GetTargetDefinitions(defaultTargetExpression);
            NukeBuild.Instance = build;

            return build;
        }
    }
}
