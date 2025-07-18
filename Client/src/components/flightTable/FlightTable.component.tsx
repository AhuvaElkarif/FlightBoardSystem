import React, { useCallback } from "react";
import {
  Table,
  TableBody,
  TableHead,
  TableHeader,
  TableRow,
} from "../ui/Table.styled";
import type { Flight } from "../../types/types";
import { EmptyState } from "./FlightTable.styled";
import FlightRowData from "./FlightRowData.component";
import { TABLE_HEADERS } from "../../utils/constants";
import Swal from "sweetalert2";
import { ErrorMessageBoard } from "../flightsBoard/FlightsBoard.styled";

interface FlightTableProps {
  flights: Flight[];
  deleteFlight: (id: string) => void;
  isDeleting?: boolean;
  deleteError: Error | null;
}

export const FlightTable: React.FC<FlightTableProps> = ({
  flights,
  deleteFlight,
  isDeleting,
  deleteError
}) => {
  const onDeleteFlight = useCallback(
    async (id: string) => {
      try {
        await deleteFlight(id);
        Swal.fire({
          title: "Flight Deleted Successfully!",
          icon: "success",
          draggable: true,
        });
      } catch (err) {
        console.error("Failed to delete flight:", err);
      }
    },
    [deleteFlight]
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
    <>
      {deleteError && (
            <ErrorMessageBoard>
              Error deleting flight: {deleteError.message}
            </ErrorMessageBoard>
          )}
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
              flight={flight}
              onDelete={onDeleteFlight}
              isDeleting={isDeleting}
            />
          ))}
        </TableBody>
      </Table>
    </>
  );
};
