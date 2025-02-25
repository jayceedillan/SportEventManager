import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { Sport } from "@/types/sport";

export interface PaginationFilterDto {
  page?: number;
  pageNumber?: number;
  pageSize?: number;
  searchTerm?: string;
  sortBy?: string;
  sortDescending: true | false;
}

// Define the paginated response interface
export interface PaginatedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

export const sportApi = createApi({
  reducerPath: "sportApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "https://localhost:7221/api/v1", // adjust this to your API base URL
  }),
  tagTypes: ["Sport"],
  endpoints: (builder) => ({
    getSports: builder.query<
      PaginatedResult<Sport>,
      PaginationFilterDto | void
    >({
      query: (params?: PaginationFilterDto) => ({
        url: "sport",
        params: {
          pageNumber: params?.pageNumber ?? 1,
          pageSize: params?.pageSize ?? 10,
          searchTerm: params?.searchTerm,
          sortBy: params?.sortBy,
          sortDescending: params?.sortDescending,
        },
      }),
      providesTags: (result) =>
        result
          ? [
              ...result.items.map(({ id }) => ({
                type: "Sport" as const,
                id,
              })),
              { type: "Sport", id: "LIST" },
            ]
          : [{ type: "Sport", id: "LIST" }],
    }),
    getSportById: builder.query<Sport, number>({
      query: (id) => `sport/${id}`,
      providesTags: (_result, _error, id) => [{ type: "Sport", id }],
    }),
    createSport: builder.mutation<Sport, Partial<Sport>>({
      query: (body) => ({
        url: "sport",
        method: "POST",
        body,
      }),
      invalidatesTags: ["Sport"],
    }),
    updateSport: builder.mutation<Sport, { id: string; body: Partial<Sport> }>({
      query: ({ id, body }) => ({
        url: `sport/${id}`,
        method: "PUT",
        body,
      }),
      invalidatesTags: (_result, _error, { id }) => [{ type: "Sport", id }],
    }),
    deleteSport: builder.mutation<void, string>({
      query: (id) => ({
        url: `sport/${id}`,
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
