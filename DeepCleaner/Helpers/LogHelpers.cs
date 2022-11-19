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

        internal static string BuildHeader(string s, int maxSize = 100)
        {
            var l = (maxSize - s.Length) / 2;
            var fix = new string('=', l);
            var res = $"{fix} {s} {fix}";
            if (res.Length % 2 == 1)
            {
                res = $"{res}=";
            }
            return res;
        }
    }
}