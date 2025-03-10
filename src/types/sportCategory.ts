export interface SportCategory {
  id: number;
  name: string;
  description: string;
  iconUrl: string;
  createdAt: string;
  updatedAt: string;
}

export interface SportCategoryFilters {
  search?: string;
  isActive?: boolean;
}

export interface SportCategoryFormData {
  name: string;
  description: string;
  iconUrl: string;
}
