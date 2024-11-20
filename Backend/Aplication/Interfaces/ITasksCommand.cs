namespace CRM.Aplication.Interfaces
{
    public interface ITasksCommand
    {
        public System.Threading.Tasks.Task UpdateTask(Domain.Entities.Tasks task);

    }
}
