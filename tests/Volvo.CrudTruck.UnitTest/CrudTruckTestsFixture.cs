using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Application.Auth;
using Volvo.CrudTruck.Application.Models;
using Volvo.CrudTruck.Application.Services;
using Volvo.CrudTruck.Domain;
using Xunit;


namespace Volvo.CrudTruck.UnitTest
{
    [CollectionDefinition(nameof(TruckCollection))]
    public class TruckCollection : ICollectionFixture<CrudTruckTestsFixture>
    { }

    public class CrudTruckTestsFixture : IDisposable
    {
        public UserService UserService;
        public TruckService TruckService;
        public AutoMocker Mocker;
        public DateTime Now = DateTime.Now;

        public UserModel.Request GerarUsuarioModelValido()
        {
            return new UserModel.Request() { Login = "Rkohl", Password = "123" };
        }
        public UserModel.Request GerarUsuarioModelInvalido()
        {
            return new UserModel.Request() { Login = "", Password = "" };
        }
        public TruckModel.Request GerarTruckModelValido()
        {
            return new TruckModel.Request() { Model = "FH", ModelYear = Now.Year };
        }
        public TruckModel.Request GerarTruckModelInvalido()
        {
            return new TruckModel.Request() { Model = "FY", ModelYear = Now.Year-2 };
        }
        public Task<User> GerarUsuarioValido()
        {
            return Task.FromResult(new User("Renan", "Rkohl", "123"));
        }
        public Task<User> GerarUsuarioInvalido()
        {
            return Task.FromResult((User)null);
        }

        public async Task<IEnumerable<Truck>> GerarCaminhoes()
        {
            var caminhoes = new List<Truck>();
            for (var i = 0; i < 6; i++)
                caminhoes.Add(await GerarCaminhaoValido());

            return caminhoes;
        }

        public Task<Truck> GerarCaminhaoValido()
        {
            return Task.FromResult(new Truck("FH", Now.Year+1));
        }

        public Task<Truck> GerarCaminhaoInvalido()
        {
            return Task.FromResult(new Truck("FY", Now.Year-2));
        }

        public UserService ObterUserService()
        {
            Mocker = new AutoMocker();
            UserService = Mocker.CreateInstance<UserService>();
            return UserService;
        }
        public TruckService ObterTruckService()
        {
            Mocker = new AutoMocker();
            TruckService = Mocker.CreateInstance<TruckService>();
            return TruckService;
        }        

        public void Dispose()
        {
        }

    }
}
