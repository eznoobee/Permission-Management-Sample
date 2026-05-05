import { create } from 'zustand';
import { persist } from 'zustand/middleware';

interface AuthState {
  token: string | null;
  adminId: number | null;
  name: string | null;
  email: string | null;
  setAuth: (token: string, adminId: number, name: string, email: string) => void;
  clearAuth: () => void;
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      token: null,
      adminId: null,
      name: null,
      email: null,
      setAuth: (token, adminId, name, email) => set({ token, adminId, name, email }),
      clearAuth: () => set({ token: null, adminId: null, name: null, email: null }),
    }),
    { name: 'auth' }
  )
);
