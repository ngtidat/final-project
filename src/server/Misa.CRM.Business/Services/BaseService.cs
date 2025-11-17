using AutoMapper;
using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Interfaces.Repositories;
using Misa.CRM.Business.Interfaces.Services;

namespace Misa.CRM.Business.Services;

public class BaseService<T, TDto> : IBaseService<T, TDto> where TDto : class where T : class
{
    protected readonly IBaseRepository<T> _repository;

    protected IMapper _mapper;

    public BaseService(IBaseRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public int Add(TDto dto)
    {
        throw new NotImplementedException();
    }

    public int Delete(TDto dto, bool isHardDelete = false)
    {
    throw new NotImplementedException();
    }

    public int Delete(IEnumerable<TDto> dtos, bool isHardDelete = false)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TDto> GetAll()
    {
        return _mapper.Map<IEnumerable<TDto>>(_repository.GetAll());
    }

    public TDto GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PaginatedResult<TDto>> Paginate(int pageNumber, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public int Update(TDto dto)
    {
        throw new NotImplementedException();
    }
}
