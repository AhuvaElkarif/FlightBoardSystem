import styled, { css } from 'styled-components';
import type { ButtonProps } from './Button.component';

export const ButtonBase = styled.button<ButtonProps>`
  display: inline-flex;
  align-items: center;
  justify-content: center;
  border: none;
  border-radius: 6px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s ease;
  gap: 8px;
  
  &:disabled {
    opacity: 0.6;
    cursor: not-allowed;
  }
  
  ${({ size }) => {
    switch (size) {
      case 'small':
        return css`
          padding: 6px 12px;
          font-size: 14px;
          height: 32px;
        `;
      case 'large':
        return css`
          padding: 12px 24px;
          font-size: 16px;
          height: 48px;
        `;
      default:
        return css`
          padding: 8px 16px;
          font-size: 14px;
          height: 40px;
        `;
    }
  }}
  
  ${({ variant }) => {
    switch (variant) {
      case 'primary':
        return css`
          background-color: #3b82f6;
          color: white;
          
          &:hover:not(:disabled) {
            background-color: #2563eb;
          }
        `;
      case 'danger':
        return css`
          background-color: #ef4444;
          color: white;
          
          &:hover:not(:disabled) {
            background-color: #dc2626;
          }
        `;
      default:
        return css`
          background-color: #f3f4f6;
          color: #374151;
          
          &:hover:not(:disabled) {
            background-color: #e5e7eb;
          }
        `;
    }
  }}
`;