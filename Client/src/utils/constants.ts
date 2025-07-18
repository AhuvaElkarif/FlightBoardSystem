export const API_ENDPOINTS = {
  FLIGHTS: '/api/flights',
  SEARCH_FLIGHTS: '/api/flights/search',
  SIGNALR_HUB: '/flightBoardhub',
} as const;

export const QUERY_KEYS = {
  FLIGHTS: 'flights',
  SEARCH_FLIGHTS: 'searchFlights',
} as const;

export const VALIDATION_PATTERNS = {
  FLIGHT_NUMBER: /^[A-Z]{2}\d{3,4}$/,
  GATE: /^[A-Z]?\d+[A-Z]?$/,
} as const;

export const VALIDATION_MESSAGES = {
  REQUIRED: 'This field is required',
  FLIGHT_NUMBER_INVALID: 'Flight number must be in format AB123 or AB1234',
  GATE_INVALID: 'Gate must be in format A1, 12, or 12A',
  FUTURE_DATE_REQUIRED: 'Departure time must be in the future',
  MIN_LENGTH: (min: number) => `Must be at least ${min} characters`,
} as const;

export const TABLE_HEADERS = [
    "Flight Number",
    "Destination",
    "Departure Time",
    "Gate",
    "Status",
    "Actions",
  ];