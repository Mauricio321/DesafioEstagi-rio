using Dominio.Models;
using Servicos.DTOs;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Servicos.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public string AddAdmin(AdministradorDTO administrador)
        {
            var admin = new Administrador
            {
                Email = administrador.Email,
                Senha = administrador.Senha
            };

            var adminExistente = userRepository.AdminExistente(admin.Email);

            string Passwordpattern = @"[@#%&$]";
            var PasswordContainsSpecialChar = Regex.IsMatch(admin.Senha, Passwordpattern);

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var containsemailPattern = Regex.IsMatch(admin.Email, emailPattern);

            string passwordNumbers = @"\d";
            var passwordContainsNumber = Regex.IsMatch(admin.Senha, passwordNumbers);

            string passwordCapitaLetter = @"[A-Z]";
            var passwordContainsCapitalLetter = Regex.IsMatch(admin.Senha, passwordCapitaLetter);

            if (adminExistente) 
            {
                return "Admin ja foi adicionado";
            }

            if (!PasswordContainsSpecialChar)
            {
                return "A senha deve conter um caractere especial";
            }

            if (!containsemailPattern)
            {
                return "Email invalido";
            }

            if (!passwordContainsNumber)
            {
                return "A senha deve conter numero";
            }

            if (!passwordContainsCapitalLetter)
            {
                return "A senha deve conter letra maiuscula";
            }

            userRepository.AddAdmin(admin);
            userRepository.Savechanges();

            return "Admin adicionado com sucesso";

        }

        public string AddUser(UsuarioDTO usuario)
        {
            var user = new Usuario
            {
                Email = usuario.Email,
                Senha = usuario.Senha
            };

            var usuarioExistente = userRepository.UsuarioExistente(user.Email);


            string Passwordpattern = @"[@#%&$]";
            var PasswordContainsSpecialChar = Regex.IsMatch(user.Senha, Passwordpattern);

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var containsemailPattern = Regex.IsMatch(user.Email, emailPattern);

            string passwordNumbers = @"\d";
            var passwordContainsNumber = Regex.IsMatch(user.Senha, passwordNumbers);

            string passwordCapitaLetter = @"[A-Z]";
            var passwordContainsCapitalLetter = Regex.IsMatch(user.Senha, passwordCapitaLetter);

            if (usuarioExistente)
            {
                return "Usuario ja foi adicionado";
            }

            if (!PasswordContainsSpecialChar) 
            {
               return "A senha deve conter um caractere especial";
            }

            if (!containsemailPattern) 
            {
                return "Email invalido";
            }

            if (!passwordContainsNumber) 
            {
                return "A senha deve conter numero";
            }

            if (!passwordContainsCapitalLetter) 
            {
                return "A senha deve conter letra maiuscula";
            }
           
            userRepository.AddUser(user);
            userRepository.Savechanges();

            return "usuario adicionado com sucesso";
        }

        public string AuthUser(string email, string senha)
        {
            var user = userRepository.GetUserByEmail(email);


            if (user == null)
            {
                return "Username or password is invalid";
            }

            var hashedPassword = HashPassword(password, user.Salt);

            if (user.Senha != hashedPassword)
            {
                return  "Username or password is invalid", TipoDeErro = TiposDeErro.BadRequest };
            }


            var token = TokenServices.GenerateToken(user);
            return new Envelope<string> { Conteudo = token };
        }
    }
}
