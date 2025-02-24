import { Event } from "@/types/event";
import { Button } from "../common/Button";
import { useRouter } from "next/navigation";
import { formatDate } from "@/utils/date";

interface EventDetailsProps {
  event: Event;
}

export const EventDetails: React.FC<EventDetailsProps> = ({ event }) => {
  const router = useRouter();

  const getStatusStyle = (status: Event["status"]) => {
    const styles = {
      scheduled: "bg-blue-100 text-blue-800",
      in_progress: "bg-green-100 text-green-800",
      completed: "bg-gray-100 text-gray-800",
      cancelled: "bg-red-100 text-red-800",
    };
    return styles[status];
  };

  return (
    <div className="max-w-3xl mx-auto p-6 bg-white rounded-lg shadow-md">
      <div className="flex justify-between items-start mb-6">
        <h1 className="text-2xl font-bold">{event.title}</h1>
        <span
          className={`px-3 py-1 rounded-full text-sm ${getStatusStyle(
            event.status
          )}`}
        >
          {event.status.replace("_", " ").toUpperCase()}
        </span>
      </div>

      <div className="space-y-6">
        <div>
          <h2 className="text-lg font-semibold mb-2">Description</h2>
          <p className="text-gray-600">{event.description}</p>
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div>
            <h2 className="text-lg font-semibold mb-2">Start Date</h2>
            <p className="text-gray-600">{formatDate(event.startDate)}</p>
          </div>
          <div>
            <h2 className="text-lg font-semibold mb-2">End Date</h2>
            <p className="text-gray-600">{formatDate(event.endDate)}</p>
          </div>
        </div>

        <div>
          <h2 className="text-lg font-semibold mb-2">Location</h2>
          <p className="text-gray-600">{event.location}</p>
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div>
            <h2 className="text-lg font-semibold mb-2">Maximum Participants</h2>
            <p className="text-gray-600">{event.maxParticipants}</p>
          </div>
          <div>
            <h2 className="text-lg font-semibold mb-2">Current Participants</h2>
            <p className="text-gray-600">{event.currentParticipants}</p>
          </div>
        </div>

        <div>
          <h2 className="text-lg font-semibold mb-2">Additional Information</h2>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <p className="text-sm text-gray-500">Created At</p>
              <p className="text-gray-600">{formatDate(event.createdAt)}</p>
            </div>
            <div>
              <p className="text-sm text-gray-500">Last Updated</p>
              <p className="text-gray-600">{formatDate(event.updatedAt)}</p>
            </div>
          </div>
        </div>

        <div className="flex space-x-4 mt-8">
          {event.status !== "completed" && event.status !== "cancelled" && (
            <Button
              onClick={() => router.push(`/events/edit/${event.id}`)}
              variant="primary"
            >
              Edit Event
            </Button>
          )}
          <Button onClick={() => router.push("/events")} variant="outline">
            Back to Events
          </Button>
        </div>
      </div>
    </div>
  );
};
