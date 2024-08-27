using DomainLayer.Models;

namespace RepositoryLayer.IRepository
{
    public interface IProviderPhotoRepository
    {
        IEnumerable<ProviderPhoto> GetAll();
        ProviderPhoto Get(int Id);
        void Insert(ProviderPhoto providerPhoto);
        void Update(ProviderPhoto providerPhoto);
        void Delete(ProviderPhoto providerPhoto);
        void Remove(ProviderPhoto providerPhoto);
        void SaveChanges();
    }
}
