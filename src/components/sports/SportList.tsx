import { Sport } from "@/types/sport";
import { Pagination } from "../common/Pagination";
import { Loading } from "../common/Loading";

interface PaginationProps {
  currentPage: number;
  totalPages: number;
  totalItems: number;
  pageSize: number;
  onPageChange: (page: number) => void;
}

interface SportListProps {
  sports?: Sport[];
  isLoading: boolean;
  pagination: PaginationProps;
}

export const SportList: React.FC<SportListProps> = ({
  sports,
  isLoading,
  pagination,
}) => {
  if (isLoading) {
    return <Loading />;
  }

  if (!sports?.length) {
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
          <div className="flex-1 p-2">Rules</div>
          <div className="flex-1 p-2">Min Players</div>
          <div className="flex-1 p-2">Max Players</div>
        </div>
        {/* Mapping over the sports and displaying each as a row */}
        {sports.map((sport) => (
          <div
            key={sport.id}
            className="flex w-full border-t border-gray-200 py-2"
          >
            <div className="flex-1 p-2">{sport.name}</div>
            <div className="flex-1 p-2">{sport.description}</div>
            <div className="flex-1 p-2">{sport.rules}</div>
            <div className="flex-1 p-2">{sport.minPlayers}</div>
            <div className="flex-1 p-2">{sport.maxPlayers}</div>
          </div>
        ))}
      </div>

      <Pagination
        currentPage={pagination.currentPage}
        totalPages={pagination.totalPages}
        onPageChange={pagination.onPageChange}
      />
    </div>
  );
};
