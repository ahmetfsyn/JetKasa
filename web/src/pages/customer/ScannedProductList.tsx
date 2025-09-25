import DataTable from "@/components/DataTable";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardAction,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { useCart } from "@/hooks/useCart";
import { useProductByBarcode } from "@/hooks/useProductByBarcode";
import type { Product } from "@/types/entities";
import { formatCurrency } from "@/utils/formatCurrency";
import { showMessage } from "@/utils/showMessage";
import type { ColumnDef } from "@tanstack/react-table";
import { ChevronRightIcon } from "lucide-react";
import { useCallback, useState } from "react";
import { useNavigate } from "react-router";

const columns: ColumnDef<Product>[] = [
  {
    accessorKey: "name",
    header: "Ürün",
  },
  {
    accessorKey: "quantity",
    header: "Adet",
  },
  {
    accessorKey: "price",
    header: "Fiyat",
    cell: ({ getValue }) => {
      const value = getValue<number>();
      return formatCurrency(value);
    },
  },
];
import BarcodeScanner from "react-qr-barcode-scanner";

const ScannedProductList = () => {
  const navigate = useNavigate();
  const [manuelBarcode, setManuelBarcode] = useState<string>("");
  const { handleAddToCart, isFetching } = useProductByBarcode();
  const { total, subTotal, totalDiscount, cartItems } = useCart();
  const formattedCurrency = useCallback(formatCurrency, [formatCurrency]);

  const onCancelShopping = useCallback(() => {
    console.log("İşlem İptal Edildi");
    return navigate("/");
  }, [navigate]);

  const handlePayment = useCallback(() => {
    if (cartItems.length === 0) {
      return showMessage({
        message: "Sepete ürün eklemelisiniz",
        options: {
          type: "error",
        },
      });
    }
    return navigate("/payment");
  }, [cartItems, navigate]);

  return (
    <>
      <div className=" flex h-full ">
        <div className="grid grid-cols-12  gap-2 w-full h-full text-white">
          <div className="bg-black/30 md:col-span-6 lg:col-span-8 backdrop-blur-md rounded-md p-4  ">
            <p className="text-2xl">Sepetiniz</p>
            <div className="my-8">
              <DataTable columns={columns} data={cartItems} />
            </div>
          </div>

          <div className="bg-black/30 md:col-span-6 lg:col-span-4  backdrop-blur-md rounded-md p-4 flex justify-between flex-col">
            {/* <Card className="bg-transparent text-white  justify-center md:h-fit lg:p-10 md:p-6 items-center border-dashed border-2">
            <ScanBarcode className="text-green-300 " size={64} />
            <div className="flex flex-col items-center">
              <p className="text-lg font-medium text-green-300">
                Ürün Barkodunu Okut
              </p>
              <p className="text-md">Veya Alttaki Kutucuğa Gir</p>
            </div>
          </Card> */}

            <BarcodeScanner
              height={128}
              delay={3500}
              onUpdate={(_, res) => {
                if (res) {
                  handleAddToCart(res.getText());
                }
              }}
            />

            <div className="flex flex-row gap-2">
              <Input
                type="text"
                value={manuelBarcode}
                onChange={(e) => setManuelBarcode(e.target.value)}
                className="md:h-12 lg:h-14 "
                placeholder="Barkod"
              />
              <Button
                size="icon"
                soundType={manuelBarcode.length > 0 && true}
                disabled={isFetching || !manuelBarcode}
                onClick={() => handleAddToCart(manuelBarcode)}
                className="md:size-12 lg:size-14 bg-green-300 hover:bg-green-600 hover:text-gray-50 text-black "
              >
                <ChevronRightIcon />
              </Button>
            </div>
            <div>
              <Card className="bg-transparent border-none text-white ">
                <CardHeader className="p-0">
                  <CardTitle className="text-xl">Alışveriş Özeti</CardTitle>

                  <div className="flex flex-row justify-between">
                    <CardDescription className="text-md text-gray-300">
                      Ara Toplam
                    </CardDescription>
                    <CardDescription className="text-md text-gray-300">
                      {formattedCurrency(subTotal)}
                    </CardDescription>
                  </div>
                  <div className="flex flex-row justify-between">
                    <CardDescription className="text-md text-gray-300">
                      İndirim
                    </CardDescription>
                    <CardDescription className="text-md text-gray-300">
                      {formattedCurrency(totalDiscount)}
                    </CardDescription>
                  </div>
                  <div className="flex flex-row justify-between">
                    <CardTitle className="text-xl ">Toplam Tutar</CardTitle>
                    <CardTitle className="text-xl text-green-300">
                      {formattedCurrency(total)}
                    </CardTitle>
                  </div>
                </CardHeader>
                <CardAction className="w-full  gap-2 flex flex-row">
                  <Button
                    onClick={onCancelShopping}
                    variant={"destructive"}
                    className="flex-1 lg:h-20 md:h-18 text-xl hover:bg-red-900"
                  >
                    İptal Et
                  </Button>
                  <Button
                    onClick={handlePayment}
                    soundType={true}
                    disabled={cartItems.length === 0}
                    className=" flex-[3] lg:h-20 md:h-18 text-xl bg-green-300 hover:bg-green-600 hover:text-gray-50 text-black"
                  >
                    Ödeme Yap
                  </Button>
                </CardAction>
              </Card>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default ScannedProductList;
