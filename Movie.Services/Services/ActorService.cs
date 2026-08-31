using Movie.Contracts.Services;
using Movie.Core.Contracts;
using Movie.Core.Dtos;
using Movie.Core.Entities;

namespace Movie.Services.Services;

public class ActorService : IActorService
{
    private readonly IUnitOfWork _unitOfWork;

    public ActorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ActorDto>> GetAllAsync()
    {
        var actors = await _unitOfWork.Actors.GetAllAsync();

        return actors.Select(MapToDto);
    }

    public async Task<ActorDto?> GetByIdAsync(int id)
    {
        var actor = await _unitOfWork.Actors.GetByIdAsync(id);

        return actor is null ? null : MapToDto(actor);
    }

    public async Task<ActorDto> CreateAsync(CreateActorDto dto)
    {
        var actor = new Actor
        {
            Name = dto.Name,
            BornYear = dto.BornYear,
            Biography = dto.Biography
        };

        await _unitOfWork.Actors.AddAsync(actor);
        await _unitOfWork.CompleteAsync();

        return MapToDto(actor);
    }

    public async Task<bool> UpdateAsync(int id, CreateActorDto dto)
    {
        var actor = await _unitOfWork.Actors.GetByIdAsync(id);
        if (actor is null)
        {
            return false;
        }

        actor.Name = dto.Name;
        actor.BornYear = dto.BornYear;
        actor.Biography = dto.Biography;

        _unitOfWork.Actors.Update(actor);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var actor = await _unitOfWork.Actors.GetByIdAsync(id);
        if (actor is null)
        {
            return false;
        }

        _unitOfWork.Actors.Delete(actor);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    private static ActorDto MapToDto(Actor actor)
    {
        return new ActorDto
        {
            Id = actor.Id,
            Name = actor.Name,
            BornYear = actor.BornYear,
            Biography = actor.Biography
        };
    }
}