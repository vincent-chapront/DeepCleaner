using DeepCleaner.Helpers;
using System.Linq;

namespace DeepCleaner
{
    [Command(PackageIds.DeepRebuildSolutionCommand)]
    internal sealed class DeepRebuildSolutionCommand : BaseCommand<DeepRebuildSolutionCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            CleanerHelpers.CleanSolutionAsync(true);
        }
    }
}