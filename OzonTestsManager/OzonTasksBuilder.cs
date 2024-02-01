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

            _task = new DataTask(tasks);
        }
        else 
        {
            string[] tasks = new string[lines.Length - 1];

            Array.Copy(lines, 1, tasks, 0, lines.Length - 1);

            _task = new DataTask(int.Parse(lines[0]), tasks);
        }
    }

    public void SetResult(string[] lines)
    {
        _result = new List<DataTaskResult>();
        
        for (int i = 0; i < lines.Length; i++)
        {
            _result.Add(new DataTaskResult(i + 1, lines[i]));
        }
    }

    public OzonCurrentTest Build()
    {
        return new OzonCurrentTest(_task ?? new DataTask(), _result ?? new List<DataTaskResult>());
    }

}