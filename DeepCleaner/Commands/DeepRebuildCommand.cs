namespace DeepCleaner
{
    [Command(PackageIds.DeepRebuildProjectCommand)]
    internal sealed class DeepRebuildProjectCommand : BaseCommand<DeepRebuildProjectCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.MessageBox.ShowWarningAsync("DeepRebuildProjectCommand", "Button clicked");
        }
    }
}