"use client";

import { Button } from "@/components/common/Button";
import { ConfirmModal } from "@/components/common/ConfirmModal";
import { SportFilters } from "@/components/sports/SportFilters";
import { SportList } from "@/components/sports/SportList";
import { useFilters } from "@/hooks/useFilters";
import { useModalState } from "@/hooks/useModalState";
import {
  type PaginationFilterDto,
  useDeleteSportMutation,
  useGetSportsQuery,
} from "@/store/services/sportApi";
import { showToast } from "@/utils/toast";
import { debounce } from "lodash";
import { useRouter } from "next/navigation";
import { useCallback, useEffect, useMemo } from "react";

const useDebounceFilters = (filters: any, delay: number = 500) => {
  return useMemo(
    () => ({
      ...filters,
      searchTerm: filters.searchTerm,
      sortBy: filters.sortBy,
      sortDescending: filters.sortDescending,
    }),
    [filters.searchTerm, filters.sortBy, filters.sortDescending]
  );
};

export default function SportsPage() {
  const router = useRouter();
  const { modalState, openModal, closeModal } = useModalState();

  const { filters, page, pageSize, setPage, handleFilterChange } = useFilters({
    searchTerm: "",
    isActive: true,
    sortBy: "" as string,
    sortDescending: false as boolean,
    sortedColumns: [
      { name: "Description", value: "Description" },
      { name: "Min Players", value: "minPlayers" },
      { name: "Max Players", value: "maxPlayers" },
    ],
  });

  const debouncedFilters = useDebounceFilters(filters);

  const queryParams: PaginationFilterDto = useMemo(
    () => ({
      page,
      pageSize,
      searchTerm: debouncedFilters.searchTerm,
      sortBy: debouncedFilters.sortBy,
      sortDescending: debouncedFilters.sortDescending,
    }),
    [page, pageSize, debouncedFilters]
  );

  const debouncedHandleFilterChange = useCallback(
    debounce((newFilters: typeof filters) => {
      handleFilterChange(newFilters);
    }, 500),
    []
  );

  const { data, isLoading, error } = useGetSportsQuery(queryParams);
  const [deleteSport] = useDeleteSportMutation();

  const handleConfirmDelete = async () => {
    if (!modalState.itemId) return;

    const loadingToastId = showToast.loading("Deleting sport category...");
    try {
      await deleteSport(modalState.itemId).unwrap();
      showToast.success("Sport category deleted successfully");
    } catch (error) {
      showToast.dismiss(loadingToastId);
      showToast.error("Failed to delete sport category");
      console.error("Delete error:", error);
    } finally {
      closeModal();
    }
  };

  const handleSortChange = useCallback(
    (e: React.ChangeEvent<HTMLSelectElement>) => {
      const newFilters = { ...filters, sortBy: e.target.value };
      debouncedHandleFilterChange(newFilters);
    },
    [filters, debouncedHandleFilterChange]
  );

  const handleSortDirectionChange = useCallback(
    (e: React.ChangeEvent<HTMLSelectElement>) => {
      const newFilters = {
        ...filters,
        sortDescending: e.target.value !== "asc",
      };
      debouncedHandleFilterChange(newFilters);
    },
    [filters, debouncedHandleFilterChange]
  );

  useEffect(() => {
    return () => {
      debouncedHandleFilterChange.cancel();
    };
  }, [debouncedHandleFilterChange]);

  return (
    <div className="space-y-6 p-8">
      {/* Header with Create Sport button */}
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
        onColumnChange={handleSortChange}
        currentFilters={filters}
        onSortDirectionChange={handleSortDirectionChange}
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
          pageNumber: page,
          totalPages: data?.totalPages ?? 1,
          onPageChange: setPage,
        }}
        handleEdit={(id: number) => router.push(`/sports/edit/${id}`)}
        handleDelete={openModal}
      />

      {/* Confirmation Modal */}
      <ConfirmModal
        isOpen={modalState.isOpen}
        onClose={closeModal}
        onConfirm={handleConfirmDelete}
        title="Delete Confirmation"
        message="Are you sure you want to delete this item? This action cannot be undone."
        type="danger"
        confirmText="Delete"
        cancelText="Cancel"
      />
    </div>
  );
}
