namespace OzonTestsManager;

using OzonTestsManager.Entities;


public class YourTestResult
{
    private IList<DataResult>? _results;
    private int _currentKey = 1;

    private char _separator = ' ';

    public IList<DataResult>? Results {get {return _results; }}
    public int CurrentKey {get {return _currentKey; }}


    public YourTestResult()
    {
        _results = new List<DataResult>();
    }


    public void Add(string result)
    {
        _results.Add(new DataResult(_currentKey, result));

        _currentKey++;
    }

    public void Add<T>(IEnumerable<T> result)
    {
        _results.Add( new DataResult(_currentKey, 
                                            string.Join( _separator, result.Select(x => x.ToString()).ToArray() )) );
        
        _currentKey++;

        _separator = ' ';
    }

    public void Add<T>(IEnumerable<T> result, char separator)
    {
        _separator = separator;

        Add<T>(result);
    }
}