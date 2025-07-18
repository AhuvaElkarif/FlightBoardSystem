import styled from "styled-components";

const colors = {
  primary: '#2563eb',      
  secondary: '#1e40af',   
  accent: '#0ea5e9',      
  success: '#059669',      
  warning: '#d97706',      
  error: '#dc2626',        
  neutral: {
    50: '#f8fafc',
    100: '#f1f5f9',
    200: '#e2e8f0',
    300: '#cbd5e1',
    400: '#94a3b8',
    500: '#64748b',
    600: '#475569',
    700: '#334155',
    800: '#1e293b',
    900: '#0f172a'
  }
};

export const LoadingSpinner = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  height: 200px;
  font-size: 18px;
  color: ${colors.neutral[500]};
  
  &::after {
    content: '';
    width: 40px;
    height: 40px;
    border: 4px solid ${colors.neutral[200]};
    border-top: 4px solid ${colors.primary};
    border-radius: 50%;
    animation: spin 1s linear infinite;
    margin-left: 1rem;
  }
  
  @keyframes spin {
    0% { transform: rotate(0deg); }
    100% { transform: rotate(360deg); }
  }
`;

export const Section = styled.section`
  margin-bottom: 32px;
`;

export const ErrorMessageBoard = styled.div`
  background-color: #fee2e2;
  border: 1px solid #fecaca;
  color: #b91c1c;
  padding: 12px;
  border-radius: 6px;
  margin-bottom: 16px;
`;

export const Container = styled.div`
  min-height: 100vh;
  width: 100%;
  max-width: 100vw;
  background: linear-gradient(135deg, #f8fafc 0%, #e2e8f0 25%, #cbd5e1 100%);
  position: relative;
  color: ${colors.neutral[800]};
  
  &::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: 
      radial-gradient(circle at 20% 80%, rgba(37, 99, 235, 0.1) 0%, transparent 50%),
      radial-gradient(circle at 80% 20%, rgba(14, 165, 233, 0.08) 0%, transparent 50%);
    pointer-events: none;
  }
`;

export const Header = styled.header`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 2rem 4rem;
  background: rgba(255, 255, 255, 0.8);
  backdrop-filter: blur(20px);
  border-bottom: 1px solid rgba(203, 213, 225, 0.3);
  position: relative;
  z-index: 10;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  
  @media (max-width: 768px) {
    padding: 1.5rem 2rem;
    flex-direction: column;
    gap: 1rem;
  }
