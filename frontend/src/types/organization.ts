export interface Organization {
  id: string;
  name: string;
  createdAt: string;
}

export interface CreateOrganizationRequest {
  name: string;
}
