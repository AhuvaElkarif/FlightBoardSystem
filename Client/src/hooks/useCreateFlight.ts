import { useMutation, useQueryClient } from '@tanstack/react-query';
import { flightApi } from '../services/flightApi';
import type { CreateFlightRequest, Flight } from '../types/types';
import { QUERY_KEYS } from '../utils/constants';
import { addFlightToCache } from '../utils/cacheUtils';

export const useCreateFlight = () => {
  const queryClient = useQueryClient();

  const createFlightMutation = useMutation({
    mutationFn: async (flight: CreateFlightRequest) => {
      try {
        return await flightApi.createFlight(flight);
      } catch (error) {
        throw new Error(
          error instanceof Error ? error.message : 'Failed to create flight'
        );
      }
    },
    onSuccess: (newFlight) => {
     addFlightToCache(queryClient, newFlight);

      queryClient.invalidateQueries({ 
        queryKey: [QUERY_KEYS.FLIGHTS],
        exact: false
      });
    },
    onError: (error) => {
      console.error('Error creating flight:', error);
    }
  });

  return {
    createFlight: createFlightMutation.mutate,
    createFlightAsync: createFlightMutation.mutateAsync,
    isCreating: createFlightMutation.isPending,
    createError: createFlightMutation.error,
  };
};