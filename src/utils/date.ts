import { format, parseISO, isValid } from "date-fns";

export const formatDate = (
  date: string | Date,
  formatString = "PP"
): string => {
  try {
    const dateObj = typeof date === "string" ? parseISO(date) : date;
    return isValid(dateObj) ? format(dateObj, formatString) : "Invalid date";
  } catch {
    return "Invalid date";
  }
};

export const formatDateTime = (date: string | Date): string => {
  return formatDate(date, "PPp");
};

export const isDateInPast = (date: string | Date): boolean => {
  const dateObj = typeof date === "string" ? parseISO(date) : date;
  return dateObj < new Date();
};

export const getDurationInHours = (
  startDate: string,
  endDate: string
): number => {
  const start = parseISO(startDate);
  const end = parseISO(endDate);
  return (end.getTime() - start.getTime()) / (1000 * 60 * 60);
};
