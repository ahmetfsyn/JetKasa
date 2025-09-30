import { View } from 'react-native';
import React, { useEffect, useState } from 'react';
import { usePaymentHub } from '../hooks/usePaymentHub';
import { ActivityIndicator, Text } from 'react-native-paper';
import { useNfcManager } from '../hooks/useNfcManager';
import { formatCurrency } from '../../utils/formatCurrency';

const Payment = () => {
  const { connection } = usePaymentHub();
  const [didStartPayment, setDidStartPayment] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(false);

  const [paymentInfo, setPaymentInfo] = useState<{
    cartTotal: number;
  } | null>(null);
  const { readNdef } = useNfcManager();

  useEffect(() => {
    if (connection) {
      const handler = async (data: any) => {
        try {
          console.log('POS mesaj aldı:', data);
          setPaymentInfo(data);
          setDidStartPayment(true);
          const cardData = await readNdef();
          setLoading(true);
          await connection?.send('PaymentProcessed', {
            isSuccess: true,
            data: cardData,
          });
        } catch (error) {
          await connection?.send('PaymentProcessed', {
            isSuccess: false,
            data: null,
          });
        } finally {
          setDidStartPayment(false);
          setLoading(false);
        }
      };

      connection.on('ReceivePaymentStart', handler);

      return () => {
        connection.off('ReceivePaymentStart', handler);
      };
    }
  }, [connection]);

  return (
    <View
      style={{
        flex: 1,
        padding: 16,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: '#353535',
      }}
    >
      {didStartPayment ? (
        <>
          <Text
            variant="headlineMedium"
            style={{ color: '#f2f4f6', marginBottom: 12 }}
          >
            {paymentInfo ? formatCurrency(paymentInfo.cartTotal) : '...'}
          </Text>

          {loading && <ActivityIndicator size="large" color="#f2f4f6" />}
        </>
      ) : (
        <Text variant="headlineMedium" style={{ color: '#f2f4f6' }}>
          JetMarket'e Hoşgeldiniz
        </Text>
      )}
    </View>
  );
};

export default Payment;
