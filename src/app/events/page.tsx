"use client";

import { useState } from "react";
import { EventList } from "@/components/events/EventList";
import { EventFilters } from "@/components/events/EventFilters";
import { Button } from "@/components/common/Button";
import { useRouter } from "next/navigation";
import { usePagination } from "@/hooks/usePagination";
import { useGetEventsQuery } from "@/store/services/eventApi";

export default function EventsPage() {
  const router = useRouter();
  const [filters, setFilters] = useState({});
  const { page, pageSize, setPage } = usePagination();

  const { data, isLoading } = useGetEventsQuery({
    page,
    pageSize,
    ...filters,
  });

  return (
    <div className="space-y-6 p-8">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold">Events</h1>
        <Button onClick={() => router.push("/events/create")} variant="primary">
          Create Event
        </Button>
      </div>

      <EventFilters onFilterChange={setFilters} />

      <EventList
        events={data?.items}
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
