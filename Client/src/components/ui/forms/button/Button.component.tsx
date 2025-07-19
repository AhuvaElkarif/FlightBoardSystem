import { ButtonBase } from "./Button.styled";

export interface ButtonProps {
  variant?: "primary" | "secondary" | "danger";
  size?: "small" | "medium" | "large";
  disabled?: boolean;
  loading?: boolean;
}
export const Button: React.FC<ButtonProps & React.ButtonHTMLAttributes<HTMLButtonElement>>
 = ({ children, loading, disabled, ...props }) => (
  <ButtonBase disabled={disabled || loading} {...props}>
    {loading && <span>Loading...</span>}
    {children}
  </ButtonBase>
);
