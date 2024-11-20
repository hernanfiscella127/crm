namespace CRM.Aplication.Response
{
    public class TasksResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public GenericResponse Status { get; set; }
        public UsersResponse UserAssigned { get; set; }
    }

}
