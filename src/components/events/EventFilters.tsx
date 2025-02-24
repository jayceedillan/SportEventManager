import { useDebounce } from "@/hooks/useDebounce";
import { SearchInput } from "../common/SearchInput";
import { Select } from "../common/Select";
import { useState } from "react";
import { EventStatus } from "@/types/event";

interface EventFiltersProps {
  onFilterChange: (filters: any) => void;
}

export const EventFilters: React.FC<EventFiltersProps> = ({
  onFilterChange,
}) => {
  const [search, setSearch] = useState("");
  const debouncedSearch = useDebounce(search, 500);

  const handleSearchChange = (value: string) => {
    setSearch(value);
  };

  const handleStatusChange = (value: EventStatus) => {
    onFilterChange((prev) => ({
      ...prev,
      status: value,
    }));
  };

  const handleDateRangeChange = (value: string) => {
    onFilterChange((prev) => ({
      ...prev,
      dateRange: value,
    }));
  };

  // Update filters when debounced search changes
  React.useEffect(() => {
    onFilterChange((prev) => ({
      ...prev,
      search: debouncedSearch,
    }));
  }, [debouncedSearch, onFilterChange]);

  return (
    <div className="flex space-x-4">
      <SearchInput
        value={search}
        onChange={handleSearchChange}
        placeholder="Search events..."
        className="w-64"
      />

      <Select
        options={[
          { value: "", label: "All Status" },
          { value: "scheduled", label: "Scheduled" },
          { value: "in_progress", label: "In Progress" },
          { value: "completed", label: "Completed" },
          { value: "cancelled", label: "Cancelled" },
        ]}
        onChange={handleStatusChange}
        className="w-40"
      />

      <Select
        options={[
          { value: "", label: "All Dates" },
          { value: "today", label: "Today" },
          { value: "upcoming", label: "Upcoming" },
          { value: "past", label: "Past" },
        ]}
        onChange={handleDateRangeChange}
        className="w-40"
      />
    </div>
  );
};
