namespace OzonTestsManager;

using OzonTestsManager.Files;
using OzonTestsManager.Exception;
using System.IO.Compression;
using System.Text.Json;
using OzonTestsManager.Entities;
using OzonTestsManager.Report;

public class OzonTasks : IDisposable
{
    private const string NameDefaultTestDirectory = "Test_Directory";
    private static string CurrentPathEnvironment = Environment.CurrentDirectory;
    private StatusProject _statusProject = new StatusProject( CurrentPathEnvironment, NameDefaultTestDirectory );

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


    public OzonTasks(string pathToArchive)
    {
        if (!AssignDefaultDirectoryForTasks())
        {
            if (_statusProject.IsDirectoryInHistory( _testDirectory.FullName ))
            {
                _managerFiles = new ManagerFiles( _testDirectory.FullName );

                return;
            }
        }

        if (!ManagerFiles.IsFileValid( ".zip" , pathToArchive ))
        {
            throw new OzonTasksException(@"There is no such file, or its extension is not "".zip""");
        }

        ZipFile.ExtractToDirectory( pathToArchive, _testDirectory.FullName );

        _statusProject.AddPath(_testDirectory.FullName);

        _managerFiles = new ManagerFiles( _testDirectory.FullName ); 
    }

    public OzonTasks(string pathToArchive, string pathToTestDirectory)
    {
        if (!AssignCurrentDirectoryForTasks( pathToTestDirectory ))
        {
            if (_statusProject.IsDirectoryInHistory( pathToTestDirectory ))
            {
                _managerFiles = new ManagerFiles( _testDirectory.FullName );

                return;
            }
        }

        if (!ManagerFiles.IsFileValid( ".zip", pathToArchive ))
        {
            throw new OzonTasksException(@"There is no such file, or its extension is not "".zip""");
        }

        ZipFile.ExtractToDirectory(pathToArchive, pathToTestDirectory);

        _statusProject.AddPath( _testDirectory.FullName );

        _managerFiles = new ManagerFiles(_testDirectory.FullName);
    }

    private bool AssignDefaultDirectoryForTasks()
    {
        DirectoryInfo projectDirectory = new DirectoryInfo( _statusProject.SourcePath );

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
            _testDirectory = new DirectoryInfo(path);

            return false;
        }
    }

    public IEnumerable<OzonCurrentTask> GetTasks()
    {
        OzonCurrentTask[] tasks = _managerFiles.ReadyTasks;

        for (int i =  0; i < tasks.Length; i++)
        {
            yield return tasks[i];
        }
    }

    public void Dispose()
    {
        OzonCurrentTask[] tasks = _managerFiles.ReadyTasks;

        List<TaskReport> reports = new List<TaskReport>();

        for (int i = 0; i < tasks.Length; i++)
        {
            OzonCurrentTask task = tasks[i];

            

            foreach (DataResult resultTest in task.yourResult.Results)
            {
                DataResult result = task.Results.First(x => x.result.Key == resultTest.result.Key);


            }
        }

        GC.SuppressFinalize(this);
    }
    
    ~OzonTasks()
    {

    }

    private class StatusProject
    {
        private const string NameHistoryPaths = "OzonTestsManager_history_status.json";

        private readonly string? _sourcePath;

        public string? SourcePath { get {return _sourcePath; }}

        public StatusProject(string pathEnvironment, string testDirectoryName)
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

            if (!File.Exists(_sourcePath + NameHistoryPaths))
            {
                FileStream fs = new FileStream(_sourcePath + NameHistoryPaths, FileMode.Create);
                List<string> testDirectoriPathList = new List<string>() 
                {
                    _sourcePath + testDirectoryName
                };

                JsonSerializer.Serialize <List<string>> (fs, testDirectoriPathList);

                fs.Close();
            }
        }

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

        public void AddPath(string pathToTestDirectory)
        {
            using (FileStream fs = new FileStream( _sourcePath + NameHistoryPaths, FileMode.Open, FileAccess.ReadWrite ))
            {
                List<string>? listHistory = JsonSerializer.Deserialize <List<string>> (fs);

                if (listHistory.Contains(pathToTestDirectory))
                    return;

                listHistory.Add(pathToTestDirectory);

                fs.SetLength(0);

                JsonSerializer.Serialize <List<string>> (fs, listHistory);

                fs.Close();
            }
        }

        public void ParseFileWithHistoryOfSourcePaths()
        {
            
        }
    }
}