using System.Linq;
using System.Threading.Tasks;

namespace DeepCleaner.Helpers
{
    internal static class CleanerHelpers
    {
        private static OutputWindowPane DeepCleanerPane = null;

        public static async Task<OutputWindowPane> GetPaneAsync()
        {
            if (DeepCleanerPane == null)
            {
                DeepCleanerPane = await VS.Windows.CreateOutputWindowPaneAsync("DeepCleaner");
            }
            return DeepCleanerPane;
        }

        internal static async Task CleanSolutionAsync(bool rebuild = false)
        {
            OutputWindowPane pane = await GetPaneAsync();
            await VS.Windows.ShowToolWindowAsync(pane.Guid);

            var projects = (await VS.Solutions.GetAllProjectsAsync()).ToArray();
            var totalStepsNumber = projects.Length * 2 + (rebuild ? 1 : 0);
            string actionName = rebuild ? "DeepRebuild" : "DeepClean";
            await pane.WriteLineAsync($"Starting {actionName} of Solution");

            await pane.WriteLineAsync("");
            await pane.WriteLineAsync("Starting DeepClean of Solution");
            await CleanProjectsAsync(pane, projects, totalStepsNumber);
            await pane.WriteLineAsync("DeepClean of Solution completed");
            await pane.WriteLineAsync("");

            if (rebuild)
            {
                await pane.WriteLineAsync(LogHelpers.BuildHeader("Rebuild Solution"));
                await pane.WriteLineAsync("Starting Rebuild of solution");
                await VS.StatusBar.ShowProgressAsync("Rebuild Solutions", totalStepsNumber, totalStepsNumber);
                await VS.Build.BuildSolutionAsync();
                await pane.WriteLineAsync("Rebuild of solution completed");
                await pane.WriteLineAsync("");
            }


            await pane.WriteLineAsync($"{actionName} of Solution completed");

            await VS.StatusBar.ShowMessageAsync("Ready");
        }

        private static async Task CleanProjectsAsync(OutputWindowPane pane, Project[] projects, int totalStepsNumber = 2)
        {
            for (int projectIdx = 0; projectIdx < projects.Length; projectIdx++)
            {
                Project currentProject = projects[projectIdx];
                if (projects.Length == 1)
                {
                    await pane.WriteLineAsync($"{LogHelpers.BuildHeader($"Project : {currentProject.Name}")}");
                }
                else
                {
                    await pane.WriteLineAsync($"{LogHelpers.BuildHeader($"Project {projectIdx + 1}/{projects.Length} : {currentProject.Name}")}");
                }
                var directory = System.IO.Path.GetDirectoryName(currentProject.FullPath);

                await pane.WriteLineAsync("Starting DeepClean");

                await VS.StatusBar.ShowProgressAsync($"{currentProject.Name} : Deleting BIN", (projectIdx * 2) + 1, totalStepsNumber);
                await pane.WriteLineAsync("Step 1/2 : Deleting BIN");
                DirectoryHelpers.DeleteDirectory(System.IO.Path.Combine(directory, "bin"));

                await VS.StatusBar.ShowProgressAsync($"{currentProject.Name} : Deleting OBJ", (projectIdx * 2) + 2, totalStepsNumber);
                await pane.WriteLineAsync("Step 2/2 : Deleting OBJ");
                DirectoryHelpers.DeleteDirectory(System.IO.Path.Combine(directory, "obj"));

                await pane.WriteLineAsync("DeepClean completed");
                await pane.WriteLineAsync("");
            }
        }

        internal static async Task CleanProjectAsync(Project currentProject, bool redbuild = false)
        {
            OutputWindowPane pane = await GetPaneAsync();
            await VS.Windows.ShowToolWindowAsync(pane.Guid);

            await CleanProjectsAsync(pane, new Project[] { currentProject }, (redbuild ? 3 : 2));

            if (redbuild)
            {
                await pane.WriteLineAsync(LogHelpers.BuildHeader("Rebuild Solution"));
                await pane.WriteLineAsync($"Starting Rebuild of project {currentProject.Name}");
                await VS.StatusBar.ShowProgressAsync("Rebuild project", 3, 3);
                await VS.Build.BuildSolutionAsync();
                await pane.WriteLineAsync($"Rebuild of project {currentProject.Name} completed");
                await pane.WriteLineAsync("");
                await pane.WriteLineAsync("DeepRebuild of Solution completed");

                await VS.StatusBar.ShowMessageAsync("Ready");
            }
        }
    }
}