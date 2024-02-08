namespace OzonTestsManager;

using OzonTestsManager.Files;
using OzonTestsManager.Exception;
using System.IO.Compression;

public class OzonTasks
{
    private DirectoryInfo? _sourceDirectory;

    private ManagerFiles _managerFiles;

    public string? SourceDirectoryName
    {
        get { return _sourceDirectory.Name; }
    }

    public string? SourceDirectoryFullName
    {
        get { return _sourceDirectory.FullName; }
    }



    public OzonTasks()
    {
        AssignDefaultSourceDirectory();
    }

    public OzonTasks(string pathToArchive)
    {
        if (!File.Exists(pathToArchive))
        {
            throw new OzonTasksException("The archive does not exist");
        }
        
        FileInfo archive = new FileInfo(pathToArchive);

        if (!archive.Name.EndsWith(".zip"))
        {
            throw new OzonTasksException(@"The archive extension should be "".zip""");
        }

        AssignDefaultSourceDirectory();

        ZipFile.ExtractToDirectory(archive.FullName, _sourceDirectory.FullName);
    }

    

    private void AssignDefaultSourceDirectory()
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
            throw new OzonTasksException("You are trying to assign a current directory with tests that does not have them");
        }
    }

    public void AssignCurrentDirectoryFromTests(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);

            _sourceDirectory = new DirectoryInfo(path);
        }
        else 
        {
            _managerFiles = new ManagerFiles(path);

            _sourceDirectory = new DirectoryInfo(path);
        }
    }
}