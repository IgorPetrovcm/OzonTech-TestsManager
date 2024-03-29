namespace OzonTestsManager.Files;

using System.Reflection;
using System.Text.RegularExpressions;

public class ManagerFiles
{
    private readonly Regex SearchFilesRegex = new Regex(@"^(\d+)(\.\w)?");

    private Dictionary<string, OzonCurrentTask>? _ozonTasks; 

    public Dictionary<string, OzonCurrentTask>? OzonTasks { get { return _ozonTasks; }}

    public bool IsOneTaskReady { 
        get 
        {
            if (TasksCount > 0)
            {
                foreach (OzonCurrentTask currentTask in _ozonTasks.Values)
                {
                    if (currentTask.Results != null && currentTask.Tests != null)
                        return true;
                }
            }
            return false;
        }   
    }

    public OzonCurrentTask[] ReadyTasks {
        get {
            return _ozonTasks.Where(x => x.Value.Results != null && x.Value.Tests != null).Select(x => x.Value).ToArray();
        }
    }

    public int TasksCount {get { return _ozonTasks != null ? _ozonTasks.Count : 0 ; }}

    private void UpdateCurrentTask(string key, string[] lines, bool isResult)
    {
        if (isResult)
        {
            _ozonTasks[key].UploadResults(lines);
        }
        else 
        {
            _ozonTasks[key].UploadTests(lines);
        }
    }

    public ManagerFiles()
    {
        _ozonTasks = new Dictionary<string, OzonCurrentTask>();
    }

    public ManagerFiles(string pathToTestDirectory)
    {
        _ozonTasks = new Dictionary<string, OzonCurrentTask>();

        DirectoryInfo sourceDirectory = new DirectoryInfo( pathToTestDirectory );

        foreach (FileInfo file in sourceDirectory.GetFiles())
        {
            if (SearchFilesRegex.IsMatch(file.Name))
            {
                Match match = SearchFilesRegex.Match(file.Name);

                string[] fileLines = File.ReadAllLines(file.FullName);

                if (!_ozonTasks.ContainsKey( match.Groups[1].Value) )
                {
                    _ozonTasks.Add( match.Groups[1].Value, new OzonCurrentTask(match.Groups[1].Value) );

                    UpdateCurrentTask( match.Groups[1].Value, fileLines, match.Groups[2].Success );
                }
                else
                {
                    UpdateCurrentTask( match.Groups[1].Value, fileLines, match.Groups[2].Success );
                }
            }
        }
    }

    public static bool IsFileValid(string trueExtension, string pathToFile)
    {
        if (!File.Exists(pathToFile))
        {
            return false;
        }

        if (!pathToFile.EndsWith(trueExtension))
        {
            return false;
        }
        
        return true;
    }
}