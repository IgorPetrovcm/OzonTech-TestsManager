namespace OzonTestsManager;

using System.Net;
using OzonTestsManager.Exception;


public enum OzonDeliveryFileMode
{
    Update,
}
public class OzonAllManager
{
    private OzonTestsDelivery? _delivery;

    private readonly string? _sourcePath = "tasksArchive";

    

    private bool stateArchive = false;

    private delegate bool IsSourceDirectoryEmply(string path);
    private IsSourceDirectoryEmply isDirEmpty = (string path) => 
    {
        if (Directory.Exists(path))
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            if (directory.GetFiles().Length > 0)
            {
                return false;
            }
            if (directory.GetDirectories().Length > 0)
            {
                return false;
            }
        }   

        return true;
    } ;


    public OzonAllManager() 
    {
        if (Directory.Exists(_sourcePath))
        {
            if (!isDirEmpty(_sourcePath))
            {
                throw new Exception("")
            }
        }
    }

    public OzonAllManager(string sourcePath)
    {
        if (isDirEmpty(sourcePath))
        _sourcePath = sourcePath;
    }

    public OzonAllManager(OzonDeliveryFileMode mode)
    {
        if (Directory)
    }

    public OzonAllManager(string sourcePath)
    {
        _sourcePath = sourcePath;
    }

    public void AddArchive(Uri uriToTest) 
    {
        _delivery = new OzonTestsDelivery(uriToTest);

        try
        {
            FileStream fileStream = new FileStream(_sourcePath, FileMode.)
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }
}