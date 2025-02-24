import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { Sport } from "@/types/sport";

export const sportApi = createApi({
  reducerPath: "sportApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "/api", // adjust this to your API base URL
  }),
  tagTypes: ["Sport"],
  endpoints: (builder) => ({
    getSports: builder.query<Sport[], void>({
      query: () => "sports",
      providesTags: ["Sport"],
    }),
    getSportById: builder.query<Sport, string>({
      query: (id) => `sports/${id}`,
      providesTags: (_result, _error, id) => [{ type: "Sport", id }],
    }),
    createSport: builder.mutation<Sport, Partial<Sport>>({
      query: (body) => ({
        url: "sports",
        method: "POST",
        body,
      }),
      invalidatesTags: ["Sport"],
    }),
    updateSport: builder.mutation<Sport, { id: string; body: Partial<Sport> }>({
      query: ({ id, body }) => ({
        url: `sports/${id}`,
        method: "PUT",
        body,
      }),
      invalidatesTags: (_result, _error, { id }) => [{ type: "Sport", id }],
    }),
    deleteSport: builder.mutation<void, string>({
      query: (id) => ({
        url: `sports/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Sport"],
    }),
  }),
});

export const {
  useGetSportsQuery,
  useGetSportByIdQuery,
  useCreateSportMutation,
  useUpdateSportMutation,
  useDeleteSportMutation,
} = sportApi;
