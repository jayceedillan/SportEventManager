"use client";

import { useParams, useRouter } from "next/navigation";
import { SportForm } from "@/components/sports/SportForm";
import {
  useGetSportByIdQuery,
  useUpdateSportMutation,
} from "@/store/services/sportApi";
import { toast } from "react-hot-toast";
import { Loading } from "@/components/common/Loading";

export default function EditSportPage() {
  const { id } = useParams();
  const router = useRouter();
  const { data: sport, isLoading: isLoadingSport } = useGetSportByIdQuery(
    Number(id)
  );
  const [updateSport, { isLoading: isUpdating }] = useUpdateSportMutation();

  if (isLoadingSport) {
    return <Loading />;
  }

  if (!sport) {
    return <div>Sport not found</div>;
  }

  const handleSubmit = async (data: SportFormData) => {
    try {
      await updateSport({ id: Number(id), data }).unwrap();
      toast.success("Sport updated successfully");
      router.push("/events");
    } catch (error) {
      toast.error("Failed to update sport");
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-8">
      <h1 className="text-2xl font-bold mb-6">Edit Sport</h1>
      <SportForm
        initialData={sport}
        onSubmit={handleSubmit}
        isLoading={isUpdating}
      />
    </div>
  );
}
