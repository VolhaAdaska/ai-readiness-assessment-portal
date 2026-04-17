export const CategoryType = {
  Data: 'Data',
  Technology: 'Technology',
  Process: 'Process',
  Security: 'Security',
  Governance: 'Governance',
  TeamCapabilities: 'TeamCapabilities',
} as const;

export type CategoryType = (typeof CategoryType)[keyof typeof CategoryType];

export const AssessmentStatus = {
  NotStarted: 'NotStarted',
  InProgress: 'InProgress',
  Completed: 'Completed',
  Archived: 'Archived',
} as const;

export type AssessmentStatus = (typeof AssessmentStatus)[keyof typeof AssessmentStatus];

export const ReadinessLevel = {
  Critical: 'Critical',
  Low: 'Low',
  Moderate: 'Moderate',
  Good: 'Good',
  Excellent: 'Excellent',
} as const;

export type ReadinessLevel = (typeof ReadinessLevel)[keyof typeof ReadinessLevel];

export const Priority = {
  Critical: 'Critical',
  High: 'High',
  Medium: 'Medium',
  Low: 'Low',
} as const;

export type Priority = (typeof Priority)[keyof typeof Priority];

export interface CategoryAssessment {
  id: string;
  category: CategoryType;
  maturityLevel: number;
  observations: string;
  categoryScore: number;
  isComplete: boolean;
}

export interface Recommendation {
  id: string;
  category: CategoryType;
  priority: Priority;
  title: string;
  description: string;
  createdAt: string;
}

export interface Assessment {
  id: string;
  organizationId: string;
  status: AssessmentStatus;
  createdAt: string;
  startedAt?: string;
  completedAt?: string;
  overallScore: number;
  readinessLevel?: ReadinessLevel;
  categories: CategoryAssessment[];
  recommendations: Recommendation[];
}

export interface CreateAssessmentRequest {
  organizationId: string;
}

export interface CreateAssessmentResponse {
  assessmentId: string;
  status: AssessmentStatus;
  createdAt: string;
}

export interface StartAssessmentResponse {
  assessmentId: string;
  status: AssessmentStatus;
  startedAt: string;
  initializedCategories: CategoryAssessment[];
}

export interface UpdateCategoryRequest {
  maturityLevel: number;
  observations: string;
}

export interface UpdateCategoryResponse {
  assessmentId: string;
  category: CategoryType;
  maturityLevel: number;
  observations: string;
  categoryScore: number;
  updatedAt: string;
}

export interface CompleteAssessmentResponse {
  assessmentId: string;
  status: AssessmentStatus;
  completedAt: string;
  overallScore: number;
  readinessLevel: ReadinessLevel;
  categoryScores: Record<CategoryType, number>;
  recommendationCount: number;
}