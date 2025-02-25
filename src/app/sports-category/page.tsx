"use client";

import { useState } from "react";
import { SportCategoryList } from "@/components/sports-categories/SportCategoryList";
import { SportCategoryFilters } from "@/components/sports-categories/SportCategoryFilters";
import { Button } from "@/components/common/Button";
import { useRouter } from "next/navigation";
import { usePagination } from "@/hooks/usePagination";
import {
  useGetSportCategoryQuery,
  useDeleteSportCategoryMutation,
  PaginationFilterDto,
} from "@/store/services/sportCategoryApi";
import { toast } from "react-hot-toast";

interface SportCategoryFilters {
  searchTerm: string;
  isActive: boolean;
  sortBy?: string;
  sortDescending: true | false;
  sortByValue?: "asc" | "desc";
}

export default function SportsCategoryPage() {
  const router = useRouter();
  const [filters, setFilters] = useState<SportCategoryFilters>({
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

  const { data, isLoading, error } = useGetSportCategoryQuery(queryParams);
  const [deleteSportCategory] = useDeleteSportCategoryMutation();

  const handleFilterChange = (newFilters: SportCategoryFilters) => {
    setFilters(newFilters);
    resetPagination();
  };

  const handleDelete = async (id: number) => {
    const confirmed = window.confirm(
      "Are you sure you want to delete this sport category?"
    );

    if (confirmed) {
      // Show loading state
      // if (isDeleting) return; // Prevent multiple clicks while deleting
      debugger;
      await deleteSportCategory(id).unwrap();

      // Optional: Show success message
      toast.success("Sport category deleted successfully");
      // Or if you're not using toast library
      // alert('Sport category deleted successfully');

      // Optionally refresh the data or handle the UI update
      // If you're using React Query or RTK Query, this might be handled automatically
    }
  };

  const handlePageChange = (newPage: number) => {
    setPage(newPage);
  };

  return (
    <div className="space-y-6 p-8">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold">Sports Category</h1>
        <Button
          onClick={() => router.push("/sports-category/create")}
          variant="primary"
          className="bg-cyan-500 text-white hover:!bg-blue-600"
        >
          Create Sport Category
        </Button>
      </div>

      <SportCategoryFilters
        onFilterChange={handleFilterChange}
        currentFilters={filters}
      />

      {error && (
        <div className="text-red-500 p-4 bg-red-50 rounded">
          Error loading sports. Please try again later.
        </div>
      )}

      <SportCategoryList
        sportsCategory={data?.items}
        isLoading={isLoading}
        pagination={{
          pageNumber: page,
          totalPages: data?.totalPages || 1,
          onPageChange: handlePageChange,
        }}
        handleEdit={(id: number) => {
          // Handle edit logic here
          alert("xxx");
          console.log("Editing category with id:", id);
        }}
        handleDelete={handleDelete}
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
