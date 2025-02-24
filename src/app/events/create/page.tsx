"use client";

import { useRouter } from "next/navigation";
import { EventForm } from "@/components/events/EventForm";
import { useCreateEventMutation } from "@/store/services/eventApi";
import { toast } from "react-hot-toast";

export default function CreateEventPage() {
  const router = useRouter();
  const [createEvent, { isLoading }] = useCreateEventMutation();

  const handleSubmit = async (data: EventFormData) => {
    try {
      await createEvent(data).unwrap();
      toast.success("Event created successfully");
      router.push("/events");
    } catch (error) {
      toast.error("Failed to create event");
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-8">
      <h1 className="text-2xl font-bold mb-6">Create New Event</h1>
      <EventForm onSubmit={handleSubmit} isLoading={isLoading} />
    </div>
  );
}
