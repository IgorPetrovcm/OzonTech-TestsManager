namespace TestTasksManager;

using TestTasksManager.Entities;


public class GeneralOzonTest
{
	private DataTask? _task;

	private IEnumerable<DataTaskResult>? _result;

	public DataTask? Task {get {return _task;} }

	public IEnumerable<DataTaskResult>? Result {get {return _result;} }


	public GeneralOzonTest(DataTask task, IEnumerable<DataTaskResult> result)
	{
		_task = task;
		_result = result;
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
	}
}
