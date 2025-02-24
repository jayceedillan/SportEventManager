import { Sport } from "@/types/sport";
import { SportCard } from "./SportCard";
import { Pagination } from "../common/Pagination";
import { Loading } from "../common/Loading";

interface SportListProps {
  sports?: Sport[];
  isLoading: boolean;
  pagination: {
    currentPage: number;
    totalPages: number;
    onPageChange: (page: number) => void;
  };
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
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {sports.map((sport) => (
          <SportCard key={sport.id} sport={sport} />
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
