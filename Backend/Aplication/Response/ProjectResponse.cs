namespace CRM.Aplication.Response
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ClientResponse Client { get; set; }
        public GenericResponse CampaignType { get; set; }
    }
}
