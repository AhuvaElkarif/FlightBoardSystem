import { Navigate, Route, Routes } from "react-router-dom";
import { FlightsBoard } from "../components/flightsBoard/FlightsBoard.component";
import { FlightForm } from "../components/flightForm/FlightForm.component";
import { FlightData } from "../components/flightTable/FlightsData.component";

export const AppRoutes: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<FlightsBoard />} />
      <Route path="/create-flight" element={<FlightForm />} />
      <Route path="/flights-table" element={<FlightData />} />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
};
