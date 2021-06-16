using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Application.Interfaces;
using Volvo.CrudTruck.Application.Models;
using Volvo.CrudTruck.Application.Validators;
using Volvo.CrudTruck.Data.Repository;
using Volvo.CrudTruck.Domain;

namespace Volvo.CrudTruck.Application.Services
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepository _repository;
        private readonly ILogger _logger;
        public TruckService(ITruckRepository repository, ILogger<TruckService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseModel<TruckModel.Response>> Delete(int id)
        {
            try
            {
                var entity = await _repository.SelectById(id);
                if(entity == null)
                    return new BaseModel<TruckModel.Response>(true, "Registro não encontrado");

                await _repository.Delete(entity);
                await _repository.UnitOfWork.Commit();

                return new BaseModel<TruckModel.Response>(false, "Registro removido com sucesso");
            }catch(Exception e)
            {
                _logger.LogError(e.Message, e);
                return new BaseModel<TruckModel.Response>(true, "Falha ao remover registro");
            }
        }

        public async Task<BaseModel<IEnumerable<TruckModel.Response>>> GetAll()
        {
            List<TruckModel.Response> response = new List<TruckModel.Response>();
            var entities = await _repository.SelectAll();
            foreach(Truck entity in entities)
            {
                response.Add(new TruckModel.Response(entity));
            }

            return new BaseModel<IEnumerable<TruckModel.Response>>(false, "Consulta realizada", response);
        }

        public async Task<BaseModel<TruckModel.Response>> GetById(int id)
        {
            var entity = await _repository.SelectById(id);
            if(entity == null)
                return new BaseModel<TruckModel.Response>(false, "Consulta sem registros", null);
            
            return new BaseModel<TruckModel.Response>(false, "Consulta realizada", new TruckModel.Response(entity));
        }

        public async Task<BaseModel<TruckModel.Response>> Insert(TruckModel.Request truck)
        {
            try
            {
                TruckValidator validator = new TruckValidator();
                var results = validator.Validate(truck);
                if(!results.IsValid)
                    return new BaseModel<TruckModel.Response>(true, "Falha ao inserir caminhão", null, BaseModel<TruckModel.Response>.SerializeErrors(results.Errors));

                await _repository.Insert(new Truck(truck.Model, truck.ModelYear));
                await _repository.UnitOfWork.Commit();
                return new BaseModel<TruckModel.Response>(false, "Registro incluído com sucesso");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return new BaseModel<TruckModel.Response>(true, "Falha ao incluir registro");
            }
        }

        public async Task<BaseModel<TruckModel.Response>> Update(int id, TruckModel.Request truck)
        {
            try
            {
                TruckValidator validator = new TruckValidator();
                var results = validator.Validate(truck);
                if (!results.IsValid)
                    return new BaseModel<TruckModel.Response>(true, "Falha ao atualizar caminhão", null, BaseModel<TruckModel.Response>.SerializeErrors(results.Errors));

                var entity = await _repository.SelectById(id);

                entity.ChangeModel(truck.Model);
                entity.ChangeModelYear(truck.ModelYear);

                await _repository.Update(entity);
                await _repository.UnitOfWork.Commit();
                return new BaseModel<TruckModel.Response>(false, "Registro atualizado com sucesso");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return new BaseModel<TruckModel.Response>(true, "Falha ao atualizar registro");
            }
        }
    }
}
