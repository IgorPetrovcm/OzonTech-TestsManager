namespace TestTasksManager.Tools;

using TestTasksManager.Entities;


public static class OzonTasksTools
{
    public static DataTask UploadTask(string[] lines)
    {
        string[] taskLines = new string[lines.Length - 1];

        Array.Copy(lines, 1, taskLines, 0, taskLines.Length);

        return new DataTask(int.Parse(lines[0]), taskLines);
    }

    public static
}
