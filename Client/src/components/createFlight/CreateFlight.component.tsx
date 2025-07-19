import React, { useCallback } from "react";
import { useForm } from "react-hook-form";
import { Card, CardContent, CardHeader } from "../ui/layout/Card.styled";
import { Button } from "../ui/forms/button/Button.component";
import { FormContainer, FormFlex } from "./CreateFlight.styled";
import { formFields } from "./utils";
import FormField from "./FormField.component";
import { useFlights } from "../../hooks/api/flights/useFlights";
import type { CreateFlightRequest } from "../../types/types";
import { useToast } from "../../hooks/ui/useToast";

export const CreateFlight: React.FC = () => {
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    reset,
  } = useForm<CreateFlightRequest>();

  const { isCreating, createFlightAsync } = useFlights();
  const isLoading = isCreating || isSubmitting;
  const { showPromise } = useToast();

  const onSubmit = useCallback(
    async (flightData: CreateFlightRequest) => {
      try {
        showPromise(createFlightAsync(flightData), {
          loading: "Saving flight...",
          success: "The flight was created successfully!",
          error: "The flight number already exists",
        });
        reset();
      } catch (err) {
        console.error("Failed to create flight:", err);
      }
    },
    [createFlightAsync, showPromise, reset]
  );
  return (
    <FormContainer>
      <Card width="30vw">
        <CardHeader>
          <h2>Add New Flight</h2>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit(onSubmit)}>
            <FormFlex>
              {formFields.map((field) => (
                <FormField
                  key={field.id}
                  id={field.id}
                  label={field.label}
                  placeholder={field.placeholder}
                  type={field.type}
                  register={register}
                  validation={field.validation}
                  disabled={isLoading}
                  error={errors[field.id as keyof CreateFlightRequest]}
                />
              ))}
            </FormFlex>

            <Button type="submit" variant="primary" loading={isLoading}>
              Add Flight
            </Button>
          </form>
        </CardContent>
      </Card>
    </FormContainer>
  );
};
