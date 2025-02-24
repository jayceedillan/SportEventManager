import { Event, EventStatus } from "@/types/event";
import { Button } from "../common/Button";
import { useRouter } from "next/navigation";
import {
  FiEdit2,
  FiTrash2,
  FiCalendar,
  FiMapPin,
  FiUsers,
} from "react-icons/fi";
import { useDeleteEventMutation } from "@/store/services/eventApi";
import { toast } from "react-hot-toast";
import { formatDate } from "@/utils/date";

interface EventCardProps {
  event: Event;
}

export const EventCard: React.FC<EventCardProps> = ({ event }) => {
  const router = useRouter();
  const [deleteEvent] = useDeleteEventMutation();

  const getStatusStyle = (status: EventStatus) => {
    const styles = {
      scheduled: "bg-blue-100 text-blue-800",
      in_progress: "bg-green-100 text-green-800",
      completed: "bg-gray-100 text-gray-800",
      cancelled: "bg-red-100 text-red-800",
    };
    return styles[status];
  };

  const handleDelete = async () => {
    if (confirm("Are you sure you want to delete this event?")) {
      try {
        await deleteEvent(event.id).unwrap();
        toast.success("Event deleted successfully");
      } catch (error) {
        toast.error("Failed to delete event");
      }
    }
  };

  return (
    <div className="bg-white rounded-lg shadow-md p-6">
      <div className="flex justify-between items-start">
        <h3 className="text-lg font-semibold">{event.title}</h3>
        <span
          className={`px-2 py-1 rounded-full text-xs ${getStatusStyle(
            event.status
          )}`}
        >
          {event.status.replace("_", " ").toUpperCase()}
        </span>
      </div>

      <p className="mt-2 text-gray-600 text-sm line-clamp-2">
        {event.description}
      </p>

      <div className="mt-4 space-y-2">
        <div className="flex items-center text-sm text-gray-500">
          <FiCalendar className="mr-2" />
          <span>
            {formatDate(event.startDate)} - {formatDate(event.endDate)}
          </span>
        </div>

        <div className="flex items-center text-sm text-gray-500">
          <FiMapPin className="mr-2" />
          <span>{event.location}</span>
        </div>

        <div className="flex items-center text-sm text-gray-500">
          <FiUsers className="mr-2" />
          <span>
            {event.currentParticipants} / {event.maxParticipants} participants
          </span>
        </div>
      </div>

      <div className="mt-6 flex space-x-3">
        <Button
          variant="outline"
          size="sm"
          onClick={() => router.push(`/events/${event.id}`)}
        >
          View Details
        </Button>
        <Button
          variant="outline"
          size="sm"
          onClick={() => router.push(`/events/edit/${event.id}`)}
        >
          <FiEdit2 className="w-4 h-4" />
        </Button>
        <Button variant="danger" size="sm" onClick={handleDelete}>
          <FiTrash2 className="w-4 h-4" />
        </Button>
      </div>
    </div>
  );
};
