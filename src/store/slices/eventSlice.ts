import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Event } from "@/types/event";

interface EventState {
  selectedEvent: Event | null;
  filters: {
    search: string;
    status: string;
    sportId?: number;
  };
}

const initialState: EventState = {
  selectedEvent: null,
  filters: {
    search: "",
    status: "all",
  },
};

const eventSlice = createSlice({
  name: "events",
  initialState,
  reducers: {
    setSelectedEvent: (state, action: PayloadAction<Event | null>) => {
      state.selectedEvent = action.payload;
    },
    setEventFilters: (
      state,
      action: PayloadAction<Partial<EventState["filters"]>>
    ) => {
      state.filters = { ...state.filters, ...action.payload };
    },
    resetEventFilters: (state) => {
      state.filters = initialState.filters;
    },
  },
});

export const { setSelectedEvent, setEventFilters, resetEventFilters } =
  eventSlice.actions;
export default eventSlice.reducer;
