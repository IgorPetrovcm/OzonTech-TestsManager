namespace OzonTestsManager.Files;

using System.Reflection;
using System.Text.RegularExpressions;

public class ManagerFiles
{
    private readonly Regex SearchFilesRegex = new Regex(@"^(\d+)(\.\w)?");

    private Dictionary<string,OzonCurrentTask>? _ozonTasks; 

    public bool IsOneTaskReady { 
        get 
        {
            if (TasksCount > 0)
            {
                foreach (OzonCurrentTask currentTask in _ozonTasks.Values)
                {
                    if (currentTask.Result != null && currentTask.Task != null)
                        return true;
                }

                return false;
            }
            return false;
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

    public ManagerFiles(string sourcePath)
    {
        _ozonTasks = new Dictionary<string, OzonCurrentTask>();

        DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);

        string currentID = "";

        foreach (FileInfo file in sourceDirectory.GetFiles())
        {
            if (SearchFilesRegex.IsMatch(file.Name))
            {
                Match match = SearchFilesRegex.Match(file.Name);

                string[] fileLines = File.ReadAllLines(file.FullName);

                if (!_ozonTasks.ContainsKey(match.Groups[1].Value))
                {
                    _ozonTasks.Add(match.Groups[1].Value, new OzonCurrentTask());

                    UpdateCurrentTask(match.Groups[1].Value, fileLines, match.Groups[2].Success);
                }
                else
                {
                    UpdateCurrentTask(match.Groups[1].Value, fileLines, match.Groups[2].Success);
                }
            }
        }
    }
}