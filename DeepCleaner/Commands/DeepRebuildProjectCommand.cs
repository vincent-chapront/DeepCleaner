using DeepCleaner.Helpers;

namespace DeepCleaner
{
    [Command(PackageIds.DeepRebuildProjectCommand)]
    internal sealed class DeepRebuildProjectCommand : BaseCommand<DeepRebuildProjectCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var currentProject = await VS.Solutions.GetActiveProjectAsync();

            await CleanerHelpers.CleanProjectAsync(currentProject, true);
        }
    }
}