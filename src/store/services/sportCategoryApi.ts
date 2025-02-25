import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { SportCategory } from "@/types/sportCategory";

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

export const sportCategoryApi = createApi({
  reducerPath: "sportCategoryApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "https://localhost:7221/api/v1", // adjust this to your API base URL
  }),
  tagTypes: ["SportCategory"],
  endpoints: (builder) => ({
    getSportCategory: builder.query<
      PaginatedResult<SportCategory>,
      PaginationFilterDto | void
    >({
      query: (params?: PaginationFilterDto) => {
        console.log("Params received in getSportCategory:", params); // Debugging line
        debugger;
        return {
          url: "sport-categories",
          params: {
            pageNumber: params?.page ?? 1,
            pageSize: params?.pageSize ?? 10,
            searchTerm: params?.searchTerm,
            sortBy: params?.sortBy,
            sortDescending: params?.sortDescending,
          },
        };
      },
      providesTags: (result) =>
        result
          ? [
              ...result.items.map(({ id }) => ({
                type: "SportCategory" as const,
                id,
              })),
              { type: "SportCategory", id: "LIST" },
            ]
          : [{ type: "SportCategory", id: "LIST" }],
    }),
    getSportCategoryById: builder.query<SportCategory, string>({
      query: (id) => `sport-categories/${id}`,
      providesTags: (_result, _error, id) => [{ type: "SportCategory", id }],
    }),
    createSportCategory: builder.mutation<
      SportCategory,
      Partial<SportCategory>
    >({
      query: (body) => {
        console.log("Creating Sport Category with body:", body); // Debugging line
        debugger;
        return {
          url: "sport-categories",
          method: "POST",
          body,
        };
      },
      invalidatesTags: ["SportCategory"],
    }),
    updateSportCategory: builder.mutation<
      SportCategory,
      { id: string; body: Partial<SportCategory> }
    >({
      query: ({ id, body }) => ({
        url: `sport-categories/${id}`,
        method: "PUT",
        body,
      }),
      invalidatesTags: (_result, _error, { id }) => [
        { type: "SportCategory", id },
      ],
    }),
    deleteSportCategory: builder.mutation<void, number>({
      query: (id) => ({
        url: `sport-categories/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: ["SportCategory"],
    }),
  }),
});

export const {
  useGetSportCategoryQuery,
  useGetSportCategoryByIdQuery,
  useCreateSportCategoryMutation,
  useUpdateSportCategoryMutation,
  useDeleteSportCategoryMutation,
} = sportCategoryApi;
