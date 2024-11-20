namespace CRM.Domain.Entities
{
    public class Projects
    {
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int CampaignType { get; set; }
        public int ClientID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public CampaignTypes CampaignTypeNavigation { get; set; }
        public Clients ClientRelation { get; set; }
        public List<Tasks> Tasks { get; set; }
        public List<Interactions> Interactions { get; set; }
    }
}
