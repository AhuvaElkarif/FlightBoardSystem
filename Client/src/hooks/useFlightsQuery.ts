import { useQuery } from "@tanstack/react-query";
import { flightApi } from "../services/flightApi";
import type { FlightFilters } from "../types/types";
import { QUERY_KEYS } from "../utils/constants";

export const useFlightsQuery = (filters?: FlightFilters) => {
  const getFlightQueryKeys = (filters?: FlightFilters) => {
    const baseKey = [QUERY_KEYS.FLIGHTS];
    return filters ? [...baseKey, filters] : baseKey;
  };

  const flightsQuery = useQuery({
    queryKey: getFlightQueryKeys(filters),
    queryFn: async () => {
      try {
        if (filters && (filters.status || filters.destination)) {
          return await flightApi.searchFlights(filters);
        }
        return await flightApi.getFlights();
      } catch (error) {
        console.error(error);
      }
    },
  });

  return {
    flights: flightsQuery.data || [],
    isLoading: flightsQuery.isLoading,
    isFetching: flightsQuery.isFetching,
    error: flightsQuery.error,
    isError: flightsQuery.isError,
    refetch: flightsQuery.refetch,
  };
};
