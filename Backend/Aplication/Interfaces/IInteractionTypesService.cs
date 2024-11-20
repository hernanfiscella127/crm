using CRM.Aplication.Response;

namespace CRM.Aplication.Interfaces
{
    public interface IInteractionTypesService
    {
        public Task<List<GenericResponse>> ObteinAllInteractionTypes();

    }
}
