namespace CRM.Domain.Entities
{
    public class CampaignTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Projects> Projects { get; set; }
    }

}
