// import React from "react";
// import { useFlights } from "../../hooks/useFlights";
// import { useAppSelector } from "../../store";
// import { useSignalR } from "../../hooks/useSignalR";
// import type { CreateFlightRequest } from "../../types/types";
// import { FlightForm } from "../flightForm/FlightForm.component";
// import { FlightFilters } from "../flightTable/FlightFilters.component";
// import { FlightTable } from "../flightTable/FlightTable.component";
// import {
//   Container,
//   ErrorMessageBoard,
//   Header,
//   LoadingSpinner,
//   Section,
//   Title,
// } from "./Flights.styled";

// export const FlightBoard: React.FC = () => {
//   const { filters } = useAppSelector((state) => state.filters);
//   const {
//     flights,
//     isLoading,
//     error,
//     createFlight,
//     deleteFlight,
//     isCreating,
//     isDeleting,
//     createError,
//     deleteError,
//   } = useFlights(filters);

//   useSignalR();

//   const handleCreateFlight = (flightData: CreateFlightRequest) => {
//     createFlight(flightData);
//   };

//   const handleDeleteFlight = (id: string) => {
//     deleteFlight(id);
//   };

//   const handleSearch = () => {
//     // The search is handled automatically by the useFlights hook
//     // when filters change due to the query key dependency
//   };

//   if (isLoading) {
//     return (
//       <Container>
//         <LoadingSpinner>Loading flights...</LoadingSpinner>
//       </Container>
//     );
//   }

//   return (
//     <Container>
//       <Header>
//         <Title>Flight Board</Title>
//       </Header>

//       <Section>
//         <FlightForm onSubmit={handleCreateFlight} isSubmitting={isCreating} />
//       </Section>

//       {createError && (
//         <ErrorMessageBoard>
//           Error creating flight: {createError.message}
//         </ErrorMessageBoard>
//       )}

//       {deleteError && (
//         <ErrorMessageBoard>
//           Error deleting flight: {deleteError.message}
//         </ErrorMessageBoard>
//       )}

//       <Section>
//         <FlightFilters onSearch={handleSearch} isSearching={isLoading} />
//       </Section>

//       <Section>
//         {error ? (
//           <ErrorMessageBoard>Error loading flights: {error.message}</ErrorMessageBoard>
//         ) : (
//           <FlightTable
//             flights={flights}
//             onDeleteFlight={handleDeleteFlight}
//             isDeleting={isDeleting}
//           />
//         )}
//       </Section>
//     </Container>
//   );
// };