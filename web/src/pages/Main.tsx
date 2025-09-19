import { Card, CardContent } from "@/components/ui/card";
import { useNavigate } from "react-router";

const Main = () => {
  const navigate = useNavigate();

  return (
    <div className="flex flex-col w-screen h-screen bg-gray-500">
      {/* Navbar */}
      <header className="h-[10vh]  shadow-sm flex shadow-gray-200 items-center justify-center text-3xl bg-gray-100">
        JetKasa İle Jet Hızında Alışveriş
      </header>

      {/* Content */}
      <main className="flex-1 grid grid-cols-2 ">
        {/* Left side - full image */}
        <div className="h-full">
          <img
            src="/cover-image.jpg"
            alt="Kapak"
            className="w-full h-full  md:object-fill"
          />
        </div>

        {/* Right side - 4 buttons */}
        <div className="grid grid-cols-4 sm:grid-cols-1 h-full gap-2 p-4">
          <Card
            onClick={() => navigate("/")}
            className="flex flex-col items-center justify-center w-full h-full active:scale-95 transition-transform duration-150 cursor-pointer"
          >
            <CardContent className="flex flex-col items-center gap-2">
              <p className="text-2xl">Kartsız Başlat</p>
            </CardContent>
          </Card>
          <Card className="flex flex-col items-center justify-center w-full h-full active:scale-95 transition-transform duration-150 cursor-pointer">
            <CardContent className="flex flex-col items-center gap-2">
              <p className="text-2xl">Market Kartı İle Başlat</p>
            </CardContent>
          </Card>
          <Card className="flex flex-col items-center justify-center w-full h-full active:scale-95 transition-transform duration-150 cursor-pointer">
            <CardContent className="flex flex-col items-center gap-2">
              <p className="text-2xl">Yardım Çağır</p>
            </CardContent>
          </Card>
          <Card className="flex flex-col items-center justify-center w-full h-full active:scale-95 transition-transform duration-150 cursor-pointer">
            <CardContent className="flex flex-col items-center gap-2">
              <p className="text-2xl">Dil, Lang, Sprache</p>
            </CardContent>
          </Card>
        </div>
      </main>
    </div>
  );
};

export default Main;
