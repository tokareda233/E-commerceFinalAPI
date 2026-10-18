using E_commerceFinalAPI.DTO.Auth;

namespace E_commerceFinalAPI.Repos.Interfaces
{
    public interface IAuthRepo
    {
        Task<string> Login(LoginDTO dto);
        Task<bool> Register(RegisterDTO dto);
    }
}
