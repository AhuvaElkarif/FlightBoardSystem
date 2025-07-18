import { BrowserRouter as Router } from "react-router-dom";
import { AppProviders } from "./providers/AppProviders.component";
import { AppRoutes } from "./routes/AppRoutes";
import { Toaster } from "react-hot-toast";
import { SignalRWrapper } from "./components/common/SignalRWrapper.component";

const App: React.FC = () => {
  return (
    <AppProviders>
      <Router>
        <SignalRWrapper />
        <Toaster />
        <AppRoutes />
      </Router>
    </AppProviders>
  );
};
export default App;
