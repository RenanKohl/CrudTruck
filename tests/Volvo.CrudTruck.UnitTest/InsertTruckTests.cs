using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Application.Services;
using Volvo.CrudTruck.Data.Repository;
using Xunit;
using FluentAssertions;

namespace Volvo.CrudTruck.UnitTest
{    
    [Collection(nameof(TruckCollection))]
    public class InsertTruckTests
    {
        private readonly CrudTruckTestsFixture _fixture;
        private readonly TruckService _service;

        public InsertTruckTests(CrudTruckTestsFixture fixture)
        {
            _fixture = fixture;
            _service = fixture.ObterTruckService();
        }

        [Fact(DisplayName = "Inserir caminhão válido")]
        [Trait("CRUD", "Inserir caminhão válido")]
        public async void CRUD_Insert_DeveEstarValido()
        {
            // Arrange
            var caminhao = await _fixture.GerarCaminhaoValido();
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.Insert(caminhao))
              .Returns(_fixture.GerarCaminhaoValido());
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.UnitOfWork.Commit())
              .Returns(Task.FromResult(true));
            // Act
            var result = await _service.Insert(_fixture.GerarTruckModelValido());

            // Assert             
            result.Error.Should().BeFalse();
            result.Message.Should().Be("Registro incluído com sucesso");
        }

        [Fact(DisplayName = "Inserir caminhão inválido")]
        [Trait("CRUD", "Inserir caminhão inválido")]
        public async void CRUD_Insert_DeveEstarInvalido()
        {
            // Arrange
            var caminhao = await _fixture.GerarCaminhaoInvalido();
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.Insert(caminhao))
              .Returns(_fixture.GerarCaminhaoInvalido());
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.UnitOfWork.Commit())
             .Returns(Task.FromResult(false));

            // Act
            var result = await _service.Insert(_fixture.GerarTruckModelInvalido());

            // Assert 
            result.Error.Should().BeTrue();
            result.Message.Should().Be("Falha ao inserir caminhão");
        }
    }
}
