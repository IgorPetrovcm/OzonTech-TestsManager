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


	public OzonCurrentTask()
	{
	}
	public OzonCurrentTask(IList<DataTest> tests, IList<DataResult> results)
	{
		_tests = tests;
		_results = results;
	}

	public void UploadResults(string[] lines)
	{
		_results = new List<DataResult>( new DataResult[lines.Length] );

		for (int i = 0; i < lines.Length; i++)
		{
			_results.Add(new DataResult(i + 1, lines[i]));
		}
	}

	public void UploadTests(string[] lines)
	{
		_tests = new List<DataTest>( new DataTest[int.Parse(lines[0])] );

		for (int i = 1; i < lines.Length; i++)
		{
			_tests.Add(new DataTest(lines[i]));
		}
	}

}
