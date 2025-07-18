import { useQuery } from '@tanstack/react-query';
import { flightApi } from '../services/flightApi';
import type { FlightFilters } from '../types/types';
import { QUERY_KEYS } from '../utils/constants';

interface FlightError {
  message: string;
  code?: string;
  details?: any;
}

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
        const flightError: FlightError = {
          message: error instanceof Error ? error.message : 'Failed to fetch flights',
          code: (error as any)?.code,
          details: error
        };
        throw flightError;
      }
    },
    staleTime: 30000,
    refetchOnWindowFocus: false,
    retry: (failureCount, error) => {
      if ((error as any)?.status >= 400 && (error as any)?.status < 500) {
        return false;
      }
      return failureCount < 3;
    },
    retryDelay: (attemptIndex) => Math.min(1000 * 2 ** attemptIndex, 30000),
  });

  return {
    flights: flightsQuery.data || [],
    isLoading: flightsQuery.isLoading,
    isFetching: flightsQuery.isFetching,
    error: flightsQuery.error as FlightError | null,
    isError: flightsQuery.isError,
    refetch: flightsQuery.refetch,
  };
};