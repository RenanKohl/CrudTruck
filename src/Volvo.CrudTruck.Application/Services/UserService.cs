using FluentValidation.Results;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Application.Auth;
using Volvo.CrudTruck.Application.Interfaces;
using Volvo.CrudTruck.Application.Models;
using Volvo.CrudTruck.Application.Validators;
using Volvo.CrudTruck.Domain.Repository;

namespace Volvo.CrudTruck.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly SignInConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public UserService(IUserRepository repository, SignInConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public async Task<BaseModel<UserModel.Response>> Login(UserModel.Request request)
        {
            UserValidator validator = new UserValidator();

            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                return new BaseModel<UserModel.Response>(true, "Usuário ou senha inválido(s)", null, results.Errors.ToArray());
            }

            var userAuthenticated = await _repository.Login(request.Login, request.Password);

            if (userAuthenticated.Equals(null))
                return new BaseModel<UserModel.Response>(true, "Usuário ou senha inválido(s)");

            return new BaseModel<UserModel.Response>(false, "Login realizado com sucesso", GenerateToken(userAuthenticated.Name, _signingConfigurations, _tokenConfigurations));

        }

        private UserModel.Response GenerateToken(string name, SignInConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(name, "Name"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
                }
            );

            DateTime created = DateTime.Now;
            DateTime expiry = created + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = created,
                Expires = expiry
            });
            var token = handler.WriteToken(securityToken);

            var resultado = new UserModel.Response(
                userName: name,
                created: created.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration: created.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken: token
            );

            return resultado;
        }

    }
}
