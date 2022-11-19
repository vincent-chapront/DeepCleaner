namespace DeepCleaner.Helpers
{
    internal static class LogHelpers
    {
        internal static async Task ShowProgressAsync(Action action, OutputWindowPane pane, string message, int currentStep, int numberOfSteps)
        {
            string log = $"Step {currentStep}/{numberOfSteps} : {message} ";
            await VS.StatusBar.ShowProgressAsync(log, currentStep, numberOfSteps);
            await pane.WriteLineAsync(log);
            action();
        }
    }
}