"use client";

import { useRouter } from "next/navigation";
import { SportCategoryForm } from "@/components/sports-categories/SportCategoryForm";
import { useCreateSportCategoryMutation } from "@/store/services/sportCategoryApi";
import { toast } from "react-hot-toast";

export default function CreateSportCateogryPage() {
  const router = useRouter();
  const [createSportCategory, { isLoading }] = useCreateSportCategoryMutation();

  const handleSubmit = async (data: SportCategoryFormData) => {
    try {
      await createSportCategory(data).unwrap();
      toast.success("Sport created successfully");
      router.push("/Sports");
    } catch (error) {
      toast.error("Failed to create Sport");
    }
  };

  return <SportCategoryForm onSubmit={handleSubmit} isLoading={isLoading} />;
}
