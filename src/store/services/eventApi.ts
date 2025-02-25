import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { Event } from "@/types/event";

export const eventApi = createApi({
  reducerPath: "eventApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "https://localhost:7221/api/v1", // adjust this to your API base URL
  }),
  tagTypes: ["Event"],
  endpoints: (builder) => ({
    getEvents: builder.query<Event[], void>({
      query: () => "event",
      providesTags: ["Event"],
    }),
    getEventById: builder.query<Event, string>({
      query: (id) => `event/${id}`,
      providesTags: (_result, _error, id) => [{ type: "Event", id }],
    }),
    createEvent: builder.mutation<Event, Partial<Event>>({
      query: (body) => ({
        url: "event",
        method: "POST",
        body,
      }),
      invalidatesTags: ["Event"],
    }),
    updateEvent: builder.mutation<Event, { id: string; body: Partial<Event> }>({
      query: ({ id, body }) => ({
        url: `event/${id}`,
        method: "PUT",
        body,
      }),
      invalidatesTags: (_result, _error, { id }) => [{ type: "Event", id }],
    }),
    deleteEvent: builder.mutation<void, string>({
      query: (id) => ({
        url: `event/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Event"],
    }),
  }),
});

export const {
  useGetEventsQuery,
  useGetEventByIdQuery,
  useCreateEventMutation,
  useUpdateEventMutation,
  useDeleteEventMutation,
} = eventApi;
