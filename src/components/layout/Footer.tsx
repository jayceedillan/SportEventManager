export const Footer: React.FC = () => {
  return (
    <footer className="bg-white border-t border-gray-200">
      <div className="max-w-7xl mx-auto py-4 px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between items-center">
          <div className="text-sm text-gray-500">
            © {new Date().getFullYear()} Sports App. All rights reserved.
          </div>
          <div className="flex space-x-6">
            <a href="#" className="text-sm text-gray-500 hover:text-gray-900">
              Privacy Policy
            </a>
            <a href="#" className="text-sm text-gray-500 hover:text-gray-900">
              Terms of Service
            </a>
            <a href="#" className="text-sm text-gray-500 hover:text-gray-900">
              Contact Us
            </a>
          </div>
        </div>
      </div>
    </footer>
  );
};
