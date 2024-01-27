namespace OzonTestsManager;

using OzonTestsManager.Entities;
using OzonTestsManager.Structures;
using OzonTestsManager.Report;


public static class ExtensionMethodsForCurrentTest
{
    public static void UploadTask(this OzonCurrentTest test, string[] lines)
    {
        string[] tasks = new string[lines.Length - 1];

        Array.Copy(lines, 1, tasks, 0, lines.Length - 1);

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

    public static string TestChecking(this OzonCurrentTest test, IList<DataTaskResult> yourResult)
    {
        TestReport report = new TestReport();

        foreach (DataTaskResult item in yourResult)
        {
            DataTaskResult taskResult = test.Result.First(x => x.result.Key == item.result.Key);

            if (item.result.Value != taskResult.result.Value)
            {
                UnitErrorReporting unitReport = OzonTools.CreateUnitErrorRepotring(item.result.Key, taskResult.result.Value, item.result.Value);

                report.AddError(unitReport);
            }
        }

        return report.ToString();
    }
}