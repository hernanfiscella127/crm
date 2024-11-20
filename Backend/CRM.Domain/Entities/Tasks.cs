namespace CRM.Domain.Entities
{
    public class Tasks
    {
        public Guid TaskID { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ProjectID { get; set; }
        public int AssignedTo { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public Projects Project { get; set; }
        public Users User { get; set; }
        public TaskStatus TaskStatus { get; set; }
    }
}
