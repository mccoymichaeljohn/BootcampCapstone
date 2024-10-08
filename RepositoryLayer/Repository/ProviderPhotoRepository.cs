﻿using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.IRepository;
namespace RepositoryLayer.Repository
{
    public class ProviderPhotoRepository : IProviderPhotoRepository
    {
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
        public async Task Delete(ProviderPhoto photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException("photo");
            }
            providerPhotos.Remove(photo);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task<ProviderPhoto> Get(int Id)
        {
            return await providerPhotos.SingleOrDefaultAsync(c => c.Id == Id);
        }
        public async Task<IEnumerable<ProviderPhoto>> GetAll()
        {
            return await providerPhotos.ToListAsync();
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
        public async Task Remove(ProviderPhoto photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException("photo");
            }
            providerPhotos.Remove(photo);
        }
        public async Task SaveChanges()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task Update(ProviderPhoto photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException("photo");
            }
            providerPhotos.Update(photo);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}