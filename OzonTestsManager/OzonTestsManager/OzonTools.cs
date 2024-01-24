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
} 