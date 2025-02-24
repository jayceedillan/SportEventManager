import { useState } from "react";
import { PaginationFilter } from "@/types/common";

interface UsePaginationProps {
  initialPage?: number;
  initialPageSize?: number;
}

export const usePagination = ({
  initialPage = 1,
  initialPageSize = 10,
}: UsePaginationProps = {}) => {
  const [page, setPage] = useState(initialPage);
  const [pageSize, setPageSize] = useState(initialPageSize);

  const getPaginationFilter = (additionalFilters = {}): PaginationFilter => ({
    page,
    pageSize,
    ...additionalFilters,
  });

  const resetPagination = () => {
    setPage(1);
  };

  return {
    page,
    pageSize,
    setPage,
    setPageSize,
    getPaginationFilter,
    resetPagination,
  };
};
