using System;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        public Task<string> Login(UsuarioLogin usuarioLogin)
        {
            throw new NotImplementedException();
        }

        public Task<string> Registro(UsuarioRegistro usuarioRegistro)
        {
            throw new NotImplementedException();
        }
    }
}

