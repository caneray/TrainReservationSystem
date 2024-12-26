using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainReservationSystem.Application.Dto;
using TrainReservationSystem.Domain.Entities;

namespace TrainReservationSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserDto userDto);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(Guid userId, UserDto userDto);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
