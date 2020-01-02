using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountService<T>
    {
        Task<IdentityResult> RegisterUser(T model);
        Task<IdentityResult> ChangePassword(string name, string oldPass, string newPass);
    }
}
