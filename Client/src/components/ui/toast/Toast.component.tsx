import { useEffect } from 'react';
import { createPortal } from 'react-dom';
import { X, CheckCircle, XCircle, AlertCircle, Info } from 'lucide-react';
import { ToastContainer, ToastContent, ToastIcon, ToastMessage, CloseButton } from './Toast.styled';

export type ToastType = 'success' | 'error' | 'warning' | 'info';

export interface ToastProps {
  id: string;
  message: string;
  type: ToastType;
  duration?: number;
  onClose: (id: string) => void;
  position?: 'top-right' | 'top-left' | 'bottom-right' | 'bottom-left' | 'top-center' | 'bottom-center';
}

const Toast: React.FC<ToastProps> = ({
  id,
  message,
  type,
  duration = 4000,
  onClose,
  position = 'top-right'
}) => {
  useEffect(() => {
    const timer = setTimeout(() => {
      onClose(id);
    }, duration);

    return () => clearTimeout(timer);
  }, [id, duration, onClose]);

  const getIcon = () => {
    switch (type) {
      case 'success':
        return <CheckCircle size={20} />;
      case 'error':
        return <XCircle size={20} />;
      case 'warning':
        return <AlertCircle size={20} />;
      case 'info':
        return <Info size={20} />;
      default:
        return <Info size={20} />;
    }
  };

  const toastElement = (
    <ToastContainer type={type} position={position}>
      <ToastContent>
        <ToastIcon type={type}>
          {getIcon()}
        </ToastIcon>
        <ToastMessage>{message}</ToastMessage>
        <CloseButton onClick={() => onClose(id)}>
          <X size={16} />
        </CloseButton>
      </ToastContent>
    </ToastContainer>
  );

  return createPortal(toastElement, document.body);
};

export default Toast;