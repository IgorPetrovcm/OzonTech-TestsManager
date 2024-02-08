namespace OzonTestsManager;

using OzonTestsManager.Entities;


public class OzonCurrentTask
{
	private int _countTests;
	private IList<DataTest>? _tests;

	private IList<DataResult>? _results;

	private YourTestResult _yourResult;

	public IEnumerable<DataTest>? Task {get {return _tests;} }

	public IEnumerable<DataResult>? Result {get {return _results;} }

	public int TestsCount {get {
		return _countTests;
	}}


	public OzonCurrentTest()
	{
	}
	public OzonCurrentTest(IList<DataTest> tests, IList<DataResult> results)
	{
		_tests = tests;
		_results = results;
	}

}
