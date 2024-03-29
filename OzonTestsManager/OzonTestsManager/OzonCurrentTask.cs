﻿namespace OzonTestsManager;

using OzonTestsManager.Entities;


public class OzonCurrentTask
{
	private string? _name;
	private int _countTests;
	private IList<DataTest>? _tests;

	private IList<DataResult>? _results;

	public YourTestResult yourResult;

	public string? Name {get { return _name; }}

	public IEnumerable<DataTest>? Tests {get {return _tests;} }

	public IEnumerable<DataResult>? Results {get {return _results;} }

	public int TestsCount {get {
		return _countTests;
	}}


	public OzonCurrentTask()
	{
	}
	public OzonCurrentTask(string name)
	{
		_name = name;
		yourResult = new YourTestResult();
	}
	public OzonCurrentTask(IList<DataTest> tests, IList<DataResult> results)
	{
		_tests = tests;
		_results = results;
	}
	public OzonCurrentTask(IList<DataTest> tests, IList<DataResult> results, string name)
	{
		_tests = tests;
		_results = results;
		_name = name;
	}


	public void UploadResults(string[] lines)
	{
		_results = new List<DataResult>();

		for (int i = 0; i < lines.Length; i++)
		{
			lines[i] = lines[i].Trim();
			
			_results.Add(new DataResult(i + 1, lines[i]));
		}
	}

	public void UploadTests(string[] lines)
	{
		_countTests = int.Parse(lines[0]);

		_tests = new List<DataTest>();

		for (int i = 1; i < lines.Length; i++)
		{
			_tests.Add(new DataTest(lines[i]));
		}
	}

	public IEnumerator<string> GetEnumerator()
	{
		DataTest[] tests = _tests.ToArray();

		for (int i = 0; i < tests.Length; i++)
		{
			yield return tests[i].line;
		}
	}
}
