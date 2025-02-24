import { useDebounce } from "@/hooks/useDebounce";
import { SearchInput } from "../common/SearchInput";
import { Select } from "../common/Select";
import { useState } from "react";

interface SportFiltersProps {
  onFilterChange: (filters: any) => void;
}

export const SportFilters: React.FC<SportFiltersProps> = ({
  onFilterChange,
}) => {
  const [search, setSearch] = useState("");
  const debouncedSearch = useDebounce(search, 500);

  const handleSearchChange = (value: string) => {
    setSearch(value);
  };

  const handleStatusChange = (value: string) => {
    onFilterChange((prev) => ({
      ...prev,
      isActive: value === "active",
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
        placeholder="Search sports..."
        className="w-64"
      />

      <Select
        options={[
          { value: "all", label: "All Status" },
          { value: "active", label: "Active" },
          { value: "inactive", label: "Inactive" },
        ]}
        onChange={handleStatusChange}
        className="w-40"
      />
    </div>
  );
};
