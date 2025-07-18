import { Container, DataTitle } from "./FlightTable.styled";
import { useAppSelector } from "../../store";
import { useFlights } from "../../hooks/useFlights";
import {
  ErrorMessageBoard,
  Section,
} from "../flightsBoard/FlightsBoard.styled";
import { FlightFilters } from "./FlightFilters.component";
import { FlightTable } from "./FlightTable.component";

export const FlightData: React.FC = () => {
  const { filters } = useAppSelector((state) => state.filters);
  const { flights, isLoading, error, deleteFlight, isDeleting, deleteError } =
    useFlights(filters);

  const handleSearch = () => {
    // The search is handled automatically by the useFlights hook
    // when filters change due to the query key dependency
  };

  return (
    <Container>
      <DataTitle>Flight Management</DataTitle>
      <Section>
        <FlightFilters onSearch={handleSearch} isSearching={isLoading} />
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
