import { FlightStatus } from "../../types/types";
import { Button } from "../ui/button/Button.component";
import { Input, Select } from "../ui/Input.styled";
import { Card, CardHeader, CardContent } from "../ui/Card.styled";
import { FilterField, FilterGrid, Label } from "./FlightTable.styled";
import { useFlightFilters } from "../../hooks/ui/useFlightFilters";

interface FlightFiltersProps {
  isSearching?: boolean;
}

export const FlightFiltersCard: React.FC<FlightFiltersProps> = ({
  isSearching,
}) => {
  const {
    localFilters,
    handleStatusChange,
    handleDestinationChange,
    handleClearFilters,
    handleSearch,
  } = useFlightFilters();

  return (
    <Card width="70vw">
      <CardHeader>
        <h2>Filter Flights</h2>
      </CardHeader>
      <CardContent>
        <FilterGrid>
          <FilterField>
            <Label htmlFor="statusFilter">Status</Label>
            <Select
              id="statusFilter"
              value={localFilters.status || ""}
              onChange={handleStatusChange}
              disabled={isSearching}
            >
              <option value="">All Statuses</option>
              {Object.values(FlightStatus).map((status) => (
                <option key={status} value={status}>
                  {status}
                </option>
              ))}
            </Select>
          </FilterField>

          <FilterField>
            <Label htmlFor="destinationFilter">Destination</Label>
            <Input
              id="destinationFilter"
              onChange={handleDestinationChange}
              placeholder="Filter by destination"
              disabled={isSearching}
            />
          </FilterField>

          <Button
            variant="primary"
            onClick={handleSearch}
            loading={isSearching}
          >
            Search
          </Button>

          <Button variant="secondary" onClick={handleClearFilters}>
            Clear Filters
          </Button>
        </FilterGrid>
      </CardContent>
    </Card>
  );
};
