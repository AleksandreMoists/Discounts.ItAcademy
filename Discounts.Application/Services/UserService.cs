using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;
using Discounts.Application.Interfaces;
using Discounts.Domain.Entities;
using Discounts.Domain.Enums;
using Discounts.Domain.Exceptions;
using Discounts.Domain.Interfaces;
using Mapster;

namespace Discounts.Application.Services;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> _userRepository;

    public UserService(IBaseRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var user = await _userRepository.GetById(id, ct);
        if (user is null)
            throw new NotFoundException($"User with ID {id} was not found.");

        return user.Adapt<UserResponseDto>();
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var users = await _userRepository.GetAll(ct);
        return users.Adapt<IEnumerable<UserResponseDto>>();
    }

    public async Task<UserResponseDto> CreateAsync(UserCreateDto dto, CancellationToken ct = default)
    {
        var user = dto.Adapt<User>();
        user.Role = UserRoles.Customer;

        var created = await _userRepository.Add(user, ct);
        return created.Adapt<UserResponseDto>();
    }

    public async Task<UserResponseDto> UpdateAsync(int id, UserUpdateDto dto, CancellationToken ct = default)
    {
        var user = await _userRepository.GetById(id, ct);
        if (user is null)
            throw new NotFoundException($"User with ID {id} was not found.");

        dto.Adapt(user);
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.Update(user);
        return user.Adapt<UserResponseDto>();
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var user = await _userRepository.GetById(id, ct);
        if (user is null)
            throw new NotFoundException($"User with ID {id} was not found.");

        return await _userRepository.Delete(user);
    }
}
