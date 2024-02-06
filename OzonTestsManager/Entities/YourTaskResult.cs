namespace OzonTestsManager;

using OzonTestsManager.Entities;


public class YourTaskResult
{
    private IList<DataTaskResult>? _results;
    private int _currentKey = 1;

    public IList<DataTaskResult>? Results {get {return _results; }}
    public int CurrentKey {get {return _currentKey; }}

    private delegate char GetSeparator(); 
    private GetSeparator _getSeparator = () => ' ';


    public YourTaskResult()
    {
        _results = new List<DataTaskResult>();
    }


    public void Add(string result)
    {
        _results.Add(new DataTaskResult(_currentKey, result));

        _currentKey++;
    }

    public void Add<T>(IEnumerable<T> result)
    {
        Results.Add( new DataTaskResult(_currentKey, 
                                            string.Join( _getSeparator() , result.Select(x => x.ToString()).ToArray() )) );
        
        _currentKey++;

        _getSeparator = () => ' ';
    }

    public void Add<T>(IEnumerable<T> result, char separator)
    {
        _getSeparator = () => separator;

        Add<T>(result);
    }
}