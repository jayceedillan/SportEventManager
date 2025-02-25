import { SportCategory } from "@/types/sportCategory";
import { Pagination } from "../common/Pagination";
import { Loading } from "../common/Loading";
import { FaEdit, FaTrash } from "react-icons/fa";

interface PaginationProps {
  pageNumber: number;
  totalPages: number;
  totalItems: number;
  pageSize: number;
  onPageChange: (page: number) => void;
}

interface SportCategoryListProps {
  sportsCategory?: SportCategory[];
  isLoading: boolean;
  pagination: PaginationProps;
  handleDelete: (id: number) => void;
  handleEdit: (id: number) => void;
}

export const SportCategoryList: React.FC<SportCategoryListProps> = ({
  sportsCategory,
  isLoading,
  pagination,
  handleDelete,
  handleEdit,
}) => {
  if (isLoading) {
    return <Loading />;
  }

  if (!sportsCategory?.length) {
    return (
      <div className="text-center py-8 text-gray-500">No sports found</div>
    );
  }

  return (
    <div className="space-y-6">
      <div className="flex flex-col space-y-4">
        <div className="flex w-full font-semibold text-gray-600">
          <div className="flex-1 p-2">Sport Name</div>
          <div className="flex-1 p-2">Description</div>
        </div>
        {sportsCategory.map((sportCategory) => (
          <div
            key={sportCategory.id}
            className="flex w-full border-t border-gray-200 py-2"
          >
            <div className="flex-1 p-2">{sportCategory.name}</div>
            <div className="flex-1 p-2">{sportCategory.description}</div>
            <div className="w-24 p-2 flex space-x-2">
              <button
                onClick={() => handleEdit(sportCategory.id)}
                className="p-1.5 text-blue-600 hover:text-blue-800 transition-colors"
                title="Edit"
              >
                <FaEdit size={18} />
              </button>
              <button
                onClick={() => handleDelete(sportCategory.id)}
                className="p-1.5 text-red-600 hover:text-red-800 transition-colors"
                title="Delete"
              >
                <FaTrash size={18} />
              </button>
            </div>
          </div>
        ))}
      </div>
      <Pagination
        currentPage={pagination.pageNumber}
        totalPages={pagination.totalPages}
        onPageChange={pagination.onPageChange}
      />
    </div>
  );
};
