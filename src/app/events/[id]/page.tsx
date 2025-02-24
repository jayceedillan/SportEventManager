"use client";

import { useParams } from "next/navigation";
import { EventDetails } from "@/components/events/EventDetails";
import { useGetEventByIdQuery } from "@/store/services/eventApi";
import { Loading } from "@/components/common/Loading";

export default function EventDetailsPage() {
  const { id } = useParams();
  const { data: event, isLoading } = useGetEventByIdQuery(Number(id));

  if (isLoading) {
    return <Loading />;
  }

  if (!event) {
    return <div>Event not found</div>;
  }

  return <EventDetails event={event} />;
}
