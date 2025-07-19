import styled from 'styled-components';

export const Table = styled.table`
  width: 100%;
  border-collapse: collapse;
  background: white;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
`;

export const TableHeader = styled.thead`
  background-color: #f9fafb;
`;

export const TableBody = styled.tbody`
  /* Body styles */
`;

export const TableRow = styled.tr`
  border-bottom: 1px solid #e5e7eb;
  
  &:hover {
    background-color: #f9fafb;
  }
  
  &:last-child {
    border-bottom: none;
  }
`;

export const TableHead = styled.th`
  padding: 12px;
  text-align: left;
  font-weight: 600;
  color: #374151;
  font-size: 14px;
`;

export const TableCell = styled.td`
  padding: 12px;
  font-size: 14px;
  color: #374151;
`;