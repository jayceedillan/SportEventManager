interface SportFiltersProps {
  onFilterChange: (filters: SportFilters) => void;
  currentFilters: SportFilters;
  onColumnChange: (filters: SportFilters) => void;
  onSortDirectionChange: (filters: SportFilters) => void;
}

export function SportFilters({
  onFilterChange,
  currentFilters,
  onColumnChange,
  onSortDirectionChange,
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
      <div className="flex gap-4 items-center">
        <select
          value={currentFilters.sortBy}
          onChange={onColumnChange}
          className="border rounded p-2"
        >
          <option value="">Sort by</option>
          {currentFilters.sortedColumns.map((option) => (
            <option key={option.value} value={option.value}>
              {option.name}
            </option>
          ))}
        </select>

        <select
          value={currentFilters.sortByValue}
          onChange={onSortDirectionChange}
          className="border rounded p-2"
        >
          <option value="asc">Ascending</option>
          <option value="desc">Descending</option>
        </select>
      </div>
    </div>
  );
}
