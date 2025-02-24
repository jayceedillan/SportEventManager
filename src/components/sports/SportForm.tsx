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
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
      <Input label="Name" {...register("name")} error={errors.name?.message} />

      <Textarea
        label="Description"
        {...register("description")}
        error={errors.description?.message}
      />

      <Textarea
        label="Rules"
        {...register("rules")}
        error={errors.rules?.message}
      />

      <div className="grid grid-cols-2 gap-4">
        <Input
          label="Minimum Players"
          type="number"
          {...register("minPlayers", { valueAsNumber: true })}
          error={errors.minPlayers?.message}
        />

        <Input
          label="Maximum Players"
          type="number"
          {...register("maxPlayers", { valueAsNumber: true })}
          error={errors.maxPlayers?.message}
        />
      </div>

      <div className="flex items-center space-x-2">
        <input type="checkbox" {...register("isActive")} id="isActive" />
        <label htmlFor="isActive">Active</label>
      </div>

      <Button type="submit" isLoading={isLoading} className="w-full">
        {initialData ? "Update Sport" : "Create Sport"}
      </Button>
    </form>
  );
};
