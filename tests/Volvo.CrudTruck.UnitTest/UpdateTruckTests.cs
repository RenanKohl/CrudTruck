using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Application.Services;
using Volvo.CrudTruck.Data.Repository;
using Xunit;

namespace Volvo.CrudTruck.UnitTest
{
    [Collection(nameof(TruckCollection))]
    public class UpdateTruckTests
    {
        private readonly CrudTruckTestsFixture _fixture;
        private readonly TruckService _service;

        public UpdateTruckTests(CrudTruckTestsFixture fixture)
        {
            _fixture = fixture;
            _service = fixture.ObterTruckService();
        }


        [Fact(DisplayName = "Atualizar caminhão válido")]
        [Trait("CRUD", "Atualizar caminhão válido")]
        public async void CRUD_Update_DeveEstarValido()
        {
            // Arrange
            var caminhao = await _fixture.GerarCaminhaoValido();
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.SelectById(caminhao.Id))
              .Returns(_fixture.GerarCaminhaoValido());
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.Update(caminhao))
              .Returns(_fixture.GerarCaminhaoValido());
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.UnitOfWork.Commit())
              .Returns(Task.FromResult(true));
            // Act
            var result = await _service.Update(caminhao.Id, _fixture.GerarTruckModelValido());

            // Assert             
            result.Error.Should().BeFalse();
            result.Message.Should().Be("Registro atualizado com sucesso");
        }

        [Fact(DisplayName = "Atualizar caminhão inválido")]
        [Trait("CRUD", "Atualizar caminhão inválido")]
        public async void CRUD_Update_DeveEstarInvalido()
        {
            // Arrange
            var caminhao = await _fixture.GerarCaminhaoInvalido();
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.Update(caminhao))
              .Returns(_fixture.GerarCaminhaoInvalido());
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.UnitOfWork.Commit())
             .Returns(Task.FromResult(false));

            // Act
            var result = await _service.Update(caminhao.Id, _fixture.GerarTruckModelInvalido());

            // Assert 
            result.Error.Should().BeTrue();
            result.Message.Should().Be("Falha ao atualizar caminhão");
        }
    }
}
