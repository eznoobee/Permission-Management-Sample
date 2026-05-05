import api from './axiosInstance';
import type { MonthlyOrder, SafetyOrder, YearlyOrder } from '../types/orders';

export const getYearlyOrders = (): Promise<YearlyOrder[]> =>
  api.get('/orders/yearly').then((r) => r.data);

export const getMonthlyOrders = (): Promise<MonthlyOrder[]> =>
  api.get('/orders/monthly').then((r) => r.data);

export const getSafetyOrders = (): Promise<SafetyOrder[]> =>
  api.get('/orders/safety').then((r) => r.data);
