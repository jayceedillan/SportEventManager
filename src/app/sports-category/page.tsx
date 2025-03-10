"use client";

import { Button } from "@/components/common/Button";
import { ConfirmModal } from "@/components/common/ConfirmModal";
import { SportCategoryFilters } from "@/components/sports-categories/SportCategoryFilters";
import { SportCategoryList } from "@/components/sports-categories/SportCategoryList";
import { useFilters } from "@/hooks/useFilters"; // Ensure this exists
import { useModalState } from "@/hooks/useModalState";
import {
  useDeleteSportCategoryMutation,
  useGetSportCategoryQuery,
  type PaginationFilterDto,
} from "@/store/services/sportCategoryApi";
import { showToast } from "@/utils/toast";
import { useRouter } from "next/navigation";

export default function SportsCategoryPage() {
  const router = useRouter();

  const { filters, page, pageSize, setPage, handleFilterChange } = useFilters();
  const { modalState, openModal, closeModal } = useModalState();

  const queryParams: PaginationFilterDto = {
    page,
    pageSize,
    searchTerm: filters.searchTerm,
    sortBy: filters.sortBy,
    sortDescending: filters.sortDescending,
  };

  const { data, isLoading, error } = useGetSportCategoryQuery(queryParams);
  const [deleteSportCategory] = useDeleteSportCategoryMutation();

  const handleDelete = (id: number) => openModal(id);

  const handleConfirm = async () => {
    if (!modalState.itemId) return;

    const loadingToastId = showToast.loading("Deleting sport category...");

    try {
      await deleteSportCategory(modalState.itemId).unwrap();
      showToast.success("Sport category deleted successfully");
    } catch (error) {
      showToast.dismiss(loadingToastId);
      showToast.error("Failed to delete sport category");
      console.error("Delete error:", error);
    } finally {
      closeModal();
    }
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
        onClose={closeModal}
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
