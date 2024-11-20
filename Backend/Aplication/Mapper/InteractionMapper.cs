using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper
{
    public class InteractionMapper : IInteractionMapper
    {
        private readonly IInteractionTypesMapper _interactionMapper;

        public InteractionMapper(IInteractionTypesMapper interactionMapper)
        {
            _interactionMapper = interactionMapper;
        }

        public async Task<List<InteractionsResponse>> GetAllInteractionResponseMapper(List<Interactions> allInteractions)
        {
            List<InteractionsResponse> list = new List<InteractionsResponse>();
            foreach (var interaction in allInteractions)
            {
                var response = new InteractionsResponse
                {
                    Id = interaction.InteractionID,
                    Date = interaction.Date,
                    Notes = interaction.Notes,
                    ProjectId = interaction.ProjectID,
                    InteractionType = await _interactionMapper.GetInteractionTypeResponseMapper(interaction.InteractionTypes)
                };
                list.Add(response);
            }
            return list;
        }

        public async Task<InteractionsResponse> GetInteractionResponseMapper(Interactions interaction)
        {
            var response = new InteractionsResponse
            {
                Id = interaction.InteractionID,
                Date = interaction.Date,
                Notes = interaction.Notes,
                InteractionType = await _interactionMapper.GetInteractionTypeResponseMapper(interaction.InteractionTypes)
            };
            return response;

        }
    }
}
