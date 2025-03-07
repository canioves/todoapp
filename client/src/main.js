import './assets/main.css'
import 'bulma/css/bulma.min.css';

import { createApp } from 'vue'
import App from './App.vue'
import axios from 'axios'

const app = createApp(App)

const http = axios.create({ baseURL: 'http://localhost:5079/' })

app.provide('$http', http)

app.mount('#app')
