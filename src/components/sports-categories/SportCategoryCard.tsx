import { SportCategory } from "@/types/sportCategory";
import { Button } from "../common/Button";
import { useRouter } from "next/navigation";
import { FiEdit2, FiTrash2 } from "react-icons/fi";
import { useDeleteSportCategoryMutation } from "@/store/services/sportCategoryApi";
import { toast } from "react-hot-toast";

interface SportCategoryCardProps {
  sportCategory: SportCategory;
}

export const SportCard: React.FC<SportCategoryCardProps> = ({
  sportCategory,
}) => {
  const router = useRouter();
  const [deleteSportCategory] = useDeleteSportCategoryMutation();

  const handleDelete = async () => {
    if (confirm("Are you sure you want to delete this sport?")) {
      try {
        await deleteSportCategory(sportCategory.id).unwrap();
        toast.success("Sport deleted successfully");
      } catch (error) {
        toast.error("Failed to delete sport");
      }
    }
  };

  return (
    <div className="bg-white rounded-lg shadow-md p-6">
      <div className="flex justify-between items-start">
        <h3 className="text-lg font-semibold">{sportCategory.name}</h3>
      </div>

      <p className="mt-2 text-gray-600 text-sm">{sportCategory.description}</p>

      <div className="mt-4">
        <p className="text-sm">
          <span className="font-medium">Players:</span> {sportCategory.iconUrl}
        </p>
      </div>

      <div className="mt-6 flex space-x-3">
        <Button
          variant="outline"
          size="sm"
          onClick={() => router.push(`/sports/${sportCategory.id}`)}
        >
          View Details
        </Button>
        <Button
          variant="outline"
          size="sm"
          onClick={() => router.push(`/sports/edit/${sportCategory.id}`)}
        >
          <FiEdit2 className="w-4 h-4" />
        </Button>
        <Button variant="danger" size="sm" onClick={handleDelete}>
          <FiTrash2 className="w-4 h-4" />
        </Button>
      </div>
    </div>
  );
};
