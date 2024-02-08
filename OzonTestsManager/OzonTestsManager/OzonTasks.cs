namespace OzonTestsManager;

using OzonTestsManager.Files;
using OzonTestsManager.Exception;
using System.IO.Compression;
using System.Text.Json;

public class OzonTasks
{
    private const string NameDefaultTestDirectory = "Test_Directory";
    private static string _currentDirectory = Environment.CurrentDirectory;
    private readonly string _sourcePath;
    private StatusManager _statusManager = new StatusManager();

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
        _sourcePath = _statusManager.GetSourceDirectory(_currentDirectory);

        AssignDefaultSourceDirectory();
    }

    public OzonTasks(string pathToArchive)
    {
        _sourcePath = _statusManager.GetSourceDirectory(_currentDirectory);

        if (!ManagerFiles.IsFileValid( ".zip" , pathToArchive ))
        {
            throw new OzonTasksException(@"There is no such file, or its extension is not "".zip""");
        }

        AssignDefaultSourceDirectory();

        if (!_statusManager.IsDirectoryInHistory( _testDirectory.FullName, _sourcePath ))
        {
            ZipFile.ExtractToDirectory(pathToArchive, _testDirectory.FullName);
        }
    }

    public OzonTasks(string pathToArchive, string pathToTestDirectory)
    {
        _sourcePath = _statusManager.GetSourceDirectory(_currentDirectory);

        if (!ManagerFiles.IsFileValid( ".zip", pathToArchive ))
        {
            throw new OzonTasksException(@"There is no such file, or its extension is not "".zip""");
        }

        AssignCurrentDirectoryFromTests(pathToTestDirectory);

        ZipFile.ExtractToDirectory(pathToArchive, pathToTestDirectory);
    }

    private void AssignDefaultSourceDirectory()
    {
        DirectoryInfo projectDirectory = new DirectoryInfo( _sourcePath );

        DirectoryInfo[] projectDirectoryChild = projectDirectory.GetDirectories();

        for (int i = 0; i < projectDirectoryChild.Length; i++)
        {
            if (projectDirectoryChild[i].Name == NameDefaultTestDirectory)
            {
                _testDirectory = projectDirectoryChild[i];

                return;
            }
        }
        Directory.CreateDirectory(projectDirectory.FullName + "\\" + NameDefaultTestDirectory);
        _testDirectory = new DirectoryInfo(projectDirectory.FullName + "\\" + NameDefaultTestDirectory);
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
        private const string NameHistoryPaths = "OzonTestsManager_history.json";

        public string GetSourceDirectory(string currentDirectory)
        {
            if (currentDirectory.Contains("bin"))
            {
                return _currentDirectory;
            }
            else 
            {
                return _currentDirectory + "\\bin\\Debug\\net" +  Environment.Version.ToString()[0] + ".0\\";
            }
        }

        public bool IsDirectoryInHistory(string pathToTestDirectory, string sourcePath)
        {
            using (FileStream fs = new FileStream( sourcePath + NameHistoryPaths, FileMode.Open ))
            {
                List<string>? paths = JsonSerializer.Deserialize<List<string>>(fs);

                foreach (string path in paths)
                {
                    if (path == pathToTestDirectory)
                        return true;
                }
            }
            return false;
        }

        public void ParseFileWithHistoryOfSourcePaths()
        {
            
        }
    }
}