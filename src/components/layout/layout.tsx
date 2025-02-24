import { Header } from "./Header";
import { Sidebar } from "./Sidebar";
import { Footer } from "./Footer";
import { useAppSelector } from "@/hooks/useAppSelector";
import clsx from "clsx";

interface LayoutProps {
  children: React.ReactNode;
}

export const Layout: React.FC<LayoutProps> = ({ children }) => {
  const sidebarOpen = useAppSelector((state) => state.ui.sidebarOpen);

  return (
    <div className="min-h-screen bg-gray-100">
      <Header />
      <Sidebar />

      <main
        className={clsx("transition-all duration-300 ease-in-out", {
          "ml-64": sidebarOpen,
          "ml-20": !sidebarOpen,
        })}
      >
        <div className="pt-16 pb-6">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            {children}
          </div>
        </div>
      </main>

      <div
        className={clsx("transition-all duration-300 ease-in-out", {
          "ml-64": sidebarOpen,
          "ml-20": !sidebarOpen,
        })}
      >
        <Footer />
      </div>
    </div>
  );
};
