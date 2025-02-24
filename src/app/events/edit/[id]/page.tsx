"use client";

import { useParams, useRouter } from "next/navigation";
import { EventForm } from "@/components/events/EventForm";
import {
  useGetEventByIdQuery,
  useUpdateEventMutation,
} from "@/store/services/eventApi";
import { toast } from "react-hot-toast";
import { Loading } from "@/components/common/Loading";

export default function EditEventPage() {
  const { id } = useParams();
  const router = useRouter();
  const { data: event, isLoading: isLoadingEvent } = useGetEventByIdQuery(
    Number(id)
  );
  const [updateEvent, { isLoading: isUpdating }] = useUpdateEventMutation();

  if (isLoadingEvent) {
    return <Loading />;
  }

  if (!event) {
    return <div>Event not found</div>;
  }

  const handleSubmit = async (data: EventFormData) => {
    try {
      await updateEvent({ id: Number(id), data }).unwrap();
      toast.success("Event updated successfully");
      router.push("/events");
    } catch (error) {
      toast.error("Failed to update event");
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-8">
      <h1 className="text-2xl font-bold mb-6">Edit Event</h1>
      <EventForm
        initialData={event}
        onSubmit={handleSubmit}
        isLoading={isUpdating}
      />
    </div>
  );
}
