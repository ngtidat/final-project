using Misa.CRM.Business.Entities;

namespace Misa.CRM.Business.Interfaces.Services;

public interface IBaseService<T, TDto, TCreateUpdateDto> where T : BaseEntity where TDto : class where TCreateUpdateDto: class
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
    TDto GetById(string id);

    /// <summary>
    /// Thêm mới bản ghi
    /// </summary>
    /// <param name="dto">Dữ liệu form input từ người dùng</param>
    /// <returns></returns>
    int Add(TCreateUpdateDto dto);

    /// <summary>
    /// Cập nhật bản ghi
    /// </summary>
    /// <param name="dto">Dữ liệu form input từ người dùng</param>
    /// <returns></returns>
    int Update(string id, TCreateUpdateDto dto);

    /// <summary>
    /// Xóa một bản bản ghi
    /// </summary>
    /// <param name="dto">Dữ liệu bản ghi</param>
    /// <param name="isHardDelete">Loại xóa: mềm(false) hay cứng</param>
    /// <returns></returns>
    int Delete(string id, bool isHardDelete = false);

    /// <summary>
    /// Xóa nhiều bản ghi
    /// </summary>
    /// <param name="dtoss">Các bản ghi muốn xóa</param>
    /// <param name="isHardDelete">Loại xóa: mềm hay cứng</param>
    /// <returns></returns>
    int Delete(IEnumerable<string> ids, bool isHardDelete = false);
}