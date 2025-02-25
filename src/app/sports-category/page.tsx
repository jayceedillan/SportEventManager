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
  type PaginationFilterDto,
} from "@/store/services/sportCategoryApi";
import { toast } from "react-hot-toast";
import { ConfirmModal } from "@/components/common/ConfirmModal";
import { showToast } from "@/utils/toast";

interface SportCategoryFilters {
  searchTerm: string;
  isActive: boolean;
  sortBy?: string;
  sortDescending: boolean;
  sortByValue?: "asc" | "desc";
}

interface ModalState {
  isOpen: boolean;
  itemId: number | null;
}

const initialFilters: SportCategoryFilters = {
  searchTerm: "",
  isActive: true,
  sortDescending: false,
};

export default function SportsCategoryPage() {
  const router = useRouter();
  const [filters, setFilters] = useState<SportCategoryFilters>(initialFilters);
  const [modalState, setModalState] = useState<ModalState>({
    isOpen: false,
    itemId: null,
  });

  const { page, pageSize, setPage, resetPagination } = usePagination();

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

  const handleDelete = (id: number) => {
    setModalState({ isOpen: true, itemId: id });
  };

  const handleConfirm = async () => {
    if (!modalState.itemId) return;

    const loadingToastId = showToast.loading("Deleting sport category...");

    try {
      await deleteSportCategory(modalState.itemId).unwrap();
      debugger;
      showToast.dismiss(loadingToastId);
      showToast.success("Sport category deleted successfully");
    } catch (error) {
      showToast.dismiss(loadingToastId);
      showToast.error("Failed to delete sport category");
      console.error("Delete error:", error);
    } finally {
      setModalState({ isOpen: false, itemId: null });
    }
  };

  const handleCloseModal = () => {
    setModalState({ isOpen: false, itemId: null });
  };

  const handleSortChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    handleFilterChange({
      ...filters,
      sortBy: e.target.value,
    });
  };

  const handleSortDirectionChange = (
    e: React.ChangeEvent<HTMLSelectElement>
  ) => {
    handleFilterChange({
      ...filters,
      sortDescending: e.target.value !== "asc",
      sortByValue: e.target.value as "asc" | "desc",
    });
  };

  if (error) {
    return (
      <div className="text-red-500 p-4 bg-red-50 rounded">
        Error loading sports. Please try again later.
      </div>
    );
  }

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

      <SportCategoryList
        sportsCategory={data?.items}
        isLoading={isLoading}
        pagination={{
          pageNumber: page,
          totalPages: data?.totalPages ?? 1,
          onPageChange: setPage,
        }}
        handleEdit={(id: number) => router.push(`/sports-category/edit/${id}`)}
        handleDelete={handleDelete}
      />

      <div className="flex gap-4 items-center">
        <select
          value={filters.sortBy}
          onChange={handleSortChange}
          className="border rounded p-2"
        >
          <option value="">Sort by</option>
          <option value="name">Name</option>
          <option value="Description">Description</option>
        </select>

        <select
          value={filters.sortByValue}
          onChange={handleSortDirectionChange}
          className="border rounded p-2"
        >
          <option value="asc">Ascending</option>
          <option value="desc">Descending</option>
        </select>
      </div>

      <ConfirmModal
        isOpen={modalState.isOpen}
        onClose={handleCloseModal}
        onConfirm={handleConfirm}
        title="Delete Confirmation"
        message="Are you sure you want to delete this item? This action cannot be undone."
        type="danger"
        confirmText="Delete"
        cancelText="Cancel"
      />
    </div>
  );
}
