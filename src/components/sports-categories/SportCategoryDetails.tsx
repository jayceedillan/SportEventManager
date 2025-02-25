import { SportCategory } from "@/types/sportCategory";
import { Button } from "../common/Button";
import { useRouter } from "next/navigation";

interface SportCategoryDetailsProps {
  sportCategory: SportCategory;
}

export const SportDetails: React.FC<SportCategoryDetailsProps> = ({
  sportCategory,
}) => {
  const router = useRouter();

  return (
    <div className="max-w-3xl mx-auto p-6 bg-white rounded-lg shadow-md">
      <div className="flex justify-between items-start mb-6">
        <h1 className="text-2xl font-bold">{sportCategory.name}</h1>
      </div>

      <div className="space-y-6">
        <div>
          <h2 className="text-lg font-semibold mb-2">Description</h2>
          <p className="text-gray-600">{sportCategory.description}</p>
        </div>

        <div>
          <h2 className="text-lg font-semibold mb-2">URL</h2>
          <p className="text-gray-600">{sportCategory.iconUrl}</p>
        </div>

        <div className="flex space-x-4 mt-8">
          <Button
            onClick={() =>
              router.push(`/sports-category/edit/${sportCategory.id}`)
            }
            variant="primary"
          >
            Edit Sport
          </Button>
          <Button
            onClick={() => router.push("/sports-category")}
            variant="outline"
          >
            Back to Dashboard
          </Button>
        </div>
      </div>
    </div>
  );
};
