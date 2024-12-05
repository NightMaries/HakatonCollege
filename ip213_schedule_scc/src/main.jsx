import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import store from '../GlobalStore';
import './index.css'
import App from './App.jsx'
import { Provider } from 'mobx-react';

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <Provider {...store}>
    <App />
    </Provider>
  </StrictMode>
)
