import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";

const PayPass = () => {
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
