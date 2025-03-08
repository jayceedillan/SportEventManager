"use client";

import { useState } from "react";
import { SportList } from "@/components/sports/SportList";
import { SportFilters } from "@/components/sports/SportFilters";
import { Button } from "@/components/common/Button";
import { useRouter } from "next/navigation";
import { usePagination } from "@/hooks/usePagination";
import {
  useGetSportsQuery,
  PaginationFilterDto,
} from "@/store/services/sportApi";

interface SportFilters {
  searchTerm: string;
  isActive: boolean;
  sortBy?: string;
  sortDescending: true | false;
  sortByValue?: "asc" | "desc";
}

export default function SportsPage() {
  const router = useRouter();
  const [filters, setFilters] = useState<SportFilters>({
    searchTerm: "",
    isActive: true,
    sortDescending: false,
  });

  // Initialize pagination with your hook
  const { page, pageSize, setPage, resetPagination } = usePagination();

  // Combine filters and pagination into query parameters
  const queryParams: PaginationFilterDto = {
    page,
    pageSize,
    searchTerm: filters.searchTerm,
    sortBy: filters.sortBy,
    sortDescending: filters.sortDescending,
  };

  const { data, isLoading, error } = useGetSportsQuery(queryParams);

  const handleFilterChange = (newFilters: SportFilters) => {
    setFilters(newFilters);
    resetPagination();
  };

  const handlePageChange = (newPage: number) => {
    setPage(newPage);
  };

  return (
    <div className="space-y-6 p-8">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold">Sports</h1>
        <Button
          onClick={() => router.push("/sports/create")}
          variant="primary"
          className="bg-cyan-500 text-white hover:!bg-blue-600"
        >
          Create Sport
        </Button>
      </div>

      <SportFilters
        onFilterChange={handleFilterChange}
        currentFilters={filters}
      />

      {error && (
        <div className="text-red-500 p-4 bg-red-50 rounded">
          Error loading sports. Please try again later.
        </div>
      )}

      <SportList
        sports={data?.items}
        isLoading={isLoading}
        pagination={{
          currentPage: page,
          totalPages: data?.totalPages || 1,
          onPageChange: handlePageChange,
        }}
      />

      {/* Sorting controls */}
      <div className="flex gap-4 items-center">
        <select
          value={filters.sortBy}
          onChange={(e) => {
            handleFilterChange({
              ...filters,
              sortBy: e.target.value,
            });
          }}
          className="border rounded p-2"
        >
          <option value="">Sort by</option>
          <option value="name">Name</option>
          <option value="Description">Description</option>
          <option value="minPlayers">Min Players</option>
          <option value="maxPlayers">Max Players</option>
        </select>

        <select
          value={filters.sortByValue}
          onChange={(e) => {
            handleFilterChange({
              ...filters,
              sortDescending: e.target.value !== "asc",
              sortByValue: e.target.value as "asc" | "desc",
            });
          }}
          className="border rounded p-2"
        >
          <option value="asc">Ascending</option>
          <option value="desc">Descending</option>
        </select>
      </div>
    </div>
  );
}
