using AutoMapper;
using Pokedex.Core.Application.Interfaces.Repositories;
using Pokedex.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Application.Services
{
    public class GenericService<sv, dto, model> : IGenericService<sv, dto, model>
        where dto : class
        where model : class
        where sv : class
    {
        private readonly IGenericRepository<model> _repository;
        private readonly IMapper _mapper;

        public GenericService(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.Repository<model>();
            _mapper = mapper;
        }

        public Task<bool> Add(sv entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(Expression<Func<dto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<dto> FindWhere(Expression<Func<dto, bool>> predicate, Expression<Func<dto, dynamic>> selector)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<dto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<dto> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<dto>> GetList(Expression<Func<model, bool>> predicate = null, Expression<Func<model, dynamic>> include = null)
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
            throw new NotImplementedException();
        }
    }
}
