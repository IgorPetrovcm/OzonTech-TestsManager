namespace OzonTestsManager.Report;

using OzonTestsManager.Structures;
using System.Text;


public class TaskReport 
{
    private string? _nameTask; 

    private IList<UnitErrorReporting>? _errors;

    private int _testCount;

    private const string ReportTitle = 
    $"Test Report\n-------------------------------------------------------------------------------------------------------\n";

    public int TestCount {get {return _testCount; }}

    public IList<UnitErrorReporting>? Errors {get {return _errors; }}

    public TaskReport(string nameTask)
    {
        _nameTask = nameTask;
    }
    public TaskReport(int testCount)
    {
        _testCount = testCount;
    }
    public TaskReport(int testCount, IList<UnitErrorReporting> errors)
    {
        _testCount = testCount;
        _errors = errors;
    }

    public void AddError(UnitErrorReporting error)
    {
        _errors = _errors ?? new List<UnitErrorReporting>();

        error.false_result = error.false_result ?? "Not found";

        _errors.Add(error);
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        result.Append("Task: " + _nameTask + "\n");

        result.Append(ReportTitle);

        if (_errors == null) 
            return result.ToString();
        
        foreach (UnitErrorReporting error in _errors)
        {
            result.Append($@"line {error.line} - true result: ""{error.true_result}""; your result: ""{error.false_result}""" + "\n");
        }

        if (_errors.Count == 0)
        {
            result.Append("Complete correct");
        }

        return result.ToString();
    }
}