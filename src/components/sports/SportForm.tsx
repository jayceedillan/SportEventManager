import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Sport } from "@/types/sport";
import { sportSchema } from "@/lib/validation";
import { Input } from "../common/Input";
import { Textarea } from "../common/Textarea";
import { Button } from "../common/Button";

interface SportFormProps {
  initialData?: Sport;
  onSubmit: (data: SportFormData) => Promise<void>;
  isLoading: boolean;
}

export const SportForm: React.FC<SportFormProps> = ({
  initialData,
  onSubmit,
  isLoading,
}) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(sportSchema),
    defaultValues: initialData,
  });

  return (
    <div className="max-w-2xl mx-auto p-6">
      <div className="bg-white rounded-lg shadow-lg p-8 border border-gray-100">
        <h2 className="text-2xl font-bold text-gray-800 mb-6">
          {initialData ? "Update Sport" : "Create New Sport"}
        </h2>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
          <div className="space-y-6">
            <div className="bg-gray-50 p-6 rounded-md">
              <Input
                label="Name"
                {...register("name")}
                error={errors.name?.message}
                className="border rounded p-2 flex-1"
              />
            </div>

            <div className="bg-gray-50 p-6 rounded-md space-y-6">
              <Textarea
                label="Description"
                {...register("description")}
                error={errors.description?.message}
                className="bg-white min-h-[120px]"
              />

              <Textarea
                label="Rules"
                {...register("rules")}
                error={errors.rules?.message}
                className="bg-white min-h-[120px]"
              />
            </div>

            <div className="bg-gray-50 p-6 rounded-md">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <Input
                  label="Minimum Players"
                  type="number"
                  {...register("minPlayers", { valueAsNumber: true })}
                  error={errors.minPlayers?.message}
                  className="border rounded p-2 flex-1"
                />

                <Input
                  label="Maximum Players"
                  type="number"
                  {...register("maxPlayers", { valueAsNumber: true })}
                  error={errors.maxPlayers?.message}
                  className="border rounded p-2 flex-1"
                />
              </div>
            </div>

            <div className="bg-gray-50 p-6 rounded-md">
              <div className="flex items-center space-x-3">
                <input
                  type="checkbox"
                  {...register("isActive")}
                  id="isActive"
                  className="w-5 h-5 text-primary-600 rounded border-gray-300 focus:ring-primary-500"
                />
                <label
                  htmlFor="isActive"
                  className="text-sm font-medium text-gray-700"
                >
                  Active Sport
                </label>
              </div>
            </div>
          </div>

          <div className="pt-4 flex justify-around">
            <Button
              type="submit"
              isLoading={isLoading}
              className=" py-3 text-lg font-semibold shadow-sm bg-blue-500  text-white"
              variant="primary"
            >
              Cancel
            </Button>
            <Button
              type="submit"
              isLoading={isLoading}
              className=" py-3 text-lg font-semibold shadow-sm bg-cyan-500 text-white hover:bg-primary-100"
              variant="primary"
            >
              Save Sport
            </Button>
          </div>
        </form>
      </div>

      {/* Optional: Add a helper text or additional information */}
      <div className="mt-4 text-center text-sm text-gray-500">
        All fields marked with * are required
      </div>
    </div>
  );
};
