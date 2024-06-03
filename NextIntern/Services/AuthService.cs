using NextIntern.Application.Common.Interfaces;
using NextIntern.Application.DTOs;
using NextIntern.Domain.Entities;
using NextIntern.Domain.IRepositories;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<string?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return null; // Invalid username or password
        }

        return _jwtService.CreateToken(user.InternId.ToString(), "User");
    }

    public async Task<bool> SignupAsync(SignupRequest request)
    {
        var existingUser = await _userRepository.GetUserByUsernameAsync(request.Username);
        if (existingUser != null)
        {
            return false; // User already exists
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var newUser = new Intern
        {
            InternId = Guid.NewGuid(),
            Username = request.Username,
            Password = hashedPassword,
            //FullName = fullName,
            //Dob = dob,
            //Gender = gender,
            //Telephone = telephone,
            //Email = email,
            //Address = address,
            //MentorId = mentorId,
            //CampaignId = campaignId,
            CreateDate = DateTime.Now
        };

        await _userRepository.CreateInternAsync(newUser);
        return true;
    }
}
