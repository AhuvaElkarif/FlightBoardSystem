import { Container, DataTitle } from "./FlightTable.styled";
import { useAppSelector } from "../../store";
import { useFlights } from "../../hooks/api/flights/useFlights";
import {
  ErrorMessageBoard,
  Section,
} from "../flightsBoard/FlightsBoard.styled";
import { FlightFiltersCard } from "./FlightFilters.component";
import { FlightTable } from "./FlightTable.component";
import { useEffect, useState } from "react";
import type { Flight } from "../../types/types";

export const FlightData: React.FC = () => {
  const { filters } = useAppSelector((state) => state.filters);
  const { flights, isLoading, error, deleteFlightAsync, isDeleting } =
    useFlights(filters);
  const [flightsData, setFlightsData] = useState<Flight[]>(flights);
  useEffect(() => {
    setFlightsData(flights);  
  }, [flights]);
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
          setFlights={setFlightsData}
            flights={flightsData}
            deleteFlight={deleteFlightAsync}
            isDeleting={isDeleting}
          />
        )}
      </Section>
    </Container>
  );
};
