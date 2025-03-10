import { useState } from "react";

interface ModalState {
  isOpen: boolean;
  itemId: number | null;
}

export function useModalState() {
  const [modalState, setModalState] = useState<ModalState>({
    isOpen: false,
    itemId: null,
  });

  const openModal = (id: number) => {
    setModalState({ isOpen: true, itemId: id });
  };

  const closeModal = () => {
    setModalState({ isOpen: false, itemId: null });
  };

  return { modalState, openModal, closeModal };
}
