"use client";

import { useParams, useRouter } from "next/navigation";
import { SportCategoryForm } from "@/components/sports-categories/SportCategoryForm";
import {
  useGetSportCategoryByIdQuery,
  useUpdateSportCategoryMutation,
} from "@/store/services/sportCategoryApi";
import { toast } from "react-hot-toast";
import { Loading } from "@/components/common/Loading";

export default function EditSportCategoryPage() {
  const { id } = useParams();
  const router = useRouter();
  const { data: sportCategory, isLoading: isLoadingSportCategory } =
    useGetSportCategoryByIdQuery(Number(id));
  const [updateSportCategory, { isLoading: isUpdating }] =
    useUpdateSportCategoryMutation();

  if (isLoadingSportCategory) {
    return <Loading />;
  }

  if (!sportCategory) {
    return <div>Sport Category not found</div>;
  }

  const handleSubmit = async (body: SportCategoryFormData) => {
    try {
      await updateSportCategory({ id: Number(id), body }).unwrap();
      toast.success("Sport Category updated successfully");
      router.push("/sports-category");
    } catch (error) {
      toast.error("Failed to update sport Category");
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-8">
      <h1 className="text-2xl font-bold mb-6">Edit Sport</h1>
      {JSON.stringify(sportCategory)}
      <SportCategoryForm
        initialData={sportCategory}
        onSubmit={handleSubmit}
        isLoading={isUpdating}
      />
    </div>
  );
}
