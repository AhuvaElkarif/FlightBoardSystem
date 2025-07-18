import { VALIDATION_MESSAGES, VALIDATION_PATTERNS } from "../../utils/constants";

export const formFields = [
    {
      id: 'flightNumber',
      label: 'Flight Number',
      placeholder: 'e.g., LH123',
      type: 'text',
      validation: {
        required: VALIDATION_MESSAGES.REQUIRED,
        pattern: {
          value: VALIDATION_PATTERNS.FLIGHT_NUMBER,
          message: VALIDATION_MESSAGES.FLIGHT_NUMBER_INVALID
        }
      }
    },
    {
      id: 'destination',
      label: 'Destination',
      placeholder: 'e.g., New York',
      type: 'text',
      validation: {
        required: VALIDATION_MESSAGES.REQUIRED,
        minLength: {
          value: 2,
          message: VALIDATION_MESSAGES.MIN_LENGTH(2)
        }
      }
    },
    {
      id: 'gate',
      label: 'Gate',
      placeholder: 'e.g., A12',
      type: 'text',
      validation: {
        required: VALIDATION_MESSAGES.REQUIRED,
        pattern: {
          value: VALIDATION_PATTERNS.GATE,
          message: VALIDATION_MESSAGES.GATE_INVALID
        }
      }
    },
    {
      id: 'departureTime',
      label: 'Departure Time',
      placeholder: '',
      type: 'datetime-local',
      validation: {
        required: VALIDATION_MESSAGES.REQUIRED,
        validate: (value: string) => {
          const selectedDate = new Date(value);
          const now = new Date();
          return selectedDate > now || VALIDATION_MESSAGES.FUTURE_DATE_REQUIRED;
        }
      }
    }
  ];