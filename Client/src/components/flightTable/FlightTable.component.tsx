import React, { useCallback } from "react";
import {
  Table,
  TableBody,
  TableHead,
  TableHeader,
  TableRow,
} from "../ui/layout/Table.styled";
import type { Flight } from "../../types/types";
import { EmptyState } from "./FlightTable.styled";
import FlightRowData from "./FlightRowData.component";
import { TABLE_HEADERS } from "../../utils/constants";
import { useToast } from "../../hooks/ui/useToast";

interface FlightTableProps {
  flights: Flight[];
  deleteFlight: (id: string) => Promise<string>;
  isDeleting?: boolean;
}

export const FlightTable: React.FC<FlightTableProps> = ({
  flights,
  deleteFlight,
  isDeleting,
}) => {
  const { showPromise } = useToast();
  const onDeleteFlight = useCallback(
    async (id: string) => {
      const confirmed = window.confirm(
        "Are you sure you want to delete this flight?"
      );

      if (!confirmed) {
        return;
      }
      showPromise(deleteFlight(id), {
        loading: "Deleting flight...",
        success: "Flight deleted successfully!",
        error: "Failed to delete flight",
      });
    },
    [deleteFlight, showPromise]
  );
  if (flights.length === 0) {
    return (
      <EmptyState>
        <h3>No flights found</h3>
        <p>Add a new flight or adjust your filters to see results.</p>
      </EmptyState>
    );
  }

  return (
    <Table>
      <TableHeader>
        <TableRow>
          {TABLE_HEADERS.map((title, index) => (
            <TableHead key={index}>{title}</TableHead>
          ))}
        </TableRow>
      </TableHeader>
      <TableBody>
        {flights.map((flight) => (
          <FlightRowData
            key={flight.id}
            flight={flight}
            onDelete={onDeleteFlight}
            isDeleting={isDeleting}
          />
        ))}
      </TableBody>
    </Table>
  );
};
