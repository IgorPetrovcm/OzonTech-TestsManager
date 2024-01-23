namespace TestTasksManager;

using System.IO;

public static class OzonData
{ 
	public static DataTasks UploadTask(string[] lines)
	{
		string[] taskLines = new string[lines.Length - 1];

		Array.Copy(lines, 1, taskLines, 0, taskLines.Length);

		return new DataTasks(int.Parse(lines[0]), taskLines);
	}

	public static 
}
