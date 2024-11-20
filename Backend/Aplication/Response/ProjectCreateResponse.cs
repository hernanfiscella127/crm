namespace CRM.Aplication.Response
{
    public class ProjectCreateResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ClientResponse Client { get; set; }
        public GenericResponse CampaignType { get; set; }
    }
}
