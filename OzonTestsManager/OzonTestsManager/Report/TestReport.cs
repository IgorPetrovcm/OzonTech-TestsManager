namespace OzonTestsManager.Report;

using OzonTestsManager.Structures;
using System.Text;


public class TestReport : IReport
{
    private string _reportTitle = 
    "Test Report\n__________________________________________________";

    public string ReportTitle {get {return _reportTitle; }}

    private IList<UnitErrorReporting> _errors;

    private int _testCount;

    public int TestCount {get {return _testCount; }}

    public IList<UnitErrorReporting> Errors {get {return _errors; }}

    public TestReport()
    {

    }
    public TestReport(int testCount)
    {
        _testCount = testCount;
    }
    public TestReport(int testCount, IList<UnitErrorReporting> errors)
    {
        _testCount = testCount;
        _errors = errors;
    }

    public void AddError(UnitErrorReporting error)
    {
        error.false_result = error.false_result ?? "Not found";

        _errors.Add(error);
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        result.Append(_reportTitle);
        
        foreach (UnitErrorReporting error in _errors)
        {
            result.Append($"\tline {error.line} - true result: {error.true_result}; your result: {error.false_result}\n");
        }

        return result.ToString();
    }
}