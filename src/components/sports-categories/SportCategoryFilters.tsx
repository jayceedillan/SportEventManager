interface SportCategoryFiltersProps {
  onFilterChange: (filters: SportCategoryFilters) => void;
  currentFilters: SportCategoryFilters;
}

export function SportCategoryFilters({
  onFilterChange,
  currentFilters,
}: SportCategoryFiltersProps) {
  return (
    <div className="flex gap-4 p-4 bg-white rounded shadow">
      <input
        type="text"
        placeholder="Search sports category..."
        value={currentFilters.searchTerm}
        onChange={(e) =>
          onFilterChange({
            ...currentFilters,
            searchTerm: e.target.value,
          })
        }
        className="border rounded p-2"
      />
    </div>
  );
}
