import { createApp } from 'vue'
import './style.css'
import '@/assets/css/common.css'
import '@/assets/css/icons.css'
import router from './router'
import App from './App.vue'

createApp(App)
    .use(router)
    .mount('#app')
