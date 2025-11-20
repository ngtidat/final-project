// src/services/customerService.js
import api from './api.js'; // import axios instance đã cấu hình

export const customerService = {
  // Lấy danh sách tất cả khách hàng
  getAll: () => api.get('/Customer'),

  // Lấy 1 khách hàng theo id
  getById: (id) => api.get(`/Customer/${id}`),

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
  create: (data) => api.post('/Customer', data),

  // Cập nhật khách hàng theo id
  update: (id, data) => api.put(`/Customer/${id}`, data),

  // Xoá khách hàng theo id
  delete: (id) => api.delete(`/Customer/${id}`)
};