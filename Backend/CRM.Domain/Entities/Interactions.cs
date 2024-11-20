namespace CRM.Domain.Entities
{
    public class Interactions
    {
        public Guid InteractionID { get; set; }

        public Guid ProjectID { get; set; }
        public Projects Project { get; set; }
        public int InteractionType { get; set; }
        public InteractionTypes InteractionTypes { get; set; }

        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}
