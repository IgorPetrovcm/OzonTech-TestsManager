namespace OzonTestsManager;

using System.Net;
using OzonTestsManager.Exception;
using System;


public enum OzonDeliveryFileMode
{
    Update,
    Add,
    Recreate
}


public class OzonAllManager
{
    private OzonTestsDelivery? _delivery;

    private string? _sourcePath = "tasksArchive";

    private ExceptionDefaultParams? currentException; 

    private bool stateArchive = false;


    public OzonAllManager() 
    {
        BaseDirectory("You use constructs without setting parameters" +
                "(by default, the class tries to save tasks in the 'tasksArchive' directory)." + 
                " The 'tasksArchive' directory must be empty, or you are using a constructor with 'OzonDeliveryFileMode'",
                _sourcePath);
    }
    
    public OzonAllManager(string sourcePath)
    {
        BaseDirectory("You are trying to base an archive with tasks in a folder with other files", sourcePath);
    }

    public OzonAllManager(OzonDeliveryFileMode mode, Uri uriToTask)
    {
        if (mode == OzonDeliveryFileMode.Add)
        {
            BaseDirectory("You are using constructs with the 'Add' file mode, and using the default source path. " + 
                    "The 'tasksArchive' directory should be empty", _sourcePath);

            _delivery = new OzonTestsDelivery(uriToTask);

            HttpContent content = _delivery.GetTestArchive();

            try 
            {
                FileStream fs = new FileStream(_sourcePath, FileMode.Create, FileAccess.Write);

                
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }

    private static bool IsDirectoryEmpty(string path) 
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
    }

    private void BaseDirectory(string exceptionBody, string sourcePath)
    {
        if (Directory.Exists(sourcePath))
        {
            if (!IsDirectoryEmpty(sourcePath))
            {
                throw new System.Exception(exceptionBody);
            }
            return;
        }

        _sourcePath = sourcePath;
        Directory.CreateDirectory(_sourcePath);
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