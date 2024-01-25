namespace OzonTestsManager;

using OzonTestsManager.Entities;
using OzonTestsManager.Structures;


public static class ExtensionMethodsForCurrentTest
{
    public static void UploadTask(this OzonCurrentTest test, string[] lines)
    {
        string[] tasks = new string[lines.Length - 1];

        Array.Copy(lines, 1, tasks, 0, tasks.Length - 1);

        test.UploadTask(new DataTask(int.Parse(lines[0]), tasks));
    }

    public static void UploadTaskResult(this OzonCurrentTest test, string[] lines)
    {
        List<DataTaskResult> result = new List<DataTaskResult>();

        for (int i = 0; i < lines.Length; i++)
        {
            result.Add(new DataTaskResult(i + 1, lines[i]));
        }

        test.UploadTaskResult(result);
    }

    public static void TestChecking(this OzonCurrentTest test, IList<DataTaskResult> yourResult)
    {
        foreach (DataTaskResult item in yourResult)
        {
            DataTaskResult taskResult = test.Result.First(x => x.result.Key == item.result.Key);

            List<UnitErrorReporting> errorReport = new List<UnitErrorReporting>();

            if (item.result.Value != taskResult.result.Value)
            {
                UnitErrorReporting unitReport = OzonTools.CreateUnitErrorRepotring(item.result.Key, taskResult.result.Value, item.result.Value);

                errorReport.Add(unitReport);
            }
        }
    }
}