namespace OzonTestsManager;

using OzonTestsManager.Files;
using OzonTestsManager.Exception;
using System.IO.Compression;
using System.Text.Json;

public class OzonTasks : IDisposable
{
    private const string NameDefaultTestDirectory = "Test_Directory";
    private static string CurrentPathEnvironment = Environment.CurrentDirectory;
    private readonly string _sourcePath;
    private StatusProject _statusProject = new StatusProject();

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
        _sourcePath = _statusProject.GetSourceDirectory( CurrentPathEnvironment );

        if (AssignDefaultDirectoryForTasks())
        {
            _statusProject.AddPath( _testDirectory.FullName, _sourcePath );
        }
    }

    public OzonTasks(string pathToArchive)
    {
        _sourcePath = _statusProject.GetSourceDirectory( CurrentPathEnvironment );

        if (!AssignDefaultDirectoryForTasks())
        {
            if (_statusProject.IsDirectoryInHistory( _testDirectory.FullName, _sourcePath ))
                return;
            else 
                _statusProject.AddPath( _testDirectory.FullName, _sourcePath );
        }

        if (!ManagerFiles.IsFileValid( ".zip" , pathToArchive ))
        {
            throw new OzonTasksException(@"There is no such file, or its extension is not "".zip""");
        }

        ZipFile.ExtractToDirectory( pathToArchive, _testDirectory.FullName );
    }

    public OzonTasks(string pathToArchive, string pathToTestDirectory)
    {
        _sourcePath = _statusProject.GetSourceDirectory( CurrentPathEnvironment ); 

        if (!AssignCurrentDirectoryForTasks( pathToTestDirectory ))
        {
            if (_statusProject.IsDirectoryInHistory( pathToTestDirectory, _sourcePath ))
                return;
            else 
                _statusProject.AddPath( _testDirectory.FullName, _sourcePath );
        }

        if (!ManagerFiles.IsFileValid( ".zip", pathToArchive ))
        {
            throw new OzonTasksException(@"There is no such file, or its extension is not "".zip""");
        }

        ZipFile.ExtractToDirectory(pathToArchive, pathToTestDirectory);
    }

    private bool AssignDefaultDirectoryForTasks()
    {
        DirectoryInfo projectDirectory = new DirectoryInfo( _sourcePath );

        DirectoryInfo[] projectDirectoryChild = projectDirectory.GetDirectories();

        for (int i = 0; i < projectDirectoryChild.Length; i++)
        {
            if (projectDirectoryChild[i].Name == NameDefaultTestDirectory)
            {
                _testDirectory = projectDirectoryChild[i];

                return false;
            }
        }
        Directory.CreateDirectory(projectDirectory.FullName + "\\" + NameDefaultTestDirectory);
        _testDirectory = new DirectoryInfo(projectDirectory.FullName + "\\" + NameDefaultTestDirectory);

        return true;
    }


    // public void AssignCurrentDirectoryWithTests(string path)
    // {
    //     _managerFiles = new ManagerFiles(path);

    //     if (!_managerFiles.IsOneTaskReady)
    //     {
    //         throw new OzonTasksException("You are trying to assign a current directory with tests that does not have them");
    //     }
    // }

    public bool AssignCurrentDirectoryForTasks(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);

            _testDirectory = new DirectoryInfo(path);

            return true;
        }
        else 
        {
            _managerFiles = new ManagerFiles(path);

            _testDirectory = new DirectoryInfo(path);

            return false;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
    
    ~OzonTasks()
    {

    }

    private class StatusProject
    {
        private const string NameHistoryPaths = "OzonTestsManager_history_status.json";

        private readonly string? _sourcePath;

        public StatusProject(string pathEnvironment)
        {
            if (pathEnvironment.Contains("bin"))
            {
                DirectoryInfo sourcePath = new DirectoryInfo(pathEnvironment + "..\\..\\..\\");

                _sourcePath = sourcePath.FullName;
            }
            else 
            {
                _sourcePath = pathEnvironment + "\\bin\\";
            }
        }

        // public string GetSourceDirectory(string currentDirectory)
        // {
        //     if (currentDirectory.Contains("bin"))
        //     {
        //         DirectoryInfo newTestDirectory = new DirectoryInfo(currentDirectory + "..\\..\\..\\");

        //         return newTestDirectory.FullName;
        //     }
        //     else 
        //     {
        //         return currentDirectory + "\\bin\\";
        //     }
        // }

        public bool IsDirectoryInHistory(string pathToTestDirectory)
        {
            using (FileStream fs = new FileStream( _sourcePath + NameHistoryPaths, FileMode.Open ))
            {
                List<string>? paths = JsonSerializer.Deserialize<List<string>>(fs);

                foreach (string path in paths)
                {
                    if (path == pathToTestDirectory)
                        return true;
                }

                fs.Close();
            }
            return false;
        }

        public void AddPath(string sourcePath,string pathToTestDirectory)
        {
            if (!File.Exists(sourcePath + NameDefaultTestDirectory))
            {
                using (FileStream fs = new FileStream( sourcePath + NameHistoryPaths, FileMode.Create))
                {
                    List<string> patternFromJsonList = new List<string>();

                    JsonSerializer.Serialize <List<string>> (fs, patternFromJsonList);

                    fs.Close();
                }
            }
            using (FileStream fs = new FileStream( sourcePath + NameHistoryPaths, FileMode.Open, FileAccess.ReadWrite))
            {
                List<string>? listHistory = JsonSerializer.Deserialize <List<string>> (fs);

                if (listHistory.Contains(pathToTestDirectory))
                    return;

                listHistory.Add(pathToTestDirectory);

                JsonSerializer.Serialize <List<string>> (fs, listHistory);

                fs.Close();
            }
        }

        public void ParseFileWithHistoryOfSourcePaths()
        {
            
        }
    }
}