export interface Event {
  id: number;
  title: string;
  description: string;
  sportId: number;
  startDate: string;
  endDate: string;
  location: string;
  status: EventStatus;
  maxParticipants: number;
  currentParticipants: number;
  organizerId: number;
  createdAt: string;
  updatedAt: string;
}

export type EventStatus =
  | "scheduled"
  | "in_progress"
  | "completed"
  | "cancelled";

export interface EventFilters {
  search?: string;
  sportId?: number;
  status?: EventStatus;
  startDate?: string;
  endDate?: string;
}

export interface EventFormData {
  title: string;
  description: string;
  sportId: number;
  startDate: string;
  endDate: string;
  location: string;
  maxParticipants: number;
  status: EventStatus;
}
