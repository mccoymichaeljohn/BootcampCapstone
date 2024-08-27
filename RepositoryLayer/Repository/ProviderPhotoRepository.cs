using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.IRepository;
namespace RepositoryLayer.Repository
{
    public class ProviderPhotoRepository : IProviderPhotoRepository { 
        #region property
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<ProviderPhoto> providerPhotos;
        #endregion
        #region Constructor
        public ProviderPhotoRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            providerPhotos = _applicationDbContext.Set<ProviderPhoto>();
        }
        #endregion
        public void Delete(ProviderPhoto photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException("photo");
            }
            providerPhotos.Remove(photo);
            _applicationDbContext.SaveChanges();
        }
        public ProviderPhoto Get(int Id)
        {
            return providerPhotos.SingleOrDefault(c => c.Id == Id);
        }
        public IEnumerable<ProviderPhoto> GetAll()
        {
            return providerPhotos.AsEnumerable();
        }
        public async Task Insert(ProviderPhoto photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException("photo");
            }
            providerPhotos.Add(photo);
            await _applicationDbContext.SaveChangesAsync();
        }
        public void Remove(ProviderPhoto photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException("photo");
            }
            providerPhotos.Remove(photo);
        }
        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }
        public void Update(ProviderPhoto photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException("photo");
            }
            providerPhotos.Update(photo);
            _applicationDbContext.SaveChanges();
        }
    }
}
