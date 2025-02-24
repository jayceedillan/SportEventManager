import { FiSearch } from "react-icons/fi";
import { Input } from "./Input";

interface SearchInputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  onSearch?: (value: string) => void;
}

export const SearchInput: React.FC<SearchInputProps> = ({
  onSearch,
  ...props
}) => {
  return (
    <div className="relative">
      <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
        <FiSearch className="h-5 w-5 text-gray-400" />
      </div>
      <Input
        className="pl-10"
        onChange={(e) => onSearch?.(e.target.value)}
        {...props}
      />
    </div>
  );
};
