using Movie.Core.Dtos;

namespace Movie.Contracts.Services;

public interface IActorService
{
    Task<IEnumerable<ActorDto>> GetAllAsync();
    Task<ActorDto?> GetByIdAsync(int id);
    Task<ActorDto> CreateAsync(CreateActorDto dto);
    Task<bool> UpdateAsync(int id, CreateActorDto dto);
    Task<bool> DeleteAsync(int id);
}