namespace OzonTestsManager;

using OzonTestsManager.Entities;


public enum TaskSettings
{
    // *Заголовок - первая строка всех task от ozon 
    // 
    // IgnoreTitle - игнорировать заголовок в качестве параметра 'count' для класса DataTask 
    //
    // WithTitle - воспринимать заговок в качестве параметра 'count' Для класса DataTask
    IgnoreTitle, 
    WithTitle,
}

public class OzonTasksBuilder
{
    private DataTask? _task;

    private IList<DataTaskResult>? _result;


    public void SetTask(TaskSettings settings, string[] lines)
    {
        if (settings == TaskSettings.IgnoreTitle)
        {
            string[] tasks = new string[lines.Length];

            Array.Copy(lines, 0, tasks, 0, tasks.Length);

            _task.lines = tasks;
        }
        else 
        {
            string[] tasks = new string[lines.Length - 1];

            Array.Copy(lines, 1, tasks, 0, lines.Length - 1);

            _task.lines = tasks;

            _task.count = int.Parse(lines[0]);
        }
    }

    public OzonCurrentTest Build()
    {
        return new OzonCurrentTest(_task, _result);
    }

}