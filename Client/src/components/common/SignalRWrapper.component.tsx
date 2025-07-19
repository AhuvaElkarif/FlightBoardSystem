import { useSignalR } from "../../hooks/realtime/useSignalR";

export const SignalRWrapper: React.FC = () => {
  useSignalR(); 
  return null;  
};
