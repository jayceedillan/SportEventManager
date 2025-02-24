import axios from "axios";
import { ApiResponse, ApiError } from "@/types/common";

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("auth_token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem("auth_token");
      window.location.href = "/login";
    }
    return Promise.reject(error);
  }
);

export const handleApiError = (error: unknown): ApiError => {
  if (axios.isAxiosError(error)) {
    return {
      message: error.response?.data?.message || "An unexpected error occurred",
      code: error.response?.data?.code || "UNKNOWN_ERROR",
      details: error.response?.data?.details,
    };
  }
  return {
    message: "An unexpected error occurred",
    code: "UNKNOWN_ERROR",
  };
};

export const get = async <T>(url: string): Promise<ApiResponse<T>> => {
  try {
    const response = await api.get<ApiResponse<T>>(url);
    return response.data;
  } catch (error) {
    throw handleApiError(error);
  }
};

export const post = async <T>(
  url: string,
  data: unknown
): Promise<ApiResponse<T>> => {
  try {
    const response = await api.post<ApiResponse<T>>(url, data);
    return response.data;
  } catch (error) {
    throw handleApiError(error);
  }
};

export const put = async <T>(
  url: string,
  data: unknown
): Promise<ApiResponse<T>> => {
  try {
    const response = await api.put<ApiResponse<T>>(url, data);
    return response.data;
  } catch (error) {
    throw handleApiError(error);
  }
};

export const del = async <T>(url: string): Promise<ApiResponse<T>> => {
  try {
    const response = await api.delete<ApiResponse<T>>(url);
    return response.data;
  } catch (error) {
    throw handleApiError(error);
  }
};

export default api;
