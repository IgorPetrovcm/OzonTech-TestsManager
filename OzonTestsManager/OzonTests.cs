namespace OzonTestsManager;



public class OzonTests
{
    private DirectoryInfo? _sourceDirectory;

    public OzonTests()
    {
    }

    public void AssignCurrentDirectoryWithTests(string path)
    {
        DirectoryInfo newSourceDirectory = new DirectoryInfo(path);

        FileInfo[] filesInNewDirectory = newSourceDirectory.GetFiles();

        for (int i = 0; i < filesInNewDirectory.Length; i++)
        {
            if (filesInNewDirectory[i].Name.EndsWith("."))
        }
    }
}