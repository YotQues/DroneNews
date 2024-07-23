export interface ListResponse<TData> {
    totalItems: number;
    items: TData[];
    isLastPage: boolean;
}

