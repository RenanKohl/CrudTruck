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
    public class DeleteTruckTests
    {
        private readonly CrudTruckTestsFixture _fixture;
        private readonly TruckService _service;

        public DeleteTruckTests(CrudTruckTestsFixture fixture)
        {
            _fixture = fixture;
            _service = fixture.ObterTruckService();
        }

        [Fact(DisplayName = "Excluir caminhão válido")]
        [Trait("CRUD", "Excluir caminhão válido")]
        public async void CRUD_Delete_DeveEstarValido()
        {
            // Arrange
            var caminhao = await _fixture.GerarCaminhaoValido();
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.SelectById(caminhao.Id))
              .Returns(_fixture.GerarCaminhaoValido());
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.Delete(caminhao))
              .Returns(Task.CompletedTask);
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.UnitOfWork.Commit())
              .Returns(Task.FromResult(true));
            // Act
            var result = await _service.Delete(caminhao.Id);

            // Assert             
            result.Error.Should().BeFalse();
            result.Message.Should().Be("Registro removido com sucesso");
        }

        [Fact(DisplayName = "Excluir caminhão inválido")]
        [Trait("CRUD", "Excluir caminhão inválido")]
        public async void CRUD_Insert_DeveEstarInvalido()
        {
            // Arrange
            var caminhao = await _fixture.GerarCaminhaoInvalido();
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.SelectById(caminhao.Id))
              .Returns(Task.FromResult((Truck)null));
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.Delete(caminhao))
              .Returns(Task.CompletedTask);
            _fixture.Mocker.GetMock<ITruckRepository>().Setup(c => c.UnitOfWork.Commit())
             .Returns(Task.FromResult(false));

            // Act
            var result = await _service.Delete(caminhao.Id);

            // Assert 
            result.Error.Should().BeTrue();
            result.Message.Should().Be("Registro não encontrado");
        }
    }
}
