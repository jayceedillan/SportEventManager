import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Sport } from "@/types/sport";

interface SportState {
  selectedSport: Sport | null;
  filters: {
    search: string;
    status: string;
  };
}

const initialState: SportState = {
  selectedSport: null,
  filters: {
    search: "",
    status: "all",
  },
};

const sportSlice = createSlice({
  name: "sports",
  initialState,
  reducers: {
    setSelectedSport: (state, action: PayloadAction<Sport | null>) => {
      state.selectedSport = action.payload;
    },
    setSportFilters: (
      state,
      action: PayloadAction<Partial<SportState["filters"]>>
    ) => {
      state.filters = { ...state.filters, ...action.payload };
    },
    resetSportFilters: (state) => {
      state.filters = initialState.filters;
    },
  },
});

export const { setSelectedSport, setSportFilters, resetSportFilters } =
  sportSlice.actions;
export default sportSlice.reducer;
