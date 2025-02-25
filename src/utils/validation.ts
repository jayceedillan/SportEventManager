import * as z from "zod";
import { EventStatus } from "@/types/event";

export const eventSchema = z
  .object({
    title: z.string().min(3, "Title must be at least 3 characters"),
    description: z
      .string()
      .min(10, "Description must be at least 10 characters"),
    sportId: z.number().positive("Please select a sport"),
    startDate: z.string().refine((date) => new Date(date) > new Date(), {
      message: "Start date must be in the future",
    }),
    endDate: z.string(),
    location: z.string().min(3, "Location is required"),
    maxParticipants: z.number().min(2, "Minimum 2 participants required"),
    status: z.enum([
      "scheduled",
      "in_progress",
      "completed",
      "cancelled",
    ] as const),
  })
  .refine((data) => new Date(data.endDate) > new Date(data.startDate), {
    message: "End date must be after start date",
    path: ["endDate"],
  });

export const sportSchema = z
  .object({
    name: z
      .string()
      .min(1, "Name is required")
      .max(100, "Name cannot exceed 100 characters"),
    description: z.string().min(1, "Description is required"),
    rules: z
      .string()
      .min(1, "Rules are required")
      .max(500, "Description cannot exceed 500 characters"),
    minPlayers: z
      .number({ invalid_type_error: "Minimum players must be a number" })
      .min(1, "Minimum 1 player required"),
    maxPlayers: z
      .number({ invalid_type_error: "Maximum players must be a number" })
      .min(1, "Minimum 1 player required"),
    isActive: z.boolean(),
    imageUrl: z.string().url().optional(),
  })
  .refine((data) => data.maxPlayers >= data.minPlayers, {
    message: "Maximum players must be greater than or equal to minimum players",
    path: ["maxPlayers"],
  });

export const validateEmail = (email: string): boolean => {
  const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;
  return emailRegex.test(email);
};

export const validatePassword = (
  password: string
): {
  isValid: boolean;
  errors: string[];
} => {
  const errors: string[] = [];

  if (password.length < 8) {
    errors.push("Password must be at least 8 characters");
  }
  if (!/[A-Z]/.test(password)) {
    errors.push("Password must contain at least one uppercase letter");
  }
  if (!/[a-z]/.test(password)) {
    errors.push("Password must contain at least one lowercase letter");
  }
  if (!/[0-9]/.test(password)) {
    errors.push("Password must contain at least one number");
  }
  if (!/[!@#$%^&*]/.test(password)) {
    errors.push("Password must contain at least one special character");
  }

  return {
    isValid: errors.length === 0,
    errors,
  };
};

export const sportCategorySchema = z.object({
  name: z
    .string()
    .min(1, "Name is required")
    .max(100, "Name cannot exceed 100 characters"),
  description: z.string().min(1, "Description is required"),
  iconUrl: z.string().url().optional(),
});
