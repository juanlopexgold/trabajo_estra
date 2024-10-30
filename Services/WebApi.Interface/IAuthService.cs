using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Interface
{
    public interface IAuthService
    {
        Task<string> Authenticate(UserLogin userLogin);
        Task Register(Usuario user);
        Task<bool> VerificarToken(string token);
    }
}