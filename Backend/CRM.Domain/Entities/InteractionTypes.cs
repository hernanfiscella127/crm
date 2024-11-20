namespace CRM.Domain.Entities
{
    public class InteractionTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Interactions> Interactions { get; set; }
    }
}
