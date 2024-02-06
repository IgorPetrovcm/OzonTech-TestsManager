namespace OzonTestsManager;

using OzonTestsManager.Entities;


public class OzonCurrentTest
{
	private DataTask? _task;

	private IList<DataTaskResult>? _result;

	public DataTask? Task {get {return _task;} }

	public IEnumerable<DataTaskResult>? Result {get {return _result;} }

	public int TaskCount {get {
		if (_task == null)
			return 0;
		else 
			return _task.count;
	}}

	public string[] ArrayTasks {get {
		return _task.lines.ToArray();
	}}


	public OzonCurrentTest()
	{

	}
	public OzonCurrentTest(DataTask task)
	{
		_task = task;
	}
	public OzonCurrentTest(IList<DataTaskResult> result)
	{
		_result = result;
	}
	public OzonCurrentTest(DataTask task, IList<DataTaskResult> result)
	{
		_task = task;
		_result = result;
	}


	public void UploadTask(DataTask task)
	{
		_task = task;
	}


	public void UploadTaskResult(IList<DataTaskResult> result)
	{
		_result = result;
	}
}
