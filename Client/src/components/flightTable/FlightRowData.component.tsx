import { format } from "date-fns";
import { TableCell, TableRow } from "../ui/Table.styled";
import { StatusBadge } from "../common/StatusBadge.component";
import { DeleteButton } from "./FlightTable.styled";
import { Trash2 } from "lucide-react";
import type { Flight } from "../../types/types";

interface FlightRowDataProps {
  flight: Flight;
  onDelete: (id: string) => void;
  isDeleting?: boolean;
}

const FlightRowData: React.FC<FlightRowDataProps> = ({
  flight,
  onDelete,
  isDeleting,
}) => {
  const formattedDepartureTime = format(
    new Date(flight.departureTime),
    "MMM dd, yyyy HH:mm"
  );

  return (
    <TableRow>
      <TableCell>{flight.flightNumber}</TableCell>
      <TableCell>{flight.destination}</TableCell>
      <TableCell>{formattedDepartureTime}</TableCell>
      <TableCell>{flight.gate}</TableCell>
      <TableCell>
        <StatusBadge status={flight.status} />
      </TableCell>
      <TableCell>
        <DeleteButton
          variant="danger"
          size="small"
          onClick={() => onDelete(flight.id)}
          disabled={isDeleting}
        >
          <Trash2 size={16} />
        </DeleteButton>
      </TableCell>
    </TableRow>
  );
};
export default FlightRowData;
