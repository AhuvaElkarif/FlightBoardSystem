import axios from "axios";
import type { CreateFlightRequest, Flight, FlightFilters } from "../types/types";
import { API_ENDPOINTS } from "../utils/constants";

const API_BASE_URL = import.meta.env.VITE_API_URL;

export const flightApi = {
  getFlights: async (): Promise<Flight[]> => {
    const response = await axios.get(`${API_BASE_URL}${API_ENDPOINTS.FLIGHTS}`);
    if (!response.data) {
      throw new Error('Failed to fetch flights');
    }
    return response.data;
  },

  createFlight: async (flight: CreateFlightRequest): Promise<Flight> => {
    try {
      const response = await axios.post(`${API_BASE_URL}${API_ENDPOINTS.FLIGHTS}`, flight, {
        headers: {
          'Content-Type': 'application/json',
        },
      });
      return response.data;
    } catch (error: any) {
      const message = error.response?.data?.message || 'Failed to create flight';
      throw new Error(message);
    }
  },

  deleteFlight: async (id: string): Promise<void> => {
    try {
      await axios.delete(`${API_BASE_URL}${API_ENDPOINTS.FLIGHTS}/${id}`);
    } catch (error) {
      throw new Error('Failed to delete flight');
    }
  },

  searchFlights: async (filters: FlightFilters): Promise<Flight[]> => {
    try {
      const params: Record<string, string> = {};
      if (filters.status) params.status = filters.status;
      if (filters.destination) params.destination = filters.destination;

      const response = await axios.get(`${API_BASE_URL}${API_ENDPOINTS.FLIGHTS}/search`, { params });
      return response.data;
    } catch (error) {
      throw new Error('Failed to search flights');
    }
  }
};