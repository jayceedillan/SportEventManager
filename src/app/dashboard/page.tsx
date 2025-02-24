"use client";

import { DashboardStats } from "@/components/dashboard/DashboardStats";
import { RecentActivities } from "@/components/dashboard/RecentActivities";
import { StatisticsChart } from "@/components/dashboard/StatisticsChart";
import { useGetEventsQuery } from "@/store/services/eventApi";
import { useGetSportsQuery } from "@/store/services/sportApi";

export default function DashboardPage() {
  const { data: events } = useGetEventsQuery({ page: 1, pageSize: 5 });
  const { data: sports } = useGetSportsQuery({ page: 1, pageSize: 5 });

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold">Dashboard</h1>

      <DashboardStats
        totalEvents={events?.totalCount || 0}
        totalSports={sports?.totalCount || 0}
        activeEvents={
          events?.items.filter((e) => e.status === "Scheduled").length || 0
        }
        activeSports={sports?.items.filter((s) => s.isActive).length || 0}
      />

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <StatisticsChart />
        <RecentActivities events={events?.items} sports={sports?.items} />
      </div>
    </div>
  );
}
