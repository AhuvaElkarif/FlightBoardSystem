import styled from "styled-components";

export const FormContainer = styled.div`
  width: 100vw;
  height: 100vh;
  margin: 0 auto;
  padding: 24px;
  display: flex;
  align-content: center;
  align-items: center;
`;

export const Label = styled.label`
  font-size: 14px;
  font-weight: 500;
  color: #374151;
`;

export const FormFlex = styled.div`
  display: flex;
  flex-direction: column;
  gap: 16px;
  margin-bottom: 16px;
`;

export const FormField = styled.div`
  display: flex;
  flex-direction: column;
  gap: 4px;
`;

export const ErrorMessage = styled.span`
  color: #ef4444;
  font-size: 12px;
`;
