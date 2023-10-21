using NsTask.Api.Dtos.Authorization;

namespace NsTask.Api.Bl.Interfaces
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(LoginModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
