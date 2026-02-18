using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;

namespace Discounts.Application.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<UserResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<UserResponseDto> CreateAsync(UserCreateDto dto, CancellationToken ct = default);
    Task<UserResponseDto> UpdateAsync(int id, UserUpdateDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
