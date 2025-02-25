import { Toaster } from "react-hot-toast";

export function ToastProvider() {
  return (
    <Toaster
      position="top-right"
      toastOptions={{
        duration: 3000,
        style: {
          background: "#333",
          color: "#fff",
          padding: "16px",
          borderRadius: "8px",
        },
        success: {
          style: {
            background: "#10B981",
          },
        },
        error: {
          duration: 4000,
          style: {
            background: "#EF4444",
          },
        },
      }}
    />
  );
}
