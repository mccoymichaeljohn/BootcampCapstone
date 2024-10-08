﻿using DomainLayer.Models;

namespace RepositoryLayer.IRepository
{
    public interface IProviderPhotoRepository
    {
        Task<IEnumerable<ProviderPhoto>> GetAll();
        Task<ProviderPhoto> Get(int Id);
        Task Insert(ProviderPhoto providerPhoto);
        Task Update(ProviderPhoto providerPhoto);
        Task Delete(ProviderPhoto providerPhoto);
        Task Remove(ProviderPhoto providerPhoto);
        Task SaveChanges();
    }
}