`;

export const Title = styled.h1`
  font-size: 2.5rem;
  font-weight: 700;
  margin: 0;
  background: linear-gradient(135deg, ${colors.primary} 0%, ${colors.secondary} 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
  text-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  
  @media (max-width: 768px) {
    font-size: 2rem;
  }
`;

export const NavigationLinks = styled.nav`
  display: flex;
  gap: 1.5rem;
  
  @media (max-width: 768px) {
    gap: 1rem;
  }
`;

export const NavigationLink = styled.div`
  a {
    color: ${colors.neutral[700]};
    text-decoration: none;
    padding: 0.75rem 1.5rem;
    border: 2px solid ${colors.primary};
    border-radius: 50px;
    font-weight: 500;
    transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
    display: block;
    position: relative;
    overflow: hidden;
    
    &::before {
      content: '';
      position: absolute;
      top: 0;
      left: -100%;
      width: 100%;
      height: 100%;
      background: linear-gradient(90deg, transparent, rgba(37, 99, 235, 0.1), transparent);
      transition: left 0.5s ease;
    }
    
    &:hover {
      background: ${colors.primary};
      color: white;
      border-color: ${colors.secondary};
      transform: translateY(-2px);
      box-shadow: 0 8px 25px rgba(37, 99, 235, 0.25);
      
      &::before {
        left: 100%;
      }
    }
    
    &:active {
      transform: translateY(0);
    }
  }
`;

export const WelcomeSection = styled.section`
  text-align: center;
  padding: 4rem 2rem;
  max-width: 900px;
  margin: 0 auto;
  position: relative;
  z-index: 5;
`;

export const WelcomeText = styled.h2`
  font-size: 3.5rem;
  margin-bottom: 1.5rem;
  font-weight: 300;
  background: linear-gradient(135deg, ${colors.neutral[800]} 0%, ${colors.primary} 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
  
  @media (max-width: 768px) {
    font-size: 2.5rem;
  }
`;

export const WelcomeDescription = styled.p`
  font-size: 1.25rem;
  color: ${colors.neutral[600]};
  margin-bottom: 2rem;
  line-height: 1.6;
`;

export const CardGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(400px, 1fr));
  gap: 2rem;
  padding: 2rem 4rem 4rem;
  max-width: 1200px;
  margin: 0 auto;
  position: relative;
  z-index: 5;
  
  @media (max-width: 768px) {
    grid-template-columns: 1fr;
    padding: 1rem 2rem 2rem;
    gap: 1.5rem;
  }
`;

export const ActionButton = styled.button`
  background: linear-gradient(135deg, #0ea5e9 0%, #2563eb 100%);
  color: white;
  border: none;
  padding: 0.875rem 2rem;
  border-radius: 50px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  margin-top: 1.5rem;
  position: relative;
  overflow: hidden;
  
  &::before {
    content: '';
    position: absolute;
    top: 0;
    left: -100%;
    width: 100%;
    height: 100%;
    background: linear-gradient(90deg, transparent, rgba(255, 255, 255, 0.2), transparent);
    transition: left 0.5s ease;
  }
  
  &:hover {
    transform: translateY(-3px);
    box-shadow: 0 12px 35px rgba(37, 99, 235, 0.4);
    
    &::before {
      left: 100%;
    }
  }
  
  &:active {
    transform: translateY(-1px);
  }
`;

export const FeatureCard = styled.div`
  background: rgba(255, 255, 255, 0.9);
  backdrop-filter: blur(20px);
  border: 1px solid rgba(203, 213, 225, 0.3);
  border-radius: 24px;
  padding: 2.5rem;
  text-align: center;
  transition: all 0.4s cubic-bezier(0.4, 0, 0.2, 1);
  position: relative;
  overflow: hidden;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.07);
  
  &::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: linear-gradient(135deg, rgba(37, 99, 235, 0.05) 0%, transparent 50%);
    opacity: 0;
    transition: opacity 0.3s ease;
  }
  
  &:hover {
    transform: translateY(-8px);
    box-shadow: 0 20px 50px rgba(0, 0, 0, 0.15);
    background: rgba(255, 255, 255, 0.95);
    border-color: rgba(37, 99, 235, 0.2);
    
    &::before {
      opacity: 1;
    }
  }
`;

export const FeatureIcon = styled.div`
  font-size: 4rem;
  margin-bottom: 1.5rem;
  filter: drop-shadow(0 2px 4px rgba(0, 0, 0, 0.1));
  
  transition: transform 0.3s ease;
  
  ${FeatureCard}:hover & {
    transform: scale(1.1) rotate(5deg);
  }
`;

export const FeatureTitle = styled.h3`
  font-size: 1.75rem;
  margin-bottom: 1rem;
  font-weight: 600;
  color: ${colors.neutral[800]};
`;

export const FeatureDescription = styled.p`
  font-size: 1.1rem;
  line-height: 1.7;
  margin-bottom: 1.5rem;
  color: ${colors.neutral[600]};
`;

export const ParticleEffect = styled.div`
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  overflow: hidden;
  pointer-events: none;
  z-index: 1;
  
  &::before,
  &::after {
    content: '';
    position: absolute;
    width: 4px;
    height: 4px;
    background: rgba(37, 99, 235, 0.3);
    border-radius: 50%;
    animation: float 6s ease-in-out infinite;
  }
  
  &::before {
    top: 20%;
    left: 10%;
    animation-delay: 0s;
  }
  
  &::after {
    top: 60%;
    right: 15%;
    animation-delay: 3s;
  }
  
  @keyframes float {
    0%, 100% {
      transform: translateY(0px) rotate(0deg);
      opacity: 0.3;
    }
    50% {
      transform: translateY(-20px) rotate(180deg);
      opacity: 0.6;
    }
  }
`;