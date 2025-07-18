import type { FlightFilters } from "../../../types/types";
import { useCreateFlight } from "./useCreateFlight";
import { useDeleteFlight } from "./useDeleteQuery";
import { useFlightsQuery } from "./useFlightsQuery";

export const useFlights = (filters?: FlightFilters) => {
  const flightsQuery = useFlightsQuery(filters);
  const createFlight = useCreateFlight();
  const deleteFlight = useDeleteFlight();

  return {
    ...flightsQuery,
    ...createFlight,
    ...deleteFlight,
    isMutating: createFlight.isCreating || deleteFlight.isDeleting
  };
};