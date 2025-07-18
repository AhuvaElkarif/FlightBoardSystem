import { useState, useCallback } from 'react';
import { useAppDispatch } from '../store';
import { clearFilters, setFilters } from '../store/slices/filtersSlice';
import { FlightStatus, type FlightFilters } from '../types/types';

export const useFlightFilters = () => {
  const dispatch = useAppDispatch();
  const [localFilters, setLocalFilters] = useState<FlightFilters>({});

  const handleStatusChange = useCallback((e: React.ChangeEvent<HTMLSelectElement>) => {
    const value = e.target.value || undefined;
    setLocalFilters((prev) => ({ ...prev, status: value as FlightStatus }));
  }, []);

  const handleDestinationChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value || undefined;
    setLocalFilters((prev) => ({ ...prev, destination: value }));
  }, []);

  const handleClearFilters = useCallback(() => {
    setLocalFilters({});
    dispatch(clearFilters());
  }, [dispatch]);

  const handleSearch = useCallback(() => {
    dispatch(setFilters(localFilters));
  }, [dispatch, localFilters]);

  return {
    localFilters,
    handleStatusChange,
    handleDestinationChange,
    handleClearFilters,
    handleSearch,
  };
};