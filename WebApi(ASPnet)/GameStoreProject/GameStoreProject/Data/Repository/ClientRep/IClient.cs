using GameStoreProject.Data.Repository.MainRep;

namespace GameStoreProject.Data.Repository.ClientRep;

using GameStoreProject.Data.Models;

public interface IClient : IRepository<User>
{
    Task<User> GetClientByEmail(string email);
    Task<User> GetClientByName(string name);
    Task<List<User>> GetLowInfoClients();
}