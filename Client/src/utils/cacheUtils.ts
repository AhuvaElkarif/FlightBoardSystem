import { QueryClient } from "@tanstack/react-query";
import type { Flight } from "../types/types";
import { QUERY_KEYS } from "../utils/constants";

export const addFlightToCache = (
  queryClient: QueryClient,
  newFlight: Flight
) => {
  queryClient.setQueriesData<Flight[]>(
    { queryKey: [QUERY_KEYS.FLIGHTS] },
    (old: Flight[] | undefined) => {
      if (!old) return [newFlight];

      const exists = old.some((flight) => flight.id === newFlight.id);
      return exists ? old : [...old, newFlight];
    }
  );

  queryClient.invalidateQueries({
    queryKey: [QUERY_KEYS.FLIGHTS],
    exact: false,
  });
};

export const removeFlightFromCache = (
  queryClient: QueryClient,
  flightId: string
) => {
  queryClient.setQueriesData<Flight[]>(
    { queryKey: [QUERY_KEYS.FLIGHTS] },
    (old) => (old ? old.filter((f) => f.id !== flightId) : [])
  );
  queryClient.invalidateQueries({
    queryKey: [QUERY_KEYS.FLIGHTS],
    exact: false,
  });
};
