import { renderHook, act, waitFor } from '@testing-library/react';
import fetchMock from 'jest-fetch-mock';
import { useArticles } from '../server/hooks';
import { ListResponse } from '../server/model';

fetchMock.enableMocks();

const mockResponse: ListResponse<any> = {
  items: [{ id: 1, title: 'Test Article' }],
  isLastPage: false,
  totalItems: 1,
};

describe('useArticles', () => {
  beforeEach(() => {
    fetchMock.resetMocks();
    fetchMock.mockResponse(JSON.stringify(mockResponse));
  });

  it('should fetch articles on initial render', async () => {
    const { result } = renderHook(() => useArticles());

    await waitFor(() => expect(result.current.articles.length).toBeTruthy());
    expect(result.current.articles.length).toEqual(mockResponse.items.length);
    expect(result.current.totalItems).toBe(mockResponse.totalItems);
    expect(result.current.isEndOfData).toBe(mockResponse.isLastPage);
  });

  it('should update search and trigger fetch', async () => {
    const { result } = renderHook(() => useArticles());

    act(() => {
      result.current.setSearch('new search');
    });

    await waitFor(() => expect(result.current.articles.length).toBeTruthy());

    expect(fetchMock).toHaveBeenCalledWith(expect.stringContaining('search=new+search'));
    expect(result.current.articles).toEqual(mockResponse.items);
  });

  it('should update source and trigger fetch', async () => {
    const { result } = renderHook(() => useArticles());

    act(() => {
      result.current.setSource(1);
    });

    await waitFor(() => expect(result.current.articles.length).toBeTruthy());

    expect(fetchMock).toHaveBeenCalledWith(expect.stringContaining('source=1'));
    expect(result.current.articles).toEqual(mockResponse.items);
  });

  it('should update author and trigger fetch', async () => {
    const { result } = renderHook(() => useArticles());

    act(() => {
      result.current.setAuthor(1);
    });

    await waitFor(() => expect(result.current.articles.length).toBeTruthy());

    expect(fetchMock).toHaveBeenCalledWith(expect.stringContaining('author=1'));
    expect(result.current.articles).toEqual(mockResponse.items);
  });

  it('should fetch next page of articles', async () => {
    const { result } = renderHook(() => useArticles());
    await waitFor(() => expect(result.current.articles.length).toBeTruthy());

    const firstLength = result.current.articles.length
    act(() => {
      result.current.fetchNextPage();
    });

    await waitFor(() => expect(!result.current.isFetching && result.current.articles.length > firstLength).toBe(true));

    expect(fetchMock).toHaveBeenCalledTimes(2);
    expect(fetchMock).toHaveBeenCalledWith(expect.stringContaining('page=2'));
    expect(result.current.articles).toEqual([...mockResponse.items, ...mockResponse.items]);
  });

  it('should not fetch next page if isEndOfData is true', async () => {
    mockResponse.isLastPage = true;

    const { result } = renderHook(() => useArticles());

    await waitFor(() => expect(result.current.articles.length).toBeTruthy());

    act(() => {
      result.current.fetchNextPage();
    });

    await waitFor(() => expect(result.current.articles.length).toBeTruthy());

    expect(fetchMock).toHaveBeenCalledTimes(1);
  });
});