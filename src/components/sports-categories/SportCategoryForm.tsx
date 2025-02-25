"use client";

import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useRouter } from "next/navigation";
import { SportCategory } from "@/types/sportCategory";
import { sportCategorySchema } from "@/lib/validation";
import { Input } from "../common/Input";
import { Button } from "../common/Button";
import { Textarea } from "../common/Textarea";

type SportCategoryFormData = z.infer<typeof sportCategorySchema>;

interface SportCategoryFormProps {
  initialData?: SportCategory;
  onSubmit: (data: SportCategoryFormData) => Promise<void>;
  isLoading: boolean;
}

export const SportCategoryForm: React.FC<SportCategoryFormProps> = ({
  initialData,
  onSubmit,
  isLoading,
}) => {
  const router = useRouter();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(sportCategorySchema),
    defaultValues: initialData,
  });

  return (
    <div className="max-w-2xl mx-auto p-6">
      <div className="bg-white rounded-lg shadow-lg p-8 border border-gray-100">
        <h2 className="text-2xl font-bold text-gray-800 mb-6">
          {initialData ? "Update Sport" : "Create New Sport"}
        </h2>

        <form
          onSubmit={handleSubmit((data) =>
            onSubmit({ ...data, id: initialData?.id })
          )}
          className="space-y-6"
        >
          <div className="space-y-6">
            <div className="bg-gray-50 p-6 rounded-md">
              <Input
                label="Name"
                {...register("name")}
                error={errors.name?.message}
                className="border rounded p-2 flex-1"
              />
            </div>
          </div>
          <div className="bg-gray-50 p-6 rounded-md space-y-6">
            <Textarea
              label="Description"
              {...register("description")}
              error={errors.description?.message}
              className="bg-white min-h-[120px] border"
            />
          </div>
          <div className="pt-4 flex justify-around">
            <Button
              type="button"
              isLoading={isLoading}
              onClick={() => router.push("/sports-category")}
              className=" py-3 text-lg font-semibold shadow-sm bg-gray-500 text-white hover:bg-gray-600"
              variant="secondary"
            >
              Cancel
            </Button>
            <Button
              type="submit"
              isLoading={isLoading}
              className=" py-3 text-lg font-semibold shadow-sm bg-cyan-500 text-white hover:bg-primary-100"
              variant="primary"
            >
              Save
            </Button>
          </div>
        </form>
      </div>

      <div className="mt-4 text-center text-sm text-gray-500">
        All fields marked with * are required
      </div>
    </div>
  );
};
