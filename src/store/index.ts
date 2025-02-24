import { configureStore } from "@reduxjs/toolkit";
import eventReducer from "./slices/eventSlice";
import sportReducer from "./slices/sportSlice";
import uiReducer from "./slices/uiSlice";
import { eventApi } from "./services/eventApi";
import { sportApi } from "./services/sportApi";

export const store = configureStore({
  reducer: {
    events: eventReducer,
    sports: sportReducer,
    ui: uiReducer,
    [eventApi.reducerPath]: eventApi.reducer,
    [sportApi.reducerPath]: sportApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(eventApi.middleware, sportApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
