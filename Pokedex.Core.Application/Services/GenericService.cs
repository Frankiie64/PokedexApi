using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Pokedex.Core.Application.Interfaces.Repositories;
using Pokedex.Core.Application.Interfaces.Services;
using Pokedex.Core.Domain.Commons;
using System.Linq.Expressions;

namespace Pokedex.Core.Application.Services
{
    public class GenericService<sv, dto, model> : IGenericService<sv, dto, model>
        where dto : class
        where model : AuditableBaseEntity
        where sv : class
    {
        private readonly IGenericRepository<model> _repository;
        private readonly IMapper _mapper;

        public GenericService(IMapper mapper,IUnitOfWork unitOfWork)
        {          
            _mapper = mapper;
            _repository = unitOfWork.Repository<model>();
        }

        public async Task<bool> Add(sv entity)
        {
            try
            {
                model model = _mapper.Map<model>(entity);
                return await _repository.Add(model);
            }
            catch (Exception e)
            {
                throw e;
            }        
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                return await _repository.Delete(Id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<bool> Exists(Expression<Func<model, bool>> predicate)
        {
            try
            {
                return await _repository.Exists(predicate);
            }
            catch (Exception e)
            {

                throw e;
            }        
        }

        public async Task<dto> FindWhere(Expression<Func<model, bool>> predicate, Expression<Func<model, dynamic>> include)
        {
            try
            {
                var result = await _repository.FindWhere(predicate, include);
                return _mapper.Map<dto>(result);

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IEnumerable<dto>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll();
                return _mapper.Map<IEnumerable<dto>>(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<dto> GetById(Guid Id)
        {
            try
            {
                var result = await _repository.GetById(Id);
                return _mapper.Map<dto>(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<dto>> GetList(Expression<Func<model, bool>> predicate = null, Func<IQueryable<model>, IIncludableQueryable<model, object>> include = null)
        {
            try
            {
                var result = await _repository.GetList(predicate, include);
                return _mapper.Map<IEnumerable<dto>>(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Update(sv entity)
        {
            try
            {
                var model = _mapper.Map<model>(entity);
                return await _repository.Update(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
