namespace OzonTestsManager;

using OzonTestsManager.Files;
using OzonTestsManager.Exception;
using System.IO.Compression;

public class OzonTasks
{
    private static string _currentDirectory = Environment.CurrentDirectory;
    private readonly string _sourcePath;
    private DirectoryInfo? _testDirectory;

    private ManagerFiles _managerFiles;

    public string? SourceDirectoryName
    {
        get { return _testDirectory.Name; }
    }

    public string? SourceDirectoryFullName
    {
        get { return _testDirectory.FullName; }
    }


    public OzonTasks()
    {
        if (_currentDirectory.Contains("bin"))
        {
            _sourcePath = _currentDirectory;
        }
        else 
        {
            
        }

        AssignDefaultSourceDirectory();
    }

    public OzonTasks(string pathToArchive)
    {
        if (!ManagerFiles.IsFileValid( ".zip" , pathToArchive ))
        {
            throw new OzonTasksException(@"There is no such file, or its extension is not "".zip""");
        }

        AssignDefaultSourceDirectory();

        ZipFile.ExtractToDirectory(pathToArchive, _testDirectory.FullName);
    }

    public OzonTasks(string pathToArchive, string pathToTestDirectory)
    {
        if (!ManagerFiles.IsFileValid( ".zip", pathToArchive ))
        {
            throw new OzonTasksException(@"There is no such file, or its extension is not "".zip""");
        }

        AssignCurrentDirectoryFromTests(pathToTestDirectory);

        ZipFile.ExtractToDirectory(pathToArchive, pathToTestDirectory);
    }

    private void AssignDefaultSourceDirectory()
    {
        string pathToProjectDirectory = Environment.CurrentDirectory;

        if (pathToProjectDirectory.Contains("bin"))
        {

        }

        DirectoryInfo projectDirectory = new DirectoryInfo(pathToProjectDirectory);

        DirectoryInfo[] projectDirectoryChild = projectDirectory.GetDirectories();
        for (int i = 0; i < projectDirectoryChild.Length; i++)
        {
            if (projectDirectoryChild[i].Name == "SourceTasks")
            {
                _testDirectory = projectDirectoryChild[i];

                return;
            }
        }
        Directory.CreateDirectory(projectDirectory.FullName + "\\SourceTasks");
        _testDirectory = new DirectoryInfo(projectDirectory.FullName + "\\SourceTasks");
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

            _testDirectory = new DirectoryInfo(path);
        }
        else 
        {
            _managerFiles = new ManagerFiles(path);

            _testDirectory = new DirectoryInfo(path);
        }
    }


    private class StatusManager
    {
        private const string sourcePath = "";

        public void ParseFileWithHistoryOfSourcePaths()
        {
            
        }
    }
}