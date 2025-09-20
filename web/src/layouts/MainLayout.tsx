import React from "react";

const MainLayout = ({ children }: { children: React.ReactElement }) => {
  return (
    <div className=" h-screen p-4 bg-[url('/background.svg')] bg-cover bg-center">
      {children}
    </div>
  );
};

export default MainLayout;
