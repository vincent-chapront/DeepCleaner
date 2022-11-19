namespace DeepCleaner
{
    [Command(PackageIds.DeepCleanSolutionCommand)]
    internal sealed class DeepCleanSolutionCommand : BaseCommand<DeepCleanSolutionCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.MessageBox.ShowWarningAsync("DeepCleanSolutionCommand", "Button clicked");
        }
    }
}