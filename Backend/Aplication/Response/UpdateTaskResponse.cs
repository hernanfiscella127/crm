namespace CRM.Aplication.Response
{
    public class UpdateTaskResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatusResponse Status { get; set; }
        public UsersResponse UserAssigned { get; set; }
    }
}
