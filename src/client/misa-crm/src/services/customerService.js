// src/services/customerService.js
import api from './api.js'; // import axios instance đã cấu hình

export const customerService = {
  // Lấy danh sách tất cả khách hàng
  getAll: () => api.get('/Customer'),

  // Lấy 1 khách hàng theo id
  getById: (id) => api.get(`/Customer/${id}`),

  getNewCustomerId: (id) => api.get(`/Customer/get-new-id`),

  // Tìm kiếm và phân trang
  searchAndPaginate: (pageIndex, pageSize, strSearch, sortColumn, sortDirection, customerTypeId) =>
    api.get('/Customer/search', {
      params: {
        pageIndex,
        pageSize,
        strSearch,
        sortColumn,
        sortDirection,
        customerTypeId
      }
    }),

  // Tạo khách hàng mới (có thể kèm file)
  create: (formData) => api.post('/Customer/create', formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  }),

  // Cập nhật khách hàng (có thể kèm file)
  update: (id, formData) => api.put(`/Customer/update`, formData, {
    params: { id },
    headers: { 'Content-Type': 'multipart/form-data' }
  }),

  // Xoá khách hàng theo id
  delete: (id) => api.delete(`/Customer/${id}`),

  // Gọi API xóa nhiều khách hàng
  deleteMulti: (ids) => api.post('/Customer/delete-multiple', ids),

  import: (formData) => api.post('/Customer/import', formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  }),

  checkExistEmail: (email) => api.get('/Customer/check-exist-email', {
    params: { email }
  }),

  checkExistPhone: (phone) => api.get('/Customer/check-exist-phone', {
    params: { phone }
  }),

  changeCustomerTypeMany: (ids, typeId) =>
    api.put('/Customer/change-customer-type', {
      ids: ids,
      customerTypeId: typeId
    })
};