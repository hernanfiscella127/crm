namespace CRM.Aplication.Response
{
    public class ProjectsWithDetailsResponse
    {
        public ProjectCreateResponse Data { get; set; }
        public List<InteractionsResponse> Interactions { get; set; }
        public List<TasksResponse> Tasks { get; set; }
    }
}
