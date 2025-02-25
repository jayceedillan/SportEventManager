"use client";

import { useParams } from "next/navigation";
import { SportDetails } from "@/components/sports/SportDetails";
import { useGetSportByIdQuery } from "@/store/services/sportApi";
import { Loading } from "@/components/common/Loading";

export default function SportDetailsPage() {
  const { id } = useParams();
  const { data: sport, isLoading } = useGetSportByIdQuery(Number(id));

  if (isLoading) {
    return <Loading />;
  }

  if (!sport) {
    return <div>Sport not found</div>;
  }

  return <SportDetails sport={sport} />;
}
