import {useInfiniteQuery} from "@tanstack/react-query";
import {useState} from "react";
import {ArticleDto, ListResponse} from "../model";

export function useArticles() {
    const [search, setSearch] = useState("");
    const {data, isFetching, fetchNextPage} = useInfiniteQuery({
        getNextPageParam<TQueryFnData, TPageParam>(lastPage: TQueryFnData, allPages: Array<TQueryFnData>, lastPageParam: TPageParam, allPageParams: Array<TPageParam>): TPageParam | undefined | null {
            return lastPageParam + 1;
        },
        initialData: undefined,
        queryKey: ['articles', {search}],
        queryFn: ({pageParam}) => populateArticles({search: search, page: pageParam as number}),
        initialPageParam: 1,
    });

    function populateArticles({search, page = 1, author, source}: {
        search: string,
        page?: number,
        author?: number,
        source?: number
    }) {
        return fetch(`api/articles?page=${page}&search=${search}&author=${author}&source=${source}`).then(res => res.json() as Promise<ListResponse<ArticleDto>>);
    }

    return {
        articles: data,
        setSearch,
        isFetching,
        fetchNextPage,

    }
}