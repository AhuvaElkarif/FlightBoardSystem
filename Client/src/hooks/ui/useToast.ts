import toast from "react-hot-toast";

export const useToast = () => {
  const showSuccess = (message: string, options = {}) => {
    toast.success(message, {
      duration: 4000,
      position: "top-right",
      style: {
        background: "#10b981",
        color: "#fff",
      },
      ...options,
    });
  };

  const showError = (message: string, options = {}) => {
    toast.error(message, {
      duration: 4000,
      position: "top-right",
      style: {
        background: "#ef4444",
        color: "#fff",
      },
      ...options,
    });
  };

  const showPromise = (promise:Promise<any>, messages: any, options = {}) => {
    return toast.promise(
      promise,
      {
        loading: messages.loading || 'טוען...',
        success: messages.success || 'בוצע בהצלחה!',
        error: messages.error || 'אירעה שגיאה',
      },
      {
        style: {
          minWidth: '250px',
        },
        success: {
          duration: 4000,
          icon: '🎉',
        },
        error: {
          duration: 4000,
          icon: '❌',
        },
        ...options
      }
    );
  };

  const showLoading = (message: string) => {
    return toast.loading(message);
  };

  const dismissToast = (toastId: string) => {
    toast.dismiss(toastId);
  };

  const dismissAll = () => {
    toast.dismiss();
  };

  return {
    showSuccess,
    showError,
    showPromise,
    showLoading,
    dismissToast,
    dismissAll,
  };
};
