interface SportFiltersProps {
  onFilterChange: (filters: SportFilters) => void;
  currentFilters: SportFilters;
}

export function SportFilters({
  onFilterChange,
  currentFilters,
}: SportFiltersProps) {
  return (
    <div className="flex gap-4 p-4 bg-white rounded shadow">
      <input
        type="text"
        placeholder="Search sports..."
        value={currentFilters.searchTerm}
        onChange={(e) =>
          onFilterChange({
            ...currentFilters,
            searchTerm: e.target.value,
          })
        }
        className="border rounded p-2 flex-1"
      />

      <select
        value={currentFilters.isActive ? "active" : "inactive"}
        onChange={(e) =>
          onFilterChange({
            ...currentFilters,
            isActive: e.target.value === "active",
          })
        }
        className="border rounded p-2"
      >
        <option value="active">Active</option>
        <option value="inactive">Inactive</option>
      </select>
    </div>
  );
}
