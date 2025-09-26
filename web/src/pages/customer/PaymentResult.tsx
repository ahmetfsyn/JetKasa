import "react-simple-keyboard/build/css/index.css";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { useRef, useState } from "react";
import { useNavigate, useSearchParams } from "react-router";
import Keyboard, { type SimpleKeyboard } from "react-simple-keyboard";
import { Check, X } from "lucide-react";

const PaymentResult = () => {
  const [searchParams] = useSearchParams();
  const [emailOrPhoneNumber, setEmailOrPhoneNumber] = useState<string>("");
  const virtualKeyboardRef = useRef<SimpleKeyboard | null>(null);
  const navigate = useNavigate();
  const handleSendReceipt = () => {
    console.log(emailOrPhoneNumber, "Fiş gönderildi");

    return navigate("/");
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const input = e.target.value;
    setEmailOrPhoneNumber(input);
    virtualKeyboardRef.current?.setInput(input);
  };

  const handleKeyboardChange = (input: string) => {
    setEmailOrPhoneNumber(input);
  };

  const handleKeyPress = (button: string) => {
    if (button === "{enter}") handleSendReceipt();
  };

  if (searchParams.get("isSuccess")?.valueOf() === "true") {
    return (
      <div className="rounded-md bg-black/30 h-full flex items-center justify-center backdrop-blur-md">
        <div className="flex flex-col items-center gap-8">
          <p className="md:text-4xl lg:text-5xl text-[#f2f4f6] flex items-center gap-4">
            <Check className="bg-green-600 rounded-full w-12 h-12 p-1 text-white" />
            Ödemeniz Başarıyla Alındı
          </p>
          <p className="md:text-xl lg:text-2xl text-gray-300">
            Fişinizi almak için telefon numaranızı veya email adresinizi giriniz
          </p>
          <div className="flex flex-col gap-8">
            <Input
              type="text"
              autoFocus
              value={emailOrPhoneNumber}
              onChange={handleInputChange}
              className="md:h-12 lg:h-14 text-[#f2f4f6] text-lg"
            />
            <Button
              onClick={handleSendReceipt}
              soundType={true}
              disabled={!emailOrPhoneNumber}
              className="lg:h-20 md:h-18 text-xl bg-green-300 hover:bg-green-600 hover:text-gray-50 text-black"
            >
              Gönder
            </Button>
          </div>

          <Keyboard
            keyboardRef={(r) => (virtualKeyboardRef.current = r)}
            onChange={handleKeyboardChange}
            onKeyPress={handleKeyPress}
          />
        </div>
      </div>
    );
  }

  return (
    <div className="rounded-md bg-black/30 h-full flex items-center justify-center backdrop-blur-md">
      <div className="flex flex-col items-center gap-8">
        <p className="md:text-4xl lg:text-5xl text-[#f2f4f6] flex items-center gap-4">
          <X className="bg-red-600 rounded-full w-12 h-12 p-1 text-white" />
          Ödemeniz Alınırken Bir Hata Oluştu
        </p>
      </div>
    </div>
  );
};

export default PaymentResult;
