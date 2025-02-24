"use client";

import {
  UserGroupIcon,
  CalendarIcon,
  TrophyIcon,
  CurrencyDollarIcon,
} from "@heroicons/react/24/outline";
import { DashboardCard } from "./DashboardCard";

export const DashboardStats: React.FC = () => {
  const stats = [
    {
      title: "Total Users",
      value: "2,543",
      icon: <UserGroupIcon className="w-8 h-8" />,
      trend: { value: 12, isUpward: true },
    },
    {
      title: "Total Events",
      value: "45",
      icon: <CalendarIcon className="w-8 h-8" />,
      trend: { value: 8, isUpward: true },
    },
    {
      title: "Active Sports",
      value: "15",
      icon: <TrophyIcon className="w-8 h-8" />,
      trend: { value: 3, isUpward: false },
    },
    {
      title: "Revenue",
      value: "$12,426",
      icon: <CurrencyDollarIcon className="w-8 h-8" />,
      trend: { value: 15, isUpward: true },
    },
  ];

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
      {stats.map((stat, index) => (
        <DashboardCard key={index} {...stat} />
      ))}
    </div>
  );
};
