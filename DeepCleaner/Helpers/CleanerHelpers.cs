namespace DeepCleaner.Helpers
{
    internal static class CleanerHelpers
    {
        internal static async Task CleanProject(Project currentProject)
        {
            var directory = System.IO.Path.GetDirectoryName(currentProject.FullPath);

            OutputWindowPane pane = await VS.Windows.CreateOutputWindowPaneAsync("DeepCleaner");
            await pane.WriteLineAsync($"Starting DeepCleaning of project {currentProject.Name}");

            await LogHelpers.ShowProgressAsync(
                () => DirectoryHelpers.DeleteDirectory(System.IO.Path.Combine(directory, "bin")),
                pane,
                "Deleting BIN",
                1,
                2
                );

            await LogHelpers.ShowProgressAsync(
                () => DirectoryHelpers.DeleteDirectory(System.IO.Path.Combine(directory, "obj")),
                pane,
                "Deleting OBJ",
                2,
                2
                );

            await pane.WriteLineAsync($"DeepCleaning of project {currentProject.Name} complete");
        }
    }
}