namespace TestTasksManager
{
	public class DataTasks
	{
		private int _count;

		private IEnumerable<string>? _lines;

		public int Count { get { return _count; } }

		public IEnumerable<string>? Lines { get { return _lines; } }

		public DataTasks(int count,  IEnumerable<string> lines)
		{
			_lines = lines;

			_count = count;
		}
	}
}
