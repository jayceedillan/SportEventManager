import { Sport } from "@/types/sport";
import { Button } from "../common/Button";
import { useRouter } from "next/navigation";

interface SportDetailsProps {
  sport: Sport;
}

export const SportDetails: React.FC<SportDetailsProps> = ({ sport }) => {
  const router = useRouter();

  return (
    <div className="max-w-3xl mx-auto p-6 bg-white rounded-lg shadow-md">
      <div className="flex justify-between items-start mb-6">
        <h1 className="text-2xl font-bold">{sport.name}</h1>
        <span
          className={`px-3 py-1 rounded-full text-sm ${
            sport.isActive
              ? "bg-green-100 text-green-800"
              : "bg-gray-100 text-gray-800"
          }`}
        >
          {sport.isActive ? "Active" : "Inactive"}
        </span>
      </div>

      <div className="space-y-6">
        <div>
          <h2 className="text-lg font-semibold mb-2">Description</h2>
          <p className="text-gray-600">{sport.description}</p>
        </div>

        <div>
          <h2 className="text-lg font-semibold mb-2">Rules</h2>
          <p className="text-gray-600">{sport.rules}</p>
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div>
            <h2 className="text-lg font-semibold mb-2">Minimum Players</h2>
            <p className="text-gray-600">{sport.minPlayers}</p>
          </div>
          <div>
            <h2 className="text-lg font-semibold mb-2">Maximum Players</h2>
            <p className="text-gray-600">{sport.maxPlayers}</p>
          </div>
        </div>

        <div className="flex space-x-4 mt-8">
          <Button
            onClick={() => router.push(`/sports/edit/${sport.id}`)}
            variant="primary"
          >
            Edit Sport
          </Button>
          <Button onClick={() => router.push("/sports")} variant="outline">
            Back to Sports
          </Button>
        </div>
      </div>
    </div>
  );
};
