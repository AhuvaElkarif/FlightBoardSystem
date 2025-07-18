import styled from "styled-components";
import { Button } from "../ui/button/Button.component";

export const Container = styled.div`
  min-height: 100vh;
  width: 70vw;
  max-width: 100vw;
  margin: 0 auto;
  padding: 24px 0;
`;

export const DataTitle = styled.h1`
  font-size: 1.5rem;
  text-align: center;
`;

export const EmptyState = styled.div`
  text-align: center;
  padding: 48px;
  color: #6b7280;
`;

export const DeleteButton = styled(Button)`
  padding: 4px 8px;
  height: auto;
  min-width: auto;
`;

export const FilterGrid = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr auto auto;
  gap: 16px;
  align-items: end;
`;

export const FilterField = styled.div`
  display: flex;
  flex-direction: column;
  gap: 4px;
`;

export const Label = styled.label`
  font-size: 14px;
  font-weight: 500;
  color: #374151;
`;
