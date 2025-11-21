import api from './api.js'; 

export const customerTypeService = {
  getAll: () => api.get('/CustomerType'),
};