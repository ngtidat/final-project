import { createApp } from 'vue'
import './style.css'
import '@/assets/css/common.css'
import '@/assets/css/icons.css'
import router from './router'
import App from './App.vue'
import { useToast } from './utils/toast'

const app = createApp(App)

const toast = useToast();
app.provide('toast', toast);

app.use(router);
app.mount('#app');
