
using Microsoft.IdentityModel.Tokens;
using Namotion.Reflection;
using VDG_Web_Api.src.DTOs.PersonDTOs;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly string FailedUserOperationMessage = "User operation failed due to unexpected error";
    private readonly string InvalidPersonObjectMessage = "Person is invalid";
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    private string UserInvalidOperationErrorMessage(string operationName) => 
        $"{operationName} {FailedUserOperationMessage}";

    private bool IsValidPerson(string? firstName, string? lastName) => 
        !firstName.IsNullOrEmpty() && !lastName.IsNullOrEmpty();

    private bool IsValidUser(string? email) =>
        !email.IsNullOrEmpty();

    public PersonDTO MapPersonToDto(Person person) => new()
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Phone = person.Phone
        };

    public PersonProfileDTO MapPersonToProfileDTO(Person person) => new()
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Phone = person.Gender,
            BirthDate = person.Birthdate,
            Gender = person.Gender,
            PersonalId = person.PersonalId
        };

    public Person MapPersonDtoToEntity(PersonDTO personDto)
    {

        return new Person();
    }

    public Person MapPersonDtoToEntity(PersonProfileDTO personDetailsDto)
    {
        return new()
        {
            Id = personDetailsDto.Id!.Value,
            Birthdate = personDetailsDto.BirthDate,
            FirstName = personDetailsDto.FirstName!,
            LastName = personDetailsDto.LastName!,
            Gender = personDetailsDto.Gender,
            PersonalId = personDetailsDto.PersonalId,
            Phone = personDetailsDto.Phone
        };
    }

    public User MapUserDtoToEntity(UserDTO userDto)
    {
        return new User()
        {
            Id = userDto.Id!.Value,
            Email = userDto.Email,
            Person = MapPersonDtoToEntity(userDto.Person),
            PersonId = userDto.Person.Id,
            Role = userDto.Role
        };
    }
    public UserDTO MapUserToDto(User user)
    {
        return new UserDTO()
        {
            Id = user.Id,
            Email = user.Email,
            Person = MapPersonToDto(user.Person!),
            Role = user.Role
        };
}

    public async Task DeleteUserAsync(int userId)
    {
        try
        {
            await _userRepository.DeleteUserAsync(userId);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"{UserInvalidOperationErrorMessage(nameof(DeleteUserAsync))}, {ex.Message}", ex);
        }
    }

    public async Task<UserDTO?> GetUser(int userId)
    {
        
        var user = await _userRepository.GetById(userId);

        if(user == null)
        {
            throw new KeyNotFoundException("User has not been found");
        }

        try
        {
            return MapUserToDto(user);    
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed mapping user, Error: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<UserDTO>> GetUsers(int page, int limit)
    {
        try
        {
            var users = await _userRepository.GetUsers(page, limit);
            return users.Select(MapUserToDto);
        }
        catch (Exception ex)
        {
            throw new Exception($"{UserInvalidOperationErrorMessage(nameof(GetUsers))}, {ex.Message}", ex);
        }
    }

    public async Task UpdateUserAsync(UserDTO userDTO)
    {
        if(userDTO.Id == null)
        {
            throw new ArgumentNullException("User Id must be provided to complete this operation.");
        }

        var user = await _userRepository.GetById(userDTO.Id!.Value);

        if(user == null)
        {
            throw new ArgumentNullException("Invalid User");
        }
        
        try
        {
            await _userRepository.UpdateUserAsync(user);    
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Update Failed. Error {ex.Message}", ex);
        }
    }
}