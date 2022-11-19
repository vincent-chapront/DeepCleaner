namespace DeepCleaner
{
    [Command(PackageIds.DeepRebuildSolutionCommand)]
    internal sealed class DeepRebuildSolutionCommand : BaseCommand<DeepRebuildSolutionCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.MessageBox.ShowWarningAsync("DeepRebuildSolutionCommand", "Button clicked");
        }
    }
}