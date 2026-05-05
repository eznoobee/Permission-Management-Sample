import api from './axiosInstance';
import type { Governorate } from '../types/governorate';

export const getGovernorates = (): Promise<Governorate[]> =>
  api.get('/governorates').then((r) => r.data);
