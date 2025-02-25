import { configureStore } from "@reduxjs/toolkit";

import eventReducer from "./slices/eventSlice";
import sportReducer from "./slices/sportSlice";
import sportsCategoryReducer from "./slices/sportCategorySlice";
import uiReducer from "./slices/uiSlice";
import { eventApi } from "./services/eventApi";
import { sportApi } from "./services/sportApi";
import { sportCategoryApi } from "./services/sportCategoryApi";

export const store = configureStore({
  reducer: {
    events: eventReducer,
    sports: sportReducer,
    sportsCategory: sportsCategoryReducer,
    ui: uiReducer,
    [eventApi.reducerPath]: eventApi.reducer,
    [sportApi.reducerPath]: sportApi.reducer,
    [sportCategoryApi.reducerPath]: sportCategoryApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(
      eventApi.middleware,
      sportApi.middleware,
      sportCategoryApi.middleware
    ),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
