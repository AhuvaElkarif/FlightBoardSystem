import React from 'react';
import { useAppDispatch, useAppSelector } from '../../store';
import { setStatusFilter, setDestinationFilter, clearFilters } from '../../store/slices/filtersSlice';
import { FlightStatus } from '../../types/types';
import { Button } from '../ui/button/Button.component';
import { Input, Select } from '../ui/Input.styled';
import { Card, CardHeader, CardContent } from '../ui/Card.styled';
import { FilterField, FilterGrid, Label } from './FlightTable.styled';

interface FlightFiltersProps {
  onSearch: () => void;
  isSearching?: boolean;
}

export const FlightFilters: React.FC<FlightFiltersProps> = ({ onSearch, isSearching }) => {
  const dispatch = useAppDispatch();
  const { filters } = useAppSelector(state => state.filters);

  const handleStatusChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const value = e.target.value || undefined;
    dispatch(setStatusFilter(value));
  };

  const handleDestinationChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value || undefined;
    dispatch(setDestinationFilter(value));
  };

  const handleClearFilters = () => {
    dispatch(clearFilters());
  };

  return (
    <Card width='70vw'>
      <CardHeader>
        <h2>Filter Flights</h2>
      </CardHeader>
      <CardContent>
        <FilterGrid>
          <FilterField>
            <Label htmlFor="statusFilter">Status</Label>
            <Select
              id="statusFilter"
              value={filters.status || ''}
              onChange={handleStatusChange}
            >
              <option value="">All Statuses</option>
              {Object.values(FlightStatus).map(status => (
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
              value={filters.destination || ''}
              onChange={handleDestinationChange}
              placeholder="Filter by destination"
            />
          </FilterField>

          <Button variant="primary" onClick={onSearch} loading={isSearching}>
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