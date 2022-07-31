import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  build: {
    outDir: '../../Server/resources/roleplay/client/view'
  },
  plugins: [react()]
})
