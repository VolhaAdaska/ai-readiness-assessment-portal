export const CategoryType = {
  Data: 0,
  Technology: 1,
  Process: 2,
  Security: 3,
  Governance: 4,
  TeamCapabilities: 5,
} as const;

export type CategoryType = (typeof CategoryType)[keyof typeof CategoryType];

export const categoryTypeLabels: Record<CategoryType, string> = {
  [CategoryType.Data]: 'Data',
  [CategoryType.Technology]: 'Technology',
  [CategoryType.Process]: 'Process',
  [CategoryType.Security]: 'Security',
  [CategoryType.Governance]: 'Governance',
  [CategoryType.TeamCapabilities]: 'Team',
};

export function isCategoryType(value: number): value is CategoryType {
  return Object.values(CategoryType).includes(value as CategoryType);
}

export function getCategoryTypeLabel(category: CategoryType): string {
  return categoryTypeLabels[category] ?? `Category ${category}`;
}

export const AssessmentStatus = {
  NotStarted: 0,
  InProgress: 1,
  Completed: 2,
  Archived: 3,
} as const;

export type AssessmentStatus = (typeof AssessmentStatus)[keyof typeof AssessmentStatus];

export const assessmentStatusLabels: Record<AssessmentStatus, string> = {
  [AssessmentStatus.NotStarted]: 'Not Started',
  [AssessmentStatus.InProgress]: 'In Progress',
  [AssessmentStatus.Completed]: 'Completed',
  [AssessmentStatus.Archived]: 'Archived',
};

export function getAssessmentStatusLabel(status: AssessmentStatus): string {
  return assessmentStatusLabels[status] ?? `Status ${status}`;
}

export const ReadinessLevel = {
  Critical: 0,
  Low: 1,
  Moderate: 2,
  Good: 3,
  Excellent: 4,
} as const;

export type ReadinessLevel = (typeof ReadinessLevel)[keyof typeof ReadinessLevel];

export const readinessLevelLabels: Record<ReadinessLevel, string> = {
  [ReadinessLevel.Critical]: 'Critical',
  [ReadinessLevel.Low]: 'Low',
  [ReadinessLevel.Moderate]: 'Moderate',
  [ReadinessLevel.Good]: 'Good',
  [ReadinessLevel.Excellent]: 'Excellent',
};

export function getReadinessLevelLabel(level: ReadinessLevel): string {
  return readinessLevelLabels[level] ?? `Level ${level}`;
}

export const Priority = {
  Critical: 0,
  High: 1,
  Medium: 2,
  Low: 3,
} as const;

export type Priority = (typeof Priority)[keyof typeof Priority];

export const priorityLabels: Record<Priority, string> = {
  [Priority.Critical]: 'Critical',
  [Priority.High]: 'High',
  [Priority.Medium]: 'Medium',
  [Priority.Low]: 'Low',
};

export function getPriorityLabel(priority: Priority): string {
  return priorityLabels[priority] ?? `Priority ${priority}`;
}

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

export interface AssessmentSummary {
  assessmentId: string;
  organizationId: string;
  status: AssessmentStatus;
  createdAt: string;
  startedAt?: string;
  completedAt?: string;
  readinessLevel?: ReadinessLevel;
  overallScore?: number;
  totalCategories: number;
  completedCategories: number;
  completionPercentage: number;
}