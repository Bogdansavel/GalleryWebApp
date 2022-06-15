using AutoMapper;
using GalleryWebApp.Exceptions;
using GalleryWebApp.Repositories.Contracts;
using GalleryWebApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace GalleryWebApp.Services
{
    public abstract class Service<Model, DTO> : IService<Model, DTO> where Model : class
                                                            where DTO : class
    {
        protected readonly IRepository<Model> _repository;
        protected readonly IMapper _mapper;

        public Service(IRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task CreateAsync(DTO item)
        {
            await _repository.CreateAsync(_mapper.Map<DTO, Model>(item));
        }

        public async Task<DTO> GetByIdAsync(Guid id)
        {
            var item = await checkIfExisted(id);
            return _mapper.Map<Model, DTO>(item);
        }

        public async Task<IEnumerable<DTO>> GetAllAsync()
        {
            return (await _repository.GetAllAsync()).Select(m => _mapper.Map<Model, DTO>(m));
        }

        public async Task<IEnumerable<DTO>> GetAsync(Expression<Func<Model, bool>> predicate)
        {
            return (await _repository.GetAsync(predicate)).Select(m => _mapper.Map<Model, DTO>(m));
        }

        public virtual async Task RemoveAsync(Guid itemId)
        {
            var item = await checkIfExisted(itemId);

            await _repository.RemoveAsync(item);
        }

        public virtual async Task UpdateAsync(DTO item)
        {
            await _repository.UpdateAsync(_mapper.Map<DTO, Model>(item));
        }

        private async Task<Model> checkIfExisted(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item is null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Id = typeof(Model).Name + " with such id doesn't exist" });
            }
            return item;
        }
    }
}
