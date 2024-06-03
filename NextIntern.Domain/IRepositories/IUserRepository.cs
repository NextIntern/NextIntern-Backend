using NextIntern.Domain.Entities;

namespace NextIntern.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<Intern?> GetUserByUsernameAsync(string username);
        Task CreateInternAsync(Intern intern);
    }
}
