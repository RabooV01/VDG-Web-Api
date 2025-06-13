using VDG_Web_Api.src.DTOs.PersonDTOs;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IUserService
{
	public Task<UserDTO?> GetUser(int userId);
	public Task<IEnumerable<UserDTO>> GetUsers(int page, int limit);
	public Task UpdateUserAsync(UserDTO userDTO);
	public Task DeleteUserAsync(int userId);
	public PersonDTO MapPersonToDto(Person person);
    public PersonProfileDTO MapPersonToProfileDTO(Person person);
    public Person MapPersonDtoToEntity(PersonDTO personDto);
    public Person MapPersonDtoToEntity(PersonProfileDTO personDetailsDto);
    public User MapUserDtoToEntity(UserDTO userDto);
    public UserDTO MapUserToDto(User user);

}