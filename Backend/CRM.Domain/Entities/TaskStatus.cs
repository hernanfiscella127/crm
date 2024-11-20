namespace CRM.Domain.Entities
{
    public class TaskStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
    }
}
