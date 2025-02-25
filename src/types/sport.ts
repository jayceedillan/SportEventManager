import { number } from "zod";

export interface Sport {
  id: number;
  name: string;
  description: string;
  rules: string;
  minPlayers: number;
  maxPlayers: number;
  categoryId: number;
  isActive: boolean;
  imageUrl?: string;
  createdAt: string;
  updatedAt: string;
}

export interface SportFilters {
  search?: string;
  isActive?: boolean;
}

export interface SportFormData {
  name: string;
  description: string;
  rules: string;
  minPlayers: number;
  maxPlayers: number;
  isActive: boolean;
  imageUrl?: string;
}
