import { createSlice } from '@reduxjs/toolkit';
import type { PayloadAction } from '@reduxjs/toolkit';
import type { FlightFilters } from '../../types/types';

interface FiltersState {
  filters: FlightFilters;
}

const initialState: FiltersState = {
  filters: {}
};

const filtersSlice = createSlice({
  name: 'filters',
  initialState,
  reducers: {
    setFilters: (state, action: PayloadAction<FlightFilters>) => {
      state.filters = action.payload;
    },
    clearFilters: (state) => {
      state.filters = {};
    },
    setStatusFilter: (state, action: PayloadAction<string | undefined>) => {
      state.filters.status = action.payload as any;
    },
    setDestinationFilter: (state, action: PayloadAction<string | undefined>) => {
      state.filters.destination = action.payload;
    }
  }
});

export const { setFilters, clearFilters, setStatusFilter, setDestinationFilter } = filtersSlice.actions;
export default filtersSlice.reducer;