import {
  CategoryType,
} from '../types/assessment';
import type {
  Assessment,
  CreateAssessmentRequest,
  CreateAssessmentResponse,
  StartAssessmentResponse,
  UpdateCategoryRequest,
  UpdateCategoryResponse,
  CompleteAssessmentResponse,
} from '../types/assessment';

const API_BASE_URL = (import.meta.env.VITE_API_BASE_URL as string | undefined) ?? '/api';

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    headers: {
      'Content-Type': 'application/json',
      ...(init?.headers ?? {}),
    },
    ...init,
  });

  if (!response.ok) {
    const fallback = `Request failed with status ${response.status}`;
    try {
      const errorBody = (await response.json()) as { message?: string; title?: string };
      throw new Error(errorBody.message ?? errorBody.title ?? fallback);
    } catch {
      throw new Error(fallback);
    }
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return (await response.json()) as T;
}

export const assessmentApi = {
  create: async (payload: CreateAssessmentRequest): Promise<CreateAssessmentResponse> => {
    return request<CreateAssessmentResponse>('/assessments', {
      method: 'POST',
      body: JSON.stringify(payload),
    });
  },

  start: async (id: string): Promise<StartAssessmentResponse> => {
    return request<StartAssessmentResponse>(`/assessments/${id}/start`, {
      method: 'POST',
    });
  },

  get: async (id: string): Promise<Assessment> => {
    return request<Assessment>(`/assessments/${id}`);
  },

  updateCategory: async (
    id: string,
    category: CategoryType,
    payload: UpdateCategoryRequest
  ): Promise<UpdateCategoryResponse> => {
    return request<UpdateCategoryResponse>(
      `/assessments/${id}/categories/${category}`,
      {
        method: 'PUT',
        body: JSON.stringify(payload),
      }
    );
  },

  complete: async (id: string): Promise<CompleteAssessmentResponse> => {
    return request<CompleteAssessmentResponse>(`/assessments/${id}/complete`, {
      method: 'POST',
    });
  },
};