import styled, { keyframes, css } from 'styled-components';
import type { ToastType } from './Toast.component';

const slideIn = keyframes`
  from {
    transform: translateX(100%);
    opacity: 0;
  }
  to {
    transform: translateX(0);
    opacity: 1;
  }
`;

const getPositionStyles = (position: string) => {
  switch (position) {
    case 'top-right':
      return css`
        top: 20px;
        right: 20px;
      `;
    case 'top-left':
      return css`
        top: 20px;
        left: 20px;
      `;
    case 'bottom-right':
      return css`
        bottom: 20px;
        right: 20px;
      `;
    case 'bottom-left':
      return css`
        bottom: 20px;
        left: 20px;
      `;
    case 'top-center':
      return css`
        top: 20px;
        left: 50%;
        transform: translateX(-50%);
      `;
    case 'bottom-center':
      return css`
        bottom: 20px;
        left: 50%;
        transform: translateX(-50%);
      `;
    default:
      return css`
        top: 20px;
        right: 20px;
      `;
  }
};

const getToastColors = (type: ToastType) => {
  switch (type) {
    case 'success':
      return {
        background: '#10b981',
        border: '#059669',
        color: '#ffffff'
      };
    case 'error':
      return {
        background: '#ef4444',
        border: '#dc2626',
        color: '#ffffff'
      };
    case 'warning':
      return {
        background: '#f59e0b',
        border: '#d97706',
        color: '#ffffff'
      };
    case 'info':
      return {
        background: '#3b82f6',
        border: '#2563eb',
        color: '#ffffff'
      };
    default:
      return {
        background: '#6b7280',
        border: '#4b5563',
        color: '#ffffff'
      };
  }
};

export const ToastContainer = styled.div<{ type: ToastType; position: string }>`
  position: fixed;
  ${({ position }) => getPositionStyles(position)};
  z-index: 9999;
  animation: ${slideIn} 0.3s ease-out;
  
  ${({ type }) => {
    const colors = getToastColors(type);
    return css`
      background: ${colors.background};
      border: 1px solid ${colors.border};
      color: ${colors.color};
    `;
  }}
  
  border-radius: 8px;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
  backdrop-filter: blur(10px);
  min-width: 300px;
  max-width: 400px;
`;

export const ToastContent = styled.div`
  display: flex;
  align-items: center;
  padding: 12px 16px;
  gap: 12px;
`;

export const ToastIcon = styled.div<{ type: ToastType }>`
  flex-shrink: 0;
  display: flex;
  align-items: center;
  justify-content: center;
`;

export const ToastMessage = styled.div`
  flex: 1;
  font-size: 14px;
  font-weight: 500;
  line-height: 1.4;
`;

export const CloseButton = styled.button`
  background: none;
  border: none;
  color: inherit;
  cursor: pointer;
  padding: 4px;
  border-radius: 4px;
  display: flex;
  align-items: center;
  justify-content: center;
  opacity: 0.8;
  transition: opacity 0.2s;

  &:hover {
    opacity: 1;
    background: rgba(255, 255, 255, 0.1);
  }
`;