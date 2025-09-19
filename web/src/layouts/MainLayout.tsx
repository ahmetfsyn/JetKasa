import type { ReactElement } from "react";

const MainLayout = ({ children }: { children: ReactElement }) => {
  return (
    <div className="bg-gray-50 h-screen flex flex-col">
      {/* Header */}
      {/* <header className=" p-2 shadow-sm flex items-center justify-center">
        JetKasa İle Jet Hızında Ödeyin
      </header> */}
      {/* Content */}
      <main className="flex">{children}</main>
      {/* Footer */}
      {/* <footer className=" p-2 text-center text-sm">© 2025 JetKasa</footer> */}
    </div>
  );
};

export default MainLayout;
