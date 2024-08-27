using DomainLayer.Models;

namespace RepositoryLayer.IRepository
{
    public interface IProviderPhotoRepository
    {
        IEnumerable<ProviderPhoto> GetAll();
        ProviderPhoto Get(int Id);
        Task Insert(ProviderPhoto providerPhoto);
        Task Update(ProviderPhoto providerPhoto);
        Task Delete(ProviderPhoto providerPhoto);
        Task Remove(ProviderPhoto providerPhoto);
        Task SaveChanges();
    }
}
