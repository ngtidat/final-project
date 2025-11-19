import { createRouter, createWebHistory } from 'vue-router';
import Customer from '../views/customer/Customer.vue';

const routes = [
    {
        path: '/',
        redirect: '/customer'
    },
    {
        path: '/customer',
        name: 'customer',
        component: Customer
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;