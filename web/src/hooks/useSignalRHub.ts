import { useEffect, useState } from "react";
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import {
  BASE_API_URL_DEV,
  BASE_API_URL_PROD,
  IS_PROD,
} from "@/configs/axiosConfig";

export const useSignalRHub = () => {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [isConnected, setIsConnected] = useState(false);

  useEffect(() => {
    const conn = new HubConnectionBuilder()
      .withUrl(`${IS_PROD ? BASE_API_URL_PROD : BASE_API_URL_DEV}/hub`)
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    conn
      .start()
      .then(() => {
        console.log("✅ Connected to SignalR");
        setIsConnected(true);
      })
      .catch((err) => console.error("❌ Connection failed:", err));

    conn.onclose(() => setIsConnected(false));

    setConnection(conn);

    return () => {
      conn.stop();
    };
  }, []);

  return { connection, isConnected };
};
