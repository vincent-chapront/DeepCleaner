namespace DeepCleaner
{
    [Command(PackageIds.DeepCleanProjectCommand)]
    internal sealed class DeepCleanProjectCommand : BaseCommand<DeepCleanProjectCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.MessageBox.ShowWarningAsync("DeepCleanProjectCommand", "Button clicked");
        }
    }
}