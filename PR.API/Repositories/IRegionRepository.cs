using PR.API.Models.Domain;

namespace PR.API.Repositories
{
    public interface IRegionRepository
    {
      Task<List<Region>> GetAllAsync();  
    }
}
