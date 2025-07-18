import React from 'react';
import { type FieldError, type UseFormRegister } from 'react-hook-form';
import type { CreateFlightRequest } from '../../types/types';
import { Input } from '../ui/Input.styled';
import { ErrorMessage, FormField as StyledFormField, Label } from './CreateFlight.styled';

interface FormFieldProps {
  id: string;
  label: string;
  placeholder: string;
  type?: string;
  register: UseFormRegister<CreateFlightRequest>;
  validation: object;
  error?: FieldError;
  disabled?: boolean;
}

const FormField: React.FC<FormFieldProps> = ({
  id,
  label,
  placeholder,
  type = 'text',
  register,
  validation,
  error,
  disabled = false,
}) => {
  return (
    <StyledFormField>
      <Label htmlFor={id}>{label}</Label>
      <Input
        id={id}
        type={type}
        disabled={disabled}
        {...(error ? { 'aria-invalid': true, 'aria-describedby': `${id}-error` } : {})}
        {...register(id as keyof CreateFlightRequest, validation)}
        placeholder={placeholder}
      />
      {error && <ErrorMessage>{error.message}</ErrorMessage>}
    </StyledFormField>
  );
};
export default FormField;