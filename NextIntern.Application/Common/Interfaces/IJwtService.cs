namespace NextIntern.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(string ID, string roles);
    }
}