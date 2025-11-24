namespace Misa.CRM.Business.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    /// <summary>
    /// Lấy danh sách tất cả KH
    /// </summary>
    /// <returns>Danh sách tất cả KH</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Lấy lệnh query base của từng entity
    /// </summary>
    /// <returns>Query base</returns>
    string GetBaseQuery();

    /// <summary>
    /// Lấy thông tin KH theo mã KH
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Tất cả thông tin của KH</returns>
    T GetById(string id);

    /// <summary>
    /// Insert KH vào DB
    /// </summary>
    /// <param name="entity">Dữ liệu KH theo thực thể</param>
    /// <returns>Số lượng bản ghi ảnh hưởng</returns>
    int Add(T entity);

    /// <summary>
    /// Cập nhật KH vào DB
    /// </summary>
    /// <param name="entity">Dữ liệu KH theo thực thể</param>
    /// <returns>Số lượng bản ghi ảnh hưởng</returns>
    int Update(T entity);

    /// <summary>
    /// Xóa một KH trong DB
    /// </summary>
    /// <param name="id">Mã KH</param>
    /// <param name="isHardDelete">Xóa cứng hoặc mềm</param>
    /// <returns>Số lượng bản ghi ảnh hưởng</returns>
    int Delete(string id, bool isHardDelete = false);

    /// <summary>
    /// Xóa nhiều KH trong DB
    /// </summary>
    /// <param name="ids">Danh sách mã KH</param>
    /// <param name="isHardDelete">Xóa cứng hoặc mềm</param>
    /// <returns>Số lượng bản ghi ảnh hưởng</returns>
    int Delete(IEnumerable<string> ids, bool isHardDelete = false);
}
