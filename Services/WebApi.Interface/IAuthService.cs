using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Interface
{
    public interface IAuthService
    {
        Task<string> Authenticate(UserLogin userLogin);
        Task Register(Usuarios user);
        Task<bool> VerificarToken(string token)
    }
}