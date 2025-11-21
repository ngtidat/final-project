import { createRouter, createWebHistory } from 'vue-router';
import Customer from '../views/customer/Customer.vue';
import CustomerForm from '../views/customer/CustomerForm.vue'

const routes = [
    { path: '/', redirect: '/customer' },
    { path: '/customer', name: 'customer', component: Customer },
    { path: '/customer/add', name: 'add-customer', component: CustomerForm },
    { path: '/customer/update/:id', name: 'update-customer', component: CustomerForm },
    { path: '/:pathMatch(.*)*', redirect: '/customer' }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;