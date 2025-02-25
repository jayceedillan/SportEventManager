import "../styles/globals.css";
import { ReduxProvider } from "@/providers/ReduxProvider";
import { Toaster } from "react-hot-toast";

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body>
        <ReduxProvider>{children}</ReduxProvider>
        <Toaster position="top-right" />
      </body>
    </html>
  );
}
