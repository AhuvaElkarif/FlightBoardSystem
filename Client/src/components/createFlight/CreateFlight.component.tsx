import React, { useCallback } from "react";
import { useForm } from "react-hook-form";
import { Card, CardContent, CardHeader } from "../ui/Card.styled";
import { Button } from "../ui/button/Button.component";
import { FormContainer, FormFlex } from "./CreateFlight.styled";
import { formFields } from "./utils";
import FormField from "./FormField.component";
import { useFlights } from "../../hooks/api/flights/useFlights";
import type { CreateFlightRequest } from "../../types/types";
import Swal from "sweetalert2";

export const CreateFlight: React.FC = () => {
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    reset,
  } = useForm<CreateFlightRequest>();

  const { isCreating, createFlight } = useFlights();
  const isLoading = isCreating || isSubmitting;

  const onSubmit = useCallback(
    async (flightData: CreateFlightRequest) => {
      try {
        await createFlight(flightData);
        reset();
        Swal.fire({
          title: "Flight Created Successfully!",
          icon: "success",
          draggable: true,
        });
      } catch (err) {
        console.error("Failed to create flight:", err);
      }
    },
    [createFlight, reset]
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
