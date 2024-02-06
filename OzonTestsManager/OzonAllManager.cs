namespace OzonTestsManager;

using OzonTestsManager.Exception;
using System;
using System.Text.RegularExpressions;


public class OzonAllManager
{
    private OzonTestsDelivery? _delivery;

    private string? _sourcePath = "tasksArchive";

    private bool stateArchive = false;


    public OzonAllManager() 
    {
        BaseDirectory("You use constructs without setting parameters" +
                "(by default, the class tries to save tasks in the 'tasksArchive' directory). The directory should be empty",
                _sourcePath);
    }
    
    public OzonAllManager(string sourcePath)
    {
        BaseDirectory("You are trying to base an archive with tasks in a folder with other files. ", sourcePath);
    }

    public OzonAllManager(Uri uriToTask)
    {

            BaseDirectory("You use constructs without setting parameters" +
                "(by default, the class tries to save tasks in the 'tasksArchive' directory). The directory should be empty",
                 _sourcePath);

            _delivery = new OzonTestsDelivery(uriToTask);

            KeyValuePair<string, HttpContent> content = _delivery.GetTestArchive();

            try 
            {
                FileStream fs = new FileStream(_sourcePath  + Regex.Match(content.Key, @"\d[1,4].zip$"), FileMode.Create, FileAccess.Write);

                content.Value.CopyToAsync(fs);

                fs.Close();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                stateArchive = true;
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
                throw new System.Exception(new ExceptionDefaultParams(exceptionBody).ReturnException());
            }
            return;
        }

        _sourcePath = sourcePath;
        Directory.CreateDirectory(_sourcePath);
    }

}