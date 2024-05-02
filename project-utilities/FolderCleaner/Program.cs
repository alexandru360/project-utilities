var targetFolders = new HashSet<string> {"bin", "obj", "node_modules"};
foreach (var arg in args)
{
    targetFolders.Add(arg.Trim('\"'));
}

var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

DeleteFolders(currentDirectory, targetFolders);

static void DeleteFolders(string startLocation, HashSet<string> targetFolders)
{
    foreach (var directory in Directory.GetDirectories(startLocation))
    {
        if (targetFolders.Contains(new DirectoryInfo(directory).Name))
        {
            try
            {
                Console.WriteLine($"Deleting: {directory}");
                Directory.Delete(directory, true);
            }
            catch (IOException)
            {
                Console.WriteLine("Unable to delete \"{0}\". It may be in use.", directory);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("No permission to delete \"{0}\".", directory);
            }
        }
        else
        {
            DeleteFolders(directory, targetFolders);
        }
    }
}
