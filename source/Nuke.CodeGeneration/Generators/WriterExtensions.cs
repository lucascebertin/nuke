// Copyright 2018 Maintainers and Contributors of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Nuke.CodeGeneration.Model;
using Nuke.CodeGeneration.Writers;

namespace Nuke.CodeGeneration.Generators
{
    public static class WriterExtensions
    {
        public static T WriteSummary<T>(this T writerWrapper, Task task)
            where T : IWriterWrapper
        {
            var paragraphs = new List<string>();
            paragraphs.Add((task.Help ?? task.Tool.Help).Paragraph());
            paragraphs.Add(GetOfficialUrlText(task.OfficialUrl ?? task.Tool.OfficialUrl).Paragraph());
            paragraphs.Add(("This task is a <a href=\"https://www.nuke.build/getting-started.html#clt-wrappers\">CLT wrapper</a> "
                           + "and allows to modify the following arguments:").Paragraph());
            paragraphs.AddRange(GetArgumentsList(task.SettingsClass));
            
            return writerWrapper.WriteSummary(paragraphs.ToArray());
        }

        private static IEnumerable<string> GetArgumentsList(SettingsClass settingsClass)
        {
            var properties = settingsClass.Properties.Where(x => !string.IsNullOrEmpty(x.Format)).ToList();
            if (properties.Count == 0)
                yield break;

            yield return "<ul>";
            foreach (var property in properties.OrderBy(x => x.Format))
            {
                var format = property.Format;
                if (format == null)
                    continue;
                
                var valueIndex = format.IndexOf(value: '{');
                var argument = valueIndex == -1
                    ? format
                    : valueIndex != 0
                        ? format.Substring(0, valueIndex).TrimEnd(':', '=', ' ')
                        : $"&lt;{property.Name.ToInstance()}&gt;";
                yield return $"  <li><c>{argument}</c> via {(settingsClass.Name + "." + property.Name).ToSeeCref()}</li>";
                
            }

            yield return "</ul>";
        }

        public static T WriteSummary<T>(this T writerWrapper, Property property)
            where T : IWriterWrapper
        {
            return writerWrapper.WriteSummary(property.Help);
        }

        public static T WriteSummary<T>(this T writerWrapper, DataClass dataClass)
            where T : IWriterWrapper
        {
            return writerWrapper.WriteSummary(GetUsedWithinText(dataClass.Tool));
        }

        public static T WriteSummary<T>(this T writerWrapper, Enumeration enumeration)
            where T : IWriterWrapper
        {
            return writerWrapper.WriteSummary(GetUsedWithinText(enumeration.Tool));
        }

        public static T WriteSummaryExtension<T>(this T writerWrapper, string actionText, Property property, Property alternativeProperty = null)
            where T : IWriterWrapper
        {
            return writerWrapper.WriteSummary(
                actionText.Emphasize().Paragraph(),
                (property.Help ?? alternativeProperty?.Help).Paragraph());
        }

        private static string GetUsedWithinText(Tool tool)
        {
            return $"Used within {tool.GetClassName().ToSeeCref()}.";
        }

        private static string GetOfficialUrlText(string url)
        {
            return $"For more details, visit the <a href=\"{url}\">official website</a>.";
        }

        public static T WriteSummary<T>(this T writerWrapper, params string[] lines)
            where T : IWriterWrapper
        {
            lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            
            if (lines.Length == 0)
                return writerWrapper;

            writerWrapper
                .WriteLine("/// <summary>")
                .ForEachWriteLine(lines.Select(x => $"///   {x}"))
                .WriteLine("/// </summary>");
                
            return writerWrapper;
        }
    }
}
