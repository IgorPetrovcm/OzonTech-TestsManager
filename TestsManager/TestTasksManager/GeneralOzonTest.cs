namespace TestTasksManager;

using TestTasksManager.Entities;


public class GeneralOzonTest
{
	private DataTask _task;

	private DataTaskResult _result;

	public GeneralOzonTest(DataTask task, DataTaskResult result)
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
}
