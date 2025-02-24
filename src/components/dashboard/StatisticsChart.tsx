"use client";

import {
  AreaChart,
  Area,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
} from "recharts";

interface ChartData {
  name: string;
  users: number;
  events: number;
}

export const StatisticsChart: React.FC = () => {
  const data: ChartData[] = [
    { name: "Jan", users: 400, events: 24 },
    { name: "Feb", users: 600, events: 28 },
    { name: "Mar", users: 800, events: 35 },
    { name: "Apr", users: 1000, events: 40 },
    { name: "May", users: 1400, events: 48 },
    { name: "Jun", users: 2000, events: 52 },
  ];

  return (
    <div className="bg-white rounded-lg shadow-sm p-6">
      <h2 className="text-lg font-semibold mb-4">Growth Statistics</h2>
      <div className="h-[400px]">
        <ResponsiveContainer width="100%" height="100%">
          <AreaChart data={data}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="name" />
            <YAxis yAxisId="left" />
            <YAxis yAxisId="right" orientation="right" />
            <Tooltip />
            <Area
              yAxisId="left"
              type="monotone"
              dataKey="users"
              stroke="#8884d8"
              fill="#8884d8"
              fillOpacity={0.3}
            />
            <Area
              yAxisId="right"
              type="monotone"
              dataKey="events"
              stroke="#82ca9d"
              fill="#82ca9d"
              fillOpacity={0.3}
            />
          </AreaChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
};
