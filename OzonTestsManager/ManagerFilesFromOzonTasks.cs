namespace OzonTestsManager.Files;

using System.Text.RegularExpressions;

public class ManagerFiles
{
    private readonly Regex SearchFilesRegex = new Regex(@"^(\d+)(\.\w)?");

    private IList<OzonCurrentTask>? _ozonTasks; 

    public ManagerFiles()
    {
        _ozonTasks = new List<OzonCurrentTask>();
    }

    public ManagerFiles(string sourcePath)
    {
        _ozonTasks = new List<OzonCurrentTask>();

        DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);

        foreach (FileInfo file in sourceDirectory.GetFiles())
        {
            if (SearchFilesRegex.IsMatch(file.Name))
            {
                Ozon
            }
        }
    }
}