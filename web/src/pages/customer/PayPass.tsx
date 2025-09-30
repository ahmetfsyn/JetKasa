import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { useCart } from "@/hooks/useCart";
import { useSignalRHub } from "@/hooks/useSignalRHub";
import { useSound } from "@/hooks/useSound";
import { createPaymentAsync } from "@/services/paymentService";
import useCartStore from "@/store/useCartStore";
import { showMessage } from "@/utils/showMessage";
import { useEffect } from "react";
import { useNavigate } from "react-router";

const PayPass = () => {
  const navigate = useNavigate();
  const { connection, isConnected } = useSignalRHub();
  const { cartId } = useCartStore();
  const playSound = useSound();
  useEffect(() => {
    playSound("/sounds/payment-is-started-sound.wav");
  }, [playSound]);

  const { total } = useCart();

  useEffect(() => {
    if (isConnected) {
      connection?.send("StartPayment", {
        cartId: cartId,
        cartTotal: total,
      });

      const handler = async (dataFromPos: any) => {
        console.log("webe postan gelen mesaj : ", dataFromPos);
        if (dataFromPos.isSuccess) {
          try {
            const data = await createPaymentAsync({
              cartId,
              email: dataFromPos.data.email,
              username: dataFromPos.data.username,
            });

            showMessage({
              message: data.data,
              options: {
                type: "success",
              },
            });

            return navigate("/payment/payment-result?isSuccess=true");
          } catch (error: any) {
            showMessage({
              message: error,
              options: {
                type: "error",
              },
            });
            return navigate("/payment-result?isSuccess=false");
          }
        }
      };

      connection?.on("ReceivePaymentResult", handler);
    }

    return () => {};
  }, [isConnected, connection]);

  return (
    <div className="flex h-full items-center justify-center bg-black/30 backdrop-blur-md rounded-md">
      <Card className="w-full max-w-lg bg-white/5 text-white shadow-xl rounded-2xl border border-white/10 p-6">
        <CardHeader>
          <CardTitle className="text-center text-2xl font-bold">
            Kartınızı Okutun
          </CardTitle>
        </CardHeader>
        <CardContent className="flex flex-col items-center gap-6">
          <img
            src="/contactless-payment.gif"
            alt="Temassız ödeme animasyonu"
            className="w-48 h-48 object-contain"
          />
          <p className="text-center text-lg text-gray-300 ">
            Lütfen kartınızı POS cihazına yaklaştırın.
          </p>
        </CardContent>
      </Card>
    </div>
  );
};

export default PayPass;
