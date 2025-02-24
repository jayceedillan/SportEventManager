import { Event } from "@/types/event";
import { EventCard } from "./EventCard";
import { Pagination } from "../common/Pagination";
import { Loading } from "../common/Loading";

interface EventListProps {
  events?: Event[];
  isLoading: boolean;
  pagination: {
    currentPage: number;
    totalPages: number;
    onPageChange: (page: number) => void;
  };
}

export const EventList: React.FC<EventListProps> = ({
  events,
  isLoading,
  pagination,
}) => {
  if (isLoading) {
    return <Loading />;
  }

  if (!events?.length) {
    return (
      <div className="text-center py-8 text-gray-500">No events found</div>
    );
  }

  return (
    <div className="space-y-6">
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {events.map((event) => (
          <EventCard key={event.id} event={event} />
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
