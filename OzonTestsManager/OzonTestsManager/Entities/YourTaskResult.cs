namespace OzonTestsManager;

using OzonTestsManager.Entities;


public class YourTaskResult
{
    private IList<DataTaskResult>? _results;

    private int _currentKey = 1;

    public IList<DataTaskResult>? Results {get {return _results; }}

    public int CurrentKey {get {return _currentKey; }}

    public YourTaskResult()
    {
        _results = new List<DataTaskResult>();
    }
    public void Add(string result)
    {
        _results.Add(new DataTaskResult(_currentKey, result));

        _currentKey++;
    }
}