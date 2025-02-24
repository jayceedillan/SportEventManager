"use client";

import { useState } from "react";
import { SportList } from "@/components/sports/SportList";
import { SportFilters } from "@/components/sports/SportFilters";
import { Button } from "@/components/common/Button";
import { useRouter } from "next/navigation";
import { usePagination } from "@/hooks/usePagination";
import { useGetSportsQuery } from "@/store/services/sportApi";

export default function SportsPage() {
  const router = useRouter();
  const [filters, setFilters] = useState({});
  const { page, pageSize, setPage } = usePagination();

  const { data, isLoading } = useGetSportsQuery({
    page,
    pageSize,
    ...filters,
  });

  return (
    <div className="space-y-6 p-8">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold">Sports</h1>
        <Button onClick={() => router.push("/sports/create")} variant="primary">
          Create Sport
        </Button>
      </div>

      <SportFilters onFilterChange={setFilters} />

      <SportList
        sports={data?.items}
        isLoading={isLoading}
        pagination={{
          currentPage: page,
          totalPages: data?.totalPages || 1,
          onPageChange: setPage,
        }}
      />
    </div>
  );
}
