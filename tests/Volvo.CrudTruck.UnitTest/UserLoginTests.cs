using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Application.Auth;
using Volvo.CrudTruck.Application.Interfaces;
using Volvo.CrudTruck.Application.Services;
using Volvo.CrudTruck.Data.Repository;
using Volvo.CrudTruck.Domain;
using Xunit;

namespace Volvo.CrudTruck.UnitTest
{
    [Collection(nameof(TruckCollection))]
    public class UserLoginTests
    {
        private readonly CrudTruckTestsFixture _fixture;
        private readonly UserService _service;

        public UserLoginTests(CrudTruckTestsFixture fixture)
        {
            _fixture = fixture;
            _service = fixture.ObterUserService();
        }

        [Fact(DisplayName = "Login Usuário Válido")]
        [Trait("Login", "Login usuário válido")]
        public async void Usuario_Login_DeveEstarValido()
        {
            // Arrange
            var usuario = _fixture.GerarUsuarioModelValido();

            _fixture.Mocker.GetMock<IUserRepository>().Setup(c => c.Login(usuario.Login, usuario.Password))
              .Returns(_fixture.GerarUsuarioValido());

            // Act
            var result = await _service.Login(usuario);

            // Assert 
            Assert.False(result.Error);
            Assert.Equal("Login realizado com sucesso", result.Message);
        }

        [Fact(DisplayName = "Login Usuário Inválido")]
        [Trait("Login", "Login usuário inválido")]
        public async void Usuario_Login_DeveEstarInValido()
        {
            // Arrange
            var usuario = _fixture.GerarUsuarioModelInvalido();

            _fixture.Mocker.GetMock<IUserRepository>().Setup(c => c.Login(usuario.Login, usuario.Password))
              .Returns(_fixture.GerarUsuarioInvalido());

            // Act
            var result = await _service.Login(usuario);

            // Assert 
            Assert.True(result.Error);
            Assert.Equal("Usuário ou senha inválido(s)", result.Message);
            Assert.NotNull(result.ValidationResults);
        }
    }
}
