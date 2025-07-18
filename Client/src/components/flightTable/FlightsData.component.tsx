import { Container, DataTitle } from "./FlightTable.styled";
import { useAppSelector } from "../../store";
import { useFlights } from "../../hooks/api/flights/useFlights";
import {
  ErrorMessageBoard,
  Section,
} from "../flightsBoard/FlightsBoard.styled";
import { FlightFiltersCard } from "./FlightFilters.component";
import { FlightTable } from "./FlightTable.component";

export const FlightData: React.FC = () => {
  const { filters } = useAppSelector((state) => state.filters);
  const { flights, isLoading, error, deleteFlight, isDeleting, deleteError } =
    useFlights(filters);

  return (
    <Container>
      <DataTitle>Flight Management</DataTitle>

      <Section>
        <FlightFiltersCard isSearching={isLoading} />
      </Section>

      <Section>
        {error ? (
          <ErrorMessageBoard>
            Error loading flights: {error.message}
          </ErrorMessageBoard>
        ) : (
          <FlightTable
            deleteError={deleteError}
            flights={flights}
            deleteFlight={deleteFlight}
            isDeleting={isDeleting}
          />
        )}
      </Section>
    </Container>
  );
};
