import { Plane } from "lucide-react";
import { useCallback } from "react";
import { useNavigate } from "react-router";

const Welcome = () => {
  const navigate = useNavigate();

  const onClickScreen = useCallback(() => {
    return navigate("user/scanned-product-list");
  }, [navigate]);

  return (
    <div
      onClick={onClickScreen}
      className="flex h-full cursor-pointer items-center flex-col gap-50 justify-center "
    >
      {/* Yazılar ve cam efekti */}
      <div className="flex flex-col items-center p-6 rounded-xl gap-4     bg-black/30 backdrop-blur-md">
        <p className="md:text-5xl lg:text-7xl  text-green-300 uppercase">
          JetMarket'e Hoşgeldiniz
        </p>
        <p className="md:text-3xl lg:text-5xl text-green-300">
          JetKasa İle{" "}
          <span className="inline-block">
            <Plane className="w-10 h-10" />
          </span>{" "}
          Gibi Alışverişler Dileriz
        </p>
      </div>

      <div className=" bg-black/30 backdrop-blur-md p-6 rounded-xl animate-bounce">
        <p className="md:text-3xl lg:text-4xl text-white">
          Başlamak için tıklayınız
        </p>
      </div>
    </div>
  );
};

export default Welcome;
