import React from "react";
import { Link, useNavigate } from "react-router-dom";
import {
  Container,
  Header,
  Title,
  NavigationLinks,
  NavigationLink,
  WelcomeSection,
  WelcomeText,
  WelcomeDescription,
  CardGrid,
  FeatureCard,
  FeatureIcon,
  FeatureTitle,
  FeatureDescription,
  ActionButton,
  ParticleEffect,
} from "./FlightsBoard.styled";

export const FlightsBoard: React.FC = () => {
  const navigate = useNavigate();

  const handleCreateFlightClick = () => {
    navigate("/create-flight");
  };

  const handleFlightsTableClick = () => {
    navigate("/flights-table");
  };

  return (
    <Container>
      <ParticleEffect />
      
      <Header>
        <Title>✈️ Flights Management System</Title>
        <NavigationLinks>
          <NavigationLink>
            <Link to="/create-flight">Create Flight</Link>
          </NavigationLink>
          <NavigationLink>
            <Link to="/flights-table">Flights Table</Link>
          </NavigationLink>
        </NavigationLinks>
      </Header>

      <WelcomeSection>
        <WelcomeText>Welcome to the Future of Flight Management</WelcomeText>
        <WelcomeDescription>
          Streamline your aviation operations with our cutting-edge flight management system. 
          Experience seamless booking, real-time tracking, and comprehensive analytics.
        </WelcomeDescription>
      </WelcomeSection>

      <CardGrid>
        <FeatureCard>
          <FeatureIcon>🚀</FeatureIcon>
          <FeatureTitle>Create Flight</FeatureTitle>
          <FeatureDescription>
            Effortlessly add new flights to your system with our intuitive interface. 
            Set flight numbers, destinations, gates, departure times, and more with just a few clicks.
          </FeatureDescription>
          <ActionButton onClick={handleCreateFlightClick}>
            Start Creating
          </ActionButton>
        </FeatureCard>

        <FeatureCard>
          <FeatureIcon>📈</FeatureIcon>
          <FeatureTitle>Manage Flights</FeatureTitle>
          <FeatureDescription>
            Access your comprehensive flight dashboard. Monitor real-time status, 
            filter by multiple criteria, and manage all your flights from one central location.
          </FeatureDescription>
          <ActionButton onClick={handleFlightsTableClick}>
            View Dashboard
          </ActionButton>
        </FeatureCard>
      </CardGrid>
    </Container>
  );
};