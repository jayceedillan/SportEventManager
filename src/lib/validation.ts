import { z } from "zod";
import {
  eventSchema,
  sportSchema,
  sportCategorySchema,
} from "@/utils/validation";

export const validateFormData = <T>(
  schema: z.ZodSchema,
  data: unknown
): { success: true; data: T } | { success: false; errors: z.ZodError } => {
  try {
    const validData = schema.parse(data);
    return { success: true, data: validData as T };
  } catch (error) {
    if (error instanceof z.ZodError) {
      return { success: false, errors: error };
    }
    throw error;
  }
};

export const getValidationErrors = (
  error: z.ZodError
): Record<string, string> => {
  const errors: Record<string, string> = {};
  error.errors.forEach((err) => {
    if (err.path) {
      errors[err.path.join(".")] = err.message;
    }
  });
  return errors;
};

export { eventSchema, sportSchema, sportCategorySchema };
