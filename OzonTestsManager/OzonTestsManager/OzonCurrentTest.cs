namespace OzonTestsManager;

using OzonTestsManager.Entities;


public class OzonCurrentTest
{
	private DataTask? _task;

	private IEnumerable<DataTaskResult>? _result;

	public DataTask? Task {get {return _task;} }

	public IEnumerable<DataTaskResult>? Result {get {return _result;} }


	public OzonCurrentTest()
	{

	}
	public OzonCurrentTest(DataTask task)
	{
		_task = task;
	}
	public OzonCurrentTest(IEnumerable<DataTaskResult> result)
	{
		_result = result;
	}
	public OzonCurrentTest(DataTask task, List<DataTaskResult> result)
	{
		_task = task;
		_result = result;
	}


	public void UploadTask(DataTask task)
	{
		_task = task;
	}
	public void UploadTask(string[] lines)
	{
		string[] tasks = new string[lines.Length - 1];

		Array.Copy(lines, 1, tasks, 0, tasks.Length);

		_task = new DataTask(int.Parse(lines[0]), tasks);
	}


	public void UploadTaskResult(string[] lines)
	{
		_result = new List<DataTaskResult>() ?? null;

		List<DataTaskResult> newResult = new List<DataTaskResult>();

		for (int i = 0; i < lines.Length; i++)
		{
			newResult.Add(new DataTaskResult(i + 1, lines[i]));
		}

		_result = newResult;
	}
}
