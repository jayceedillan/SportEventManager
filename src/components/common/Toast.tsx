import { Toaster } from "react-hot-toast";

export const Toast: React.FC = () => {
  return (
    <Toaster
      position="top-right"
      toastOptions={{
        duration: 3000,
        style: {
          background: "#363636",
          color: "#fff",
        },
        success: {
          duration: 3000,
          theme: {
            primary: "#059669",
          },
        },
        error: {
          duration: 4000,
          theme: {
            primary: "#DC2626",
          },
        },
      }}
    />
  );
};
