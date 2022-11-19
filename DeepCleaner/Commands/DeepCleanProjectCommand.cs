using DeepCleaner.Helpers;

namespace DeepCleaner
{
    [Command(PackageIds.DeepCleanProjectCommand)]
    internal sealed class DeepCleanProjectCommand : BaseCommand<DeepCleanProjectCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var currentProject = await VS.Solutions.GetActiveProjectAsync();

            await CleanerHelpers.CleanProjectAsync(currentProject);
        }
    }
}