namespace CRM.Aplication.Response
{
    public class InteractionsResponse
    {
        public Guid Id { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public Guid ProjectId { get; set; }
        public GenericResponse InteractionType { get; set; }
    }
}
