using CRM.Aplication.Interfaces;
using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Response;

namespace CRM.Aplication.UseCases
{
    public class InteractionTypesService : IInteractionTypesService
    {
        //private readonly IInteractionTypesCommand _command;
        private readonly IInteractionTypesQuery _query;

        private readonly IInteractionTypesMapper _interactionTypesMapper;

        public InteractionTypesService(IInteractionTypesQuery query, IInteractionTypesMapper interactionTypesMapper)
        {
            // _command = command;
            _query = query;
            _interactionTypesMapper = interactionTypesMapper;
        }


        public async Task<List<GenericResponse>> ObteinAllInteractionTypes()
        {
            var allInteractionTypes = await _query.GetAllInterationTypes();

            return await _interactionTypesMapper.GetAllIInteractionTypesResponseMapper(allInteractionTypes);

        }
    }
}