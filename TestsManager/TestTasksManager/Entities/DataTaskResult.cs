namespace TestTasksManager.Entities;


public class DataTaskResult
{
	public KeyValuePair<int, string> result;

	public DataTaskResult(int key, string value)
	{
		result = new KeyValuePair<int, string>(key,value);
	}
}