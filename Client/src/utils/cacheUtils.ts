import { QueryClient } from '@tanstack/react-query';
import type { Flight } from '../types/types';
import { QUERY_KEYS } from '../utils/constants';

export const addFlightToCache = (queryClient: QueryClient, newFlight: Flight) => {
  queryClient.setQueryData<Flight[]>([QUERY_KEYS.FLIGHTS], (old) =>
    old ? [...old, newFlight] : [newFlight]
  );
};

export const removeFlightFromCache = (queryClient: QueryClient, flightId: string) => {
  queryClient.setQueryData<Flight[]>([QUERY_KEYS.FLIGHTS], (old) =>
    old ? old.filter(f => f.id !== flightId) : []
  );
};

export const updateFlightInCache = (queryClient: QueryClient, updatedFlight: Flight) => {
  queryClient.setQueryData<Flight[]>([QUERY_KEYS.FLIGHTS], (old) =>
    old ? old.map(f => (f.id === updatedFlight.id ? updatedFlight : f)) : [updatedFlight]
  );
};
