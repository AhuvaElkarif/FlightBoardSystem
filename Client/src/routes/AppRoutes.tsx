import { Navigate, Route, Routes } from "react-router-dom";
import { FlightsBoard } from "../components/flightsBoard/FlightsBoard.component";
import { FlightData } from "../components/flightTable/FlightsData.component";
import { CreateFlight } from "../components/createFlight/CreateFlight.component";

export const AppRoutes: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<FlightsBoard />} />
      <Route path="/create-flight" element={<CreateFlight />} />
      <Route path="/flights-table" element={<FlightData />} />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
};
