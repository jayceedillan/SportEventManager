import { Sport } from "@/types/sport";
import { Button } from "../common/Button";
import { useRouter } from "next/navigation";
import { FiEdit2, FiTrash2 } from "react-icons/fi";
import { useDeleteSportMutation } from "@/store/services/sportApi";
import { toast } from "react-hot-toast";

interface SportCardProps {
  sport: Sport;
}

export const SportCard: React.FC<SportCardProps> = ({ sport }) => {
  const router = useRouter();
  const [deleteSport] = useDeleteSportMutation();

  const handleDelete = async () => {
    if (confirm("Are you sure you want to delete this sport?")) {
      try {
        await deleteSport(sport.id).unwrap();
        toast.success("Sport deleted successfully");
      } catch (error) {
        toast.error("Failed to delete sport");
      }
    }
  };

  return (
    <div className="bg-white rounded-lg shadow-md p-6">
      <div className="flex justify-between items-start">
        <h3 className="text-lg font-semibold">{sport.name}</h3>
        <span
          className={`px-2 py-1 rounded-full text-xs ${
            sport.isActive
              ? "bg-green-100 text-green-800"
              : "bg-gray-100 text-gray-800"
          }`}
        >
          {sport.isActive ? "Active" : "Inactive"}
        </span>
      </div>

      <p className="mt-2 text-gray-600 text-sm">{sport.description}</p>

      <div className="mt-4">
        <p className="text-sm">
          <span className="font-medium">Players:</span> {sport.minPlayers} -{" "}
          {sport.maxPlayers}
        </p>
      </div>

      <div className="mt-6 flex space-x-3">
        <Button
          variant="outline"
          size="sm"
          onClick={() => router.push(`/sports/${sport.id}`)}
        >
          View Details
        </Button>
        <Button
          variant="outline"
          size="sm"
          onClick={() => router.push(`/sports/edit/${sport.id}`)}
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
