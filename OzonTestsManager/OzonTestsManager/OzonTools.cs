namespace OzonTestsManager;

using OzonTestsManager.Entities;

public static class OzonTestTool
{
    public static OzonCurrentTest CreateWithTask(string[] lines)
    {
        string[] tasks = new string[lines.Length - 1];

        Array.Copy(lines, 1, tasks, 0, tasks.Length - 1);

        return new OzonCurrentTest(
                    new DataTask(int.Parse(lines[0]), tasks)
                    );
    }

    public static OzonCurrentTest CompleteCreation(string[] lines_task, string[] lines_result)
    {
        OzonCurrentTest currentTest = CreateWithTask(lines_task);

        List<DataTaskResult> result = new List<DataTaskResult>();

        for (int i = 0; i < lines_result.Length; i++)
        {
            result.Add(new DataTaskResult(i + 1, lines_result[i]));
        }

        currentTest.UploadTaskResult(result);

        return currentTest;
    }
} 