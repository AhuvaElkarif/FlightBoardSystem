import styled from 'styled-components';

export const Card = styled.div<{width:string}>`
  background: white;
  border-radius: 8px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  padding: 24px;
  border: 1px solid #e5e7eb;
  width: ${props=> props.width};
  margin: 0 auto;
`;

export const CardHeader = styled.div`
  margin-bottom: 16px;
  
  h2 {
    margin: 0;
    font-size: 20px;
    font-weight: 600;
    color: #111827;
  }
`;

export const CardContent = styled.div`
  /* Content styles */
`;