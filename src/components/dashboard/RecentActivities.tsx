"use client";

import { format } from "date-fns";

interface Activity {
  id: string;
  type: "event" | "sport" | "user";
  action: string;
  description: string;
  timestamp: Date;
  user: {
    name: string;
    avatar: string;
  };
}

export const RecentActivities: React.FC = () => {
  const activities: Activity[] = [
    {
      id: "1",
      type: "event",
      action: "Created new event",
      description: "Summer Basketball Tournament",
      timestamp: new Date("2023-07-10T09:00:00"),
      user: {
        name: "John Doe",
        avatar: "https://api.dicebear.com/6.x/initials/svg?seed=JD",
      },
    },
    // Add more activities as needed
  ];

  return (
    <div className="bg-white rounded-lg shadow-sm p-6">
      <h2 className="text-lg font-semibold mb-4">Recent Activities</h2>
      <div className="space-y-4">
        {activities.map((activity) => (
          <div key={activity.id} className="flex items-start space-x-4">
            <img
              src={activity.user.avatar}
              alt={activity.user.name}
              className="w-10 h-10 rounded-full"
            />
            <div className="flex-1">
              <p className="text-sm">
                <span className="font-medium">{activity.user.name}</span>{" "}
                {activity.action}
              </p>
              <p className="text-sm text-gray-600">{activity.description}</p>
              <p className="text-xs text-gray-400">
                {format(activity.timestamp, "MMM d, yyyy h:mm a")}
              </p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};
