import PaymentOption from "@/components/PaymentOption";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardAction,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import usePaymentStore from "@/store/usePaymentStore";
import type { PaymentId, PaymentMethod } from "@/types/entities";
import { CreditCard, Smartphone, Wallet } from "lucide-react";
import { useState } from "react";
import { useNavigate } from "react-router";

const paymentOptions: PaymentMethod[] = [
  {
    id: 1,
    title: "Kredi / Banka Kartı",
    icon: <CreditCard size={42} className="text-green-300" />,
    badges: [{ text: "Sadece Temassız", color: "bg-amber-400 text-black" }],
  },
  {
    id: 2,
    title: "Mobil Ödeme",
    icon: <Smartphone size={42} className="text-blue-300" />,
    badges: [{ text: "Yakında", color: "bg-blue-300 text-black" }],
    disabled: true,
  },
  {
    id: 3,
    title: "Nakit Ödeme",
    icon: <Wallet size={42} className="text-yellow-300" />,
    badges: [{ text: "Yakında", color: "bg-blue-300 text-black" }],
    disabled: true,
  },
];

const paymentRoutes: Record<PaymentId, string> = {
  1: "/payment/paypass",
  2: "/payment/mobile",
  3: "/payment/cash",
};

const PaymentMethods = () => {
  const navigate = useNavigate();
  const { setSelectedPaymentMethod } = usePaymentStore();
  const [selectedId, setSelectedId] = useState<PaymentId | null>(null);

  const handleContinue = () => {
    if (selectedId) {
      setSelectedPaymentMethod(selectedId);
      navigate(paymentRoutes[selectedId]);
    }
  };

  return (
    <div className="flex h-full items-center justify-center backdrop-blur-md bg-black/30 rounded-md p-4">
      <Card className="md:w-[75%] lg:w-full max-w-2xl bg-white/5 text-white shadow-xl rounded-2xl border border-white/10">
        <CardHeader className="space-y-2 text-center">
          <CardTitle className="text-2xl font-semibold tracking-wide">
            Nasıl Ödeme Yapmak İstersiniz?
          </CardTitle>
        </CardHeader>

        <CardContent className="space-y-4 px-4">
          {paymentOptions.map((option) => (
            <PaymentOption
              key={option.id}
              option={option}
              selected={selectedId === option.id}
              onSelect={setSelectedId}
            />
          ))}
        </CardContent>

        <CardAction className="flex w-full px-4">
          <Button
            disabled={!selectedId}
            onClick={handleContinue}
            soundType={true}
            className="flex-1 lg:h-20 md:h-18 text-xl bg-green-300 hover:bg-green-600 hover:text-gray-50 text-black"
          >
            Devam Et
          </Button>
        </CardAction>
      </Card>
    </div>
  );
};

export default PaymentMethods;
