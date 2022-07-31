import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  base: "",
  build: {
    outDir: '../../Server/resources/roleplay/client/view',
    emptyOutDir: true,
  },
  plugins: [react()]
})
