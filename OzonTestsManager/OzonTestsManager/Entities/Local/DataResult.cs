namespace OzonTestsManager.Entities;


public class DataResult
{
	public KeyValuePair<int, string> result;

	public DataResult() {}

	public DataResult(int key, string value)
	{
		result = new KeyValuePair<int, string>(key,value.ToLower());
	}
}