"use client";

import { useAppSelector } from "@/hooks/useAppSelector";
import Link from "next/link";
import { usePathname } from "next/navigation";
import {
  FiHome,
  FiCalendar,
  FiUsers,
  FiSettings,
  FiActivity,
} from "react-icons/fi";
import clsx from "clsx";

const navigation = [
  { name: "Dashboard", href: "/dashboard", icon: FiHome },
  { name: "Events", href: "/events", icon: FiCalendar },
  { name: "Sports Category", href: "/sports-category", icon: FiActivity },
  { name: "Sports", href: "/sports", icon: FiActivity },
  { name: "Users", href: "/users", icon: FiUsers },
  { name: "Settings", href: "/settings", icon: FiSettings },
];

export const Sidebar: React.FC = () => {
  const pathname = usePathname();
  const isOpen = useAppSelector((state) => state.ui.sidebarOpen);

  return (
    <div
      className={clsx(
        "fixed left-0 top-0 h-full bg-gray-900 text-white transition-all duration-300 ease-in-out z-40",
        {
          "w-64": isOpen,
          "w-20": !isOpen,
        }
      )}
    >
      {/* Logo */}
      <div className="h-16 flex items-center justify-center border-b border-gray-800">
        <Link href="/dashboard" className="text-xl font-bold">
          {isOpen ? "Sports App" : "SA"}
        </Link>
      </div>

      {/* Navigation */}
      <nav className="mt-6">
        {navigation.map((item) => {
          const isActive = pathname.startsWith(item.href);
          return (
            <Link
              key={item.name}
              href={item.href}
              className={clsx(
                "flex items-center px-4 py-3 transition-colors duration-200",
                {
                  "bg-gray-800 text-white": isActive,
                  "text-gray-400 hover:bg-gray-800 hover:text-white": !isActive,
                }
              )}
            >
              <item.icon className="h-6 w-6" />
              {isOpen && <span className="ml-4">{item.name}</span>}
            </Link>
          );
        })}
      </nav>
    </div>
  );
};
