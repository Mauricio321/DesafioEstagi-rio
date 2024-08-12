using Dominio.Models;
using FluentResults;
using Servicos.DTOs;
using Servicos.Erros;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public async Task<Result> AddAdmin(AdministradorDTO administrador)
        {
            const int RoleIdUser = 1;

            var admin = new Usuario
            {
                Email = administrador.Email,
                Senha = HashPassword(administrador.Senha, out var salt),
                RoleId = RoleIdUser,
                Salt = salt
            };

            var adminExistente = await userRepository.AdminExistente(administrador.Email);

            string Passwordpattern = @"[@#%&$]";
            var PasswordContainsSpecialChar = Regex.IsMatch(administrador.Senha, Passwordpattern);

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var containsemailPattern = Regex.IsMatch(administrador.Email, emailPattern);

            string passwordNumbers = @"\d";
            var passwordContainsNumber = Regex.IsMatch(administrador.Senha, passwordNumbers);

            string passwordCapitaLetter = @"[A-Z]";
            var passwordContainsCapitalLetter = Regex.IsMatch(administrador.Senha, passwordCapitaLetter);

            if (adminExistente)
            {
                return Result.Fail(new Forbiden("Admin ja foi adicionado"));
            }

            if (!PasswordContainsSpecialChar)
            {
                return Result.Fail(new BadRequest("A senha deve conter um caractere especial"));
            }

            if (!containsemailPattern)
            {
                return Result.Fail(new BadRequest("Email invalido"));
            }

            if (!passwordContainsNumber)
            {
                return Result.Fail(new BadRequest("A senha deve conter numero"));
            }

            if (!passwordContainsCapitalLetter)
            {
                return Result.Fail(new BadRequest("A senha deve conter letra maiuscula"));
            }

            userRepository.AddUser(admin);
            userRepository.Savechanges();

            return Result.Ok();

        }

        public async Task<Result> AddUser(UsuarioDTO usuario)
        {
            const int RoleIdUser = 2;

            var user = new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = HashPassword(usuario.Senha, out var salt),
                RoleId = RoleIdUser,
                Salt = salt
            };

            var usuarioExistente = await userRepository.UsuarioExistente(user.Email);


            string Passwordpattern = @"[@#%&$]";
            var PasswordContainsSpecialChar = Regex.IsMatch(usuario.Senha, Passwordpattern);

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var containsemailPattern = Regex.IsMatch(usuario.Email, emailPattern);

            string passwordNumbers = @"\d";
            var passwordContainsNumber = Regex.IsMatch(usuario.Senha, passwordNumbers);

            string passwordCapitaLetter = @"[A-Z]";
            var passwordContainsCapitalLetter = Regex.IsMatch(usuario.Senha, passwordCapitaLetter);

            if (usuarioExistente)
            {
                return Result.Fail(new Forbiden("Usuario ja foi adicionado"));
            }

            if (!PasswordContainsSpecialChar)
            {
                return Result.Fail(new BadRequest("A senha deve conter um caractere especial"));
            }

            if (!containsemailPattern)
            {
                return Result.Fail(new BadRequest("Email invalido"));
            }

            if (!passwordContainsNumber)
            {
                return Result.Fail(new BadRequest("A senha deve conter numero"));
            }

            if (!passwordContainsCapitalLetter)
            {
                return Result.Fail(new BadRequest("A senha deve conter letra maiuscula"));
            }

            userRepository.AddUser(user);
            userRepository.Savechanges();

            return Result.Ok();
        }

        public async Task<Result<string>> AuthUser(string email, string senha, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmail(email, cancellationToken);


            if (user == null)
            {
                return Result.Fail(new BadRequest("Username or password is invalid"));
            }

            if (!ComparerHashes(senha, user.Salt, user.Senha))
            {
                return Result.Fail(new BadRequest("Username or password is invalid"));
            }


            var token = TokenService.GenerateToken(user);
            return Result.Ok(token);
        }

        private const int keySize = 64;
        private const int iterations = 350000;
        private readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        private string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            return HashPassword(password, salt);
        }

        private string HashPassword(string password, byte[] salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
               Encoding.UTF8.GetBytes(password),
            salt,
               iterations,
               hashAlgorithm,
               keySize);

            return Convert.ToHexString(hash);
        }

        private bool ComparerHashes(string password, byte[] salt, string hashedPasswordToComparer)
        {
            var hashedPassword = HashPassword(password, salt);

            return hashedPassword == hashedPasswordToComparer;
        }

        public async Task DeleteUser(int id)
        {
            var usuario = await userRepository.FiltrarUsuarioPorId(id);

            userRepository.DeleteUsuario(usuario);

            userRepository.Savechanges();
        }

        public Task<IEnumerable<Usuario>> ObterTodosUsuarios()
        {
            return userRepository.ObterTodosUsuarios();
        }
    }
}
