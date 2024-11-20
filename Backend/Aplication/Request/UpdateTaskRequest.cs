namespace CRM.Aplication.Request
{
    public class UpdateTaskRequest
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
    }
}
