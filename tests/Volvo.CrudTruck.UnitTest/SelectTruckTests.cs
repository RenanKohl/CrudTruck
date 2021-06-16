using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Application.Services;
using Volvo.CrudTruck.Data.Repository;
using Volvo.CrudTruck.Domain;
using Xunit;

namespace Volvo.CrudTruck.UnitTest
{
    [Collection(nameof(TruckCollection))]
    public class SelectTruckTests
    {
        private readonly CrudTruckTestsFixture _fixture;
        private readonly TruckService _service;
        public SelectTruckTests(CrudTruckTestsFixture fixture)
        {
            _fixture = fixture;
            _service = fixture.ObterTruckService();
        }

        [Fact(DisplayName = "Selecionar todos os caminhões")]
        [Trait("CRUD", "Selecionar todos os caminhões")]
        public async void CRUD_Select_DeveEstarValido()
        {
            // Arrange
            var caminhao = await _fixture.GerarCaminhaoValido();
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.SelectAll())
              .Returns(_fixture.GerarCaminhoes());
           
            // Act
            var result = await _service.GetAll();

            // Assert             
            result.Error.Should().BeFalse();
            result.Message.Should().Be("Consulta realizada");
            result.Data.Should().NotBeNull();
        }

        [Fact(DisplayName = "Selecionar caminhão por id válido")]
        [Trait("CRUD", "Selecionar caminhão por id válido")]
        public async void CRUD_SelectById_DeveEstarValido()
        {
            // Arrange
            var caminhao = await _fixture.GerarCaminhaoValido();
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.SelectById(caminhao.Id))
              .Returns(_fixture.GerarCaminhaoValido());

            // Act
            var result = await _service.GetById(caminhao.Id);

            // Assert             
            result.Error.Should().BeFalse();
            result.Message.Should().Be("Consulta realizada");
            result.Data.Should().NotBeNull();
        }

        [Fact(DisplayName = "Selecionar caminhão por id inválido")]
        [Trait("CRUD", "Selecionar caminhão por id inválido")]
        public async void CRUD_SelectById_DeveEstarInvalido()
        {
            // Arrange
            var caminhao = await _fixture.GerarCaminhaoValido();
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.SelectById(caminhao.Id))
              .Returns(Task.FromResult((Truck)null));

            // Act
            var result = await _service.GetById(caminhao.Id);

            // Assert             
            result.Error.Should().BeFalse();
            result.Message.Should().Be("Consulta sem registros");
            result.Data.Should().BeNull();
        }

    }
}
