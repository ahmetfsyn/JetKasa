import { useEffect, useState } from 'react';
import {
  HubConnectionBuilder,
  LogLevel,
  HubConnection,
  HttpTransportType,
} from '@microsoft/signalr';
import {
  BASE_API_URL_DEV,
  BASE_API_URL_PROD,
  IS_PROD,
} from '../constants/constant';

export const usePaymentHub = () => {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [isConnected, setIsConnected] = useState(false);

  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const hubConnection = new HubConnectionBuilder()
      .withUrl(`${IS_PROD ? BASE_API_URL_PROD : BASE_API_URL_DEV}/hub`, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
      })
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    hubConnection
      .start()
      .then(() => {
        console.log('✅ SignalR connected.');
        setIsConnected(true);
      })
      .catch(err => {
        console.error('❌ Connection error:', err);
        setError(err.message || 'Connection failed');
        setIsConnected(false);
      });

    hubConnection.onclose(() => {
      console.log('⚠️ SignalR disconnected.');
      setIsConnected(false);
    });

    setConnection(hubConnection);

    return () => {
      hubConnection.stop();
    };
  }, []);

  return { connection, isConnected, error };
};
