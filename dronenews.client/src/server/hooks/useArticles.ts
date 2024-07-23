import { useCallback, useEffect, useMemo, useState } from 'react';
import { debounce } from '../../utils/debounce.util';
import { getSearchParams } from '../../utils/getSearchParams.ts';
import type { Article, ListResponse } from '../model';

export function useArticles() {
  const [search, setSearch] = useState('');
  const [source, setSource] = useState<number | null>(null);
  const [author, setAuthor] = useState<number | null>(null);
  const [isEndOfData, setIsEndOfData] = useState(false);
  const [isFetching, setIsFetching] = useState(false);
  const [totalItems, setTotalItems] = useState(0);
  const [page, setPage] = useState(1);
  const [data, setData] = useState<Article[]>([]);
  const [fetchTrigger, triggerFetch] = useState(Symbol());

  const fetchArticles = useCallback(() => {
    setIsFetching(true);
    return fetch(
      `api/articles?${getSearchParams({
        search,
        page,
        author,
        source
      }).toString()}`
    )
      .then((res) => res.json() as Promise<ListResponse<Article>>)
      .then((resp) => {
        setIsEndOfData(resp.isLastPage);
        setTotalItems(resp.totalItems);
        setData((prev) => (page == 1 ? resp.items : [...prev, ...resp.items]));
        setIsFetching(false);
      });
  }, [search, page, author, source]);

  useEffect(() => {
    setPage(1);
    triggerFetch(Symbol());
  }, [search, source, author]);

  useEffect(() => {
    const timeout = window.setTimeout(() => {
      fetchArticles();
    }, 300);
    return () => {
      timeout && window.clearTimeout(timeout);
    };
  }, [fetchTrigger, fetchArticles]);

  return useMemo(() => ({
    articles: data,
    setSearch,
    setAuthor,
    setSource,
    isFetching,
    fetchNextPage: () => !isEndOfData && setPage((curr) => curr + 1),
    isEndOfData,
    totalItems,
    source
  }), [data, isEndOfData, isFetching, source, totalItems]);
}
