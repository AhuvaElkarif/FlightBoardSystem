import { format, isValid } from 'date-fns';

export const formatDateTime = (dateString: string): string => {
  const date = new Date(dateString);
  if (!isValid(date)) {
    return 'Invalid Date';
  }
  return format(date, 'MMM dd, yyyy HH:mm');
};

export const formatDateTimeInput = (dateString: string): string => {
  const date = new Date(dateString);
  if (!isValid(date)) {
    return '';
  }
  return format(date, "yyyy-MM-dd'T'HH:mm");
};

export const isDateInFuture = (dateString: string): boolean => {
  const date = new Date(dateString);
  const now = new Date();
  return isValid(date) && date > now;
};