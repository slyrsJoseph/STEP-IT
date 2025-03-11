using GameStoreProject.Services.Interfaces;
using GameStoreProject.Data.Models;
namespace GameStoreProject.Services.Classes;
using AutoMapper;
using GameStoreProject.DTO.Responses;
using GameStoreProject.Data.Repository;
using GameStoreProject.Services.Interfaces;
using GameStoreProject.Services.Classes;
using Microsoft.EntityFrameworkCore;

public class ClientService : IClientService
{
     private readonly IUnitOfWork _clientRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ClientService> _logger;
    
    public ClientService(IUnitOfWork clientRepository, IMapper mapper, ILogger<ClientService> logger)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task CreateClient(CreateClientRequest request)
    {
        _logger.LogInformation("Creating a new client: {@Request}", request);
        Client client = _mapper.Map<Client>(request);
        await _clientRepository.ClientRep.AddAsync(client);
        await _clientRepository.SaveChangesAsync();
    }

    public async Task ChangeDataClient(ChangeDataClientRequest request)
    {
        _logger.LogInformation("Client data change: {@Request}", request);
        Client client = await _clientRepository.ClientRep.GetClientByEmail(request.oldEmail);
        if (client == null)
        {
            throw new KeyNotFoundException($"Client with email {request.oldEmail} not found");
        }
        client = _mapper.Map(request, client);
        _clientRepository.ClientRep.Update(client);
        await _clientRepository.SaveChangesAsync();
    }
    
    public async Task DeleteClient(DeleteClientRequest request)
    {
        _logger.LogInformation("Removing client: {@Request}", request);
        Client client = await _clientRepository.ClientRep.GetClientByEmail(request.email);
        if (client == null)
        {
            throw new KeyNotFoundException($"Client with email {request.email} not found");
        }
        _clientRepository.ClientRep.Delete(client);
        await _clientRepository.SaveChangesAsync();
    }

    public async Task<Client> FindClient(FindClientRequest request)
    {
        _logger.LogInformation("Client search: {@Request}", request);
        Client? client = await _clientRepository.ClientRep.GetClientByEmail(request.email);
        if (client == null)
        {
            _logger.LogWarning("Client with email {Email} has not found", request.email);
            throw new KeyNotFoundException($"Client with email {request.email} not found");
        }
        _logger.LogInformation("Client has found: {ClientId}", client.Id);
        return client;
    }

    public async Task<GetAllClientsResponse> GetAllClients()
    {
        var clients = await _clientRepository.ClientRep.GetLowInfoClientsList();
        return new GetAllClientsResponse
        {
            Clients = _mapper.Map<List<Client>>(clients)
        };
    }
    
    
}