import { useEffect, useRef } from "react";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useQueryClient } from "@tanstack/react-query";
import type { Flight } from "../types/types";
import { addFlightToCache, removeFlightFromCache } from "../utils/cacheUtils";
import { API_ENDPOINTS } from "../utils/constants";

export const useSignalR = () => {
  const connectionRef = useRef<HubConnection | null>(null);
  const queryClient = useQueryClient();
  const API_BASE_URL = import.meta.env.VITE_API_URL;

  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl(`${API_BASE_URL}${API_ENDPOINTS.SIGNALR_HUB}`)
      .withAutomaticReconnect()
      .build();

    connectionRef.current = connection;

    const startConnection = async () => {
      try {
        await connection.start();
        console.log("SignalR Connected");

        connection.on("FlightAdded", (flight: Flight) => {
          addFlightToCache(queryClient, flight);
        });

        connection.on("FlightDeleted", (flightId: string) => {
          removeFlightFromCache(queryClient, flightId);
        });
      } catch (error) {
        console.error("SignalR Connection Error:", error);
      }
    };

    startConnection();

    connection.onclose(() => {
      console.log("SignalR Disconnected");
    });

    return () => {
      connection.stop();
    };
  }, [queryClient]);

  return connectionRef.current;
};
