namespace DeepCleaner.Helpers
{
    internal static class DirectoryHelpers
    {
        internal static void DeleteDirectory(string directoryBin)
        {
            try
            {
                System.IO.Directory.Delete(directoryBin, true);
                while (System.IO.Directory.Exists(directoryBin))
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
    }
}