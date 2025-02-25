import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { SportCategory } from "@/types/SportCategory";

interface SportCategoryState {
  selectedSportCategory: SportCategory | null;
  filters: {
    search: string;
    status: string;
  };
}

const initialState: SportCategoryState = {
  selectedSportCategory: null,
  filters: {
    search: "",
    status: "all",
  },
};

const sportCategorySlice = createSlice({
  name: "sportCategory",
  initialState,
  reducers: {
    setSelectedSportCategory: (
      state,
      action: PayloadAction<SportCategory | null>
    ) => {
      state.selectedSportCategory = action.payload;
    },
    setSportCategoryFilters: (
      state,
      action: PayloadAction<Partial<SportCategoryState["filters"]>>
    ) => {
      state.filters = { ...state.filters, ...action.payload };
    },
    resetSportCategoryFilters: (state) => {
      state.filters = initialState.filters;
    },
  },
});

export const {
  setSelectedSportCategory,
  setSportCategoryFilters,
  resetSportCategoryFilters,
} = sportCategorySlice.actions;
export default sportCategorySlice.reducer;
