import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { Event } from "@/types/event";

export const eventApi = createApi({
  reducerPath: "eventApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "/api", // adjust this to your API base URL
  }),
  tagTypes: ["Event"],
  endpoints: (builder) => ({
    getEvents: builder.query<Event[], void>({
      query: () => "events",
      providesTags: ["Event"],
    }),
    getEventById: builder.query<Event, string>({
      query: (id) => `events/${id}`,
      providesTags: (_result, _error, id) => [{ type: "Event", id }],
    }),
    createEvent: builder.mutation<Event, Partial<Event>>({
      query: (body) => ({
        url: "events",
        method: "POST",
        body,
      }),
      invalidatesTags: ["Event"],
    }),
    updateEvent: builder.mutation<Event, { id: string; body: Partial<Event> }>({
      query: ({ id, body }) => ({
        url: `events/${id}`,
        method: "PUT",
        body,
      }),
      invalidatesTags: (_result, _error, { id }) => [{ type: "Event", id }],
    }),
    deleteEvent: builder.mutation<void, string>({
      query: (id) => ({
        url: `events/${id}`,
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
