"use client";

import { useRouter } from "next/navigation";
import { SportForm } from "@/components/sports/SportForm";
import { useCreateSportMutation } from "@/store/services/sportApi";
import { toast } from "react-hot-toast";

export default function CreateSportPage() {
  const router = useRouter();
  const [createSport, { isLoading }] = useCreateSportMutation();

  const handleSubmit = async (data: SportFormData) => {
    try {
      await createSport(data).unwrap();
      toast.success("Sport created successfully");
      router.push("/Sports");
    } catch (error) {
      toast.error("Failed to create Sport");
    }
  };

  return <SportForm onSubmit={handleSubmit} isLoading={isLoading} />;
}
