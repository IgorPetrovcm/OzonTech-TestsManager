namespace OzonTestsManager;

using OzonTestsManager.Files;

public class OzonTasks
{
    private DirectoryInfo? _sourceDirectory;

    private ManagerFiles _managerFiles;

    public OzonTasks()
    {
        string pathToProjectDirectory = Environment.CurrentDirectory;
        DirectoryInfo projectDirectory = new DirectoryInfo(pathToProjectDirectory);

        DirectoryInfo[] projectDirectoryChild = projectDirectory.GetDirectories();
        for (int i = 0; i < projectDirectoryChild.Length; i++)
        {
            if (projectDirectoryChild[i].Name == "SourceTasks")
            {
                _sourceDirectory = projectDirectoryChild[i];

                return;
            }
        }
        Directory.CreateDirectory(projectDirectory.FullName + "\\SourceTasks");
        _sourceDirectory = new DirectoryInfo(projectDirectory.FullName + "\\SourceTasks");
    }

    public void AssignCurrentDirectoryWithTests(string path)
    {
        _managerFiles = new ManagerFiles(path);

        if (!_managerFiles.IsOneTaskReady)
        {
            throw new Exception("You are trying to assign a current directory with tests that does not have them");
        }
    }
}