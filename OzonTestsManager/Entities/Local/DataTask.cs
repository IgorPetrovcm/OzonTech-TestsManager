namespace OzonTestsManager.Entities;


public class DataTask
{
    public int count;

    public IEnumerable<string>? lines;

    public DataTask() {}

    public DataTask(IEnumerable<string> lines)
    {
        this.lines = lines;
    }

    public DataTask(int count, IEnumerable<string> lines)
    {
        this.lines = lines;

        this.count = count;
    }
}
