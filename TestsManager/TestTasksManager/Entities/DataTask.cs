namespace TestTasksManager.Entities
{
    public class DataTask
    {
        public int count;

        public IEnumerable<string>? lines;

        public DataTask(int count, IEnumerable<string> lines)
        {
            lines = lines;

            count = count;
        }
    }
}
