import { useMutation, useQueryClient } from "@tanstack/react-query";
import { flightApi } from "../services/flightApi";
import type { Flight } from "../types/types";
import { QUERY_KEYS } from "../utils/constants";
import { removeFlightFromCache } from "../utils/cacheUtils";

export const useDeleteFlight = () => {
  const queryClient = useQueryClient();

  const deleteFlightMutation = useMutation({
    mutationFn: async (id: string) => {
      try {
        await flightApi.deleteFlight(id);
        return id;
      } catch (error) {
        throw new Error(
          error instanceof Error ? error.message : "Failed to delete flight"
        );
      }
    },
    onMutate: async (flightId) => {
      await queryClient.cancelQueries({ queryKey: [QUERY_KEYS.FLIGHTS] });

      const previousFlights = queryClient.getQueryData<Flight[]>([
        QUERY_KEYS.FLIGHTS,
      ]);

      removeFlightFromCache(queryClient, flightId);

      return { previousFlights };
    },
    onError: (error, _, context) => {
      if (context?.previousFlights) {
        queryClient.setQueryData([QUERY_KEYS.FLIGHTS], context.previousFlights);
      }
      console.error("Error deleting flight:", error);
    },
    onSettled: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.FLIGHTS],
        exact: false,
      });
    },
  });

  return {
    deleteFlight: deleteFlightMutation.mutate,
    deleteFlightAsync: deleteFlightMutation.mutateAsync,
    isDeleting: deleteFlightMutation.isPending,
    deleteError: deleteFlightMutation.error,
  };
};
