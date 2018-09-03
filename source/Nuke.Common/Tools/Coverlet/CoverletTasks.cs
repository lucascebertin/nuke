namespace Nuke.Common.Tools.Coverlet
{
    partial class CoverletTasks
    {
        public static CoverletSettings DefaultCoverletSettings =>
            new CoverletSettings()
                .SetTarget("dotnet")
                .SetFormat("lcov")
                .SetThreshold("80");
    }
}
