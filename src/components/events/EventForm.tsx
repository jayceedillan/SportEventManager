import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Event, EventFormData } from "@/types/event";
import { eventSchema } from "@/lib/validation";
import { Input } from "../common/Input";
import { Textarea } from "../common/Textarea";
import { Select } from "../common/Select";
import { Button } from "../common/Button";

interface EventFormProps {
  initialData?: Event;
  onSubmit: (data: EventFormData) => Promise<void>;
  isLoading: boolean;
}

export const EventForm: React.FC<EventFormProps> = ({
  initialData,
  onSubmit,
  isLoading,
}) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<EventFormData>({
    resolver: zodResolver(eventSchema),
    defaultValues: initialData,
  });

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
      <Input
        label="Title"
        {...register("title")}
        error={errors.title?.message}
      />

      <Textarea
        label="Description"
        {...register("description")}
        error={errors.description?.message}
      />

      <div className="grid grid-cols-2 gap-4">
        <Input
          label="Start Date"
          type="datetime-local"
          {...register("startDate")}
          error={errors.startDate?.message}
        />

        <Input
          label="End Date"
          type="datetime-local"
          {...register("endDate")}
          error={errors.endDate?.message}
        />
      </div>

      <Input
        label="Location"
        {...register("location")}
        error={errors.location?.message}
      />

      <div className="grid grid-cols-2 gap-4">
        <Input
          label="Maximum Participants"
          type="number"
          {...register("maxParticipants", { valueAsNumber: true })}
          error={errors.maxParticipants?.message}
        />

        <Select
          label="Status"
          {...register("status")}
          error={errors.status?.message}
          options={[
            { value: "scheduled", label: "Scheduled" },
            { value: "in_progress", label: "In Progress" },
            { value: "completed", label: "Completed" },
            { value: "cancelled", label: "Cancelled" },
          ]}
        />
      </div>

      <Button type="submit" isLoading={isLoading} className="w-full">
        {initialData ? "Update Event" : "Create Event"}
      </Button>
    </form>
  );
};
