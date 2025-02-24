import { Provider } from "react-redux";
import { store } from "@/store";
import { Toast } from "@/components/common/Toast";

interface AppProvidersProps {
  children: React.ReactNode;
}

export const AppProviders: React.FC<AppProvidersProps> = ({ children }) => {
  return (
    <Provider store={store}>
      <Toast />
      {children}
    </Provider>
  );
};
