import React from 'react';
import styled from 'styled-components';
import { FlightStatus } from '../../types/types';

const Badge = styled.span<{ status: FlightStatus }>`
  display: inline-block;
  padding: 4px 8px;
  border-radius: 4px;
  font-size: 12px;
  font-weight: 500;
  text-transform: uppercase;
  
  ${({ status }) => {
    switch (status) {
      case FlightStatus.Scheduled:
        return `
          background-color: #dbeafe;
          color: #1e40af;
        `;
      case FlightStatus.Boarding:
        return `
          background-color: #fef3c7;
          color: #92400e;
        `;
      case FlightStatus.Departed:
        return `
          background-color: #dcfce7;
          color: #166534;
        `;
      case FlightStatus.Landed:
        return `
          background-color: #f3e8ff;
          color: #7c3aed;
        `;
      default:
        return `
          background-color: #f3f4f6;
          color: #374151;
        `;
    }
  }}
`;

interface StatusBadgeProps {
  status: FlightStatus;
}

export const StatusBadge: React.FC<StatusBadgeProps> = ({ status }) => (
  <Badge status={status}>{status}</Badge>
);