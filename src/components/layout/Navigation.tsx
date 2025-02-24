import { usePathname } from "next/navigation";
import Link from "next/link";
import clsx from "clsx";

interface NavigationItem {
  name: string;
  href: string;
  matchExact?: boolean;
}

interface NavigationProps {
  items: NavigationItem[];
  className?: string;
}

export const Navigation: React.FC<NavigationProps> = ({ items, className }) => {
  const pathname = usePathname();

  return (
    <nav className={clsx("flex space-x-4", className)}>
      {items.map((item) => {
        const isActive = item.matchExact
          ? pathname === item.href
          : pathname.startsWith(item.href);

        return (
          <Link
            key={item.name}
            href={item.href}
            className={clsx("px-3 py-2 rounded-md text-sm font-medium", {
              "bg-gray-900 text-white": isActive,
              "text-gray-300 hover:bg-gray-700 hover:text-white": !isActive,
            })}
          >
            {item.name}
          </Link>
        );
      })}
    </nav>
  );
};
