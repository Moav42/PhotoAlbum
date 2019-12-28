using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthorizationService<T>
    {
        Task<string> GetJWT(string name, string password);
    }
}
