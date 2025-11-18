using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Dtos;

namespace Misa.CRM.Business.Interfaces.Services;

public interface IBaseService<T, TDto> where T : class where TDto : BaseDto
{
    /// <summary>
    /// Lấy tất cả bản ghi
    /// </summary>
    /// <returns></returns>
    IEnumerable<TDto> GetAll();

    /// <summary>
    /// Lấy bản ghi theo id
    /// </summary>
    /// <param name="id">Mã bản ghi</param>
    /// <returns></returns>
    TDto GetById(Guid id);

    /// <summary>
    /// Thêm mới bản ghi
    /// </summary>
    /// <param name="dto">Dữ liệu form input từ người dùng</param>
    /// <returns></returns>
    int Add(TDto dto);

    /// <summary>
    /// Cập nhật bản ghi
    /// </summary>
    /// <param name="dto">Dữ liệu form input từ người dùng</param>
    /// <returns></returns>
    int Update(TDto dto);

    /// <summary>
    /// Xóa một bản bản ghi
    /// </summary>
    /// <param name="dto">Dữ liệu bản ghi</param>
    /// <param name="isHardDelete">Loại xóa: mềm(false) hay cứng</param>
    /// <returns></returns>
    int Delete(TDto dto, bool isHardDelete = false);

    /// <summary>
    /// Xóa nhiều bản ghi
    /// </summary>
    /// <param name="dtoss">Các bản ghi muốn xóa</param>
    /// <param name="isHardDelete">Loại xóa: mềm hay cứng</param>
    /// <returns></returns>
    int Delete(IEnumerable<TDto> dtos, bool isHardDelete = false);
}