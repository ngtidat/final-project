using AutoMapper;
using Microsoft.VisualBasic;
using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Dtos;
using Misa.CRM.Business.Interfaces.Repositories;
using Misa.CRM.Business.Interfaces.Services;

namespace Misa.CRM.Business.Services;

public class BaseService<T, TDto> : IBaseService<T, TDto> where TDto : BaseDto where T : class
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

    // public PaginatedResult<TDto> Paginate(int pageIndex, int pageSize, string? strSearch, string? sortColumn, int sortDirection)
    // {
    //     var dto = (TDto)Activator.CreateInstance(typeof(TDto))!;
    //     var searchColumns = string.Join(", ", dto.SearchableColumns!);
    //     var baseQuery = _repository.GetBaseQuery();

    //     var paginatedResult = _repository.Paginate(baseQuery, searchColumns, pageIndex, pageSize, strSearch, sortColumn, sortDirection);
    //     var mappedItems = _mapper.Map<List<TDto>>(paginatedResult.Items);
    //     return new PaginatedResult<TDto>(
    //         pageIndex,
    //         pageSize,
    //         mappedItems.Count,
    //         [.. mappedItems]
    //     );
    // }

    public int Update(TDto dto)
    {
        throw new NotImplementedException();
    }
}
