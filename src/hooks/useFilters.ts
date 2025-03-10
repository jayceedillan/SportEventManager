import { usePagination } from "@/hooks/usePagination";
import { CommonFilters } from "@/types/common";
import { useState } from "react";

const initialFilters: CommonFilters = {
  searchTerm: "",
  isActive: true,
  sortDescending: false,
  sortedColumns: [],
};

export function useFilters<T extends CommonFilters>(defaultFilters?: T) {
  const [filters, setFilters] = useState<T>(
    defaultFilters || (initialFilters as T)
  );
  const { page, pageSize, setPage, resetPagination } = usePagination();

  // const handleFilterChange = (newFilters: T) => {
  //   setFilters(newFilters);
  //   resetPagination();
  // };

  const handleFilterChange = (newFilters: Partial<T>) => {
    setFilters((prevFilters) => ({
      ...prevFilters,
      ...newFilters,
    }));
    resetPagination();
  };

  return {
    filters,
    setFilters,
    page,
    pageSize,
    setPage,
    resetPagination,
    handleFilterChange,
  };
}
