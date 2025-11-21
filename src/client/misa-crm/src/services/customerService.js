// src/services/customerService.js
import api from './api.js'; // import axios instance đã cấu hình

export const customerService = {
  // Lấy danh sách tất cả khách hàng
  getAll: () => api.get('/Customer'),

  // Lấy 1 khách hàng theo id
  getById: (id) => api.get(`/Customer/${id}`),

  getNewCustomerId: (id) => api.get(`/Customer/get-new-id`),

  // Tìm kiếm và phân trang
  searchAndPaginate: (pageIndex, pageSize, strSearch, sortColumn, sortDirection) =>
    api.get('/Customer/search', {
      params: {
        pageIndex,
        pageSize,
        strSearch,
        sortColumn,
        sortDirection
      }
    }),

  // Tạo khách hàng mới
  create: (data) => api.post('/Customer/create', data),

  // Cập nhật khách hàng theo id
  update: (id, data) => api.put(`/Customer/update`, data, { params: { id } }),

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
  })
};