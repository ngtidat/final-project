using Misa.CRM.Business.Common.Models;

namespace Misa.CRM.Business.Interfaces.Services;

public interface IBaseService<T, TDto> where T : class where TDto : class
{
    IEnumerable<TDto> GetAll();

    IEnumerable<PaginatedResult<TDto>> Paginate(int pageNumber, int pageSize, out int totalRecords);

    TDto GetById(Guid id);

    int Add(TDto dto);

    int Update(TDto dto);

    int Delete(TDto dto, bool isHardDelete = false);

    int Delete(IEnumerable<TDto> dtos, bool isHardDelete = false);
}