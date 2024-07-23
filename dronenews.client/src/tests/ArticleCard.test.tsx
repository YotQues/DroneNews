import React from 'react';
import renderer from 'react-test-renderer';
import { render, fireEvent, act } from '@testing-library/react';
import { Article } from '../server/model';
import { ArticleCard } from '../Components/ArticleCard/ArticleCard.tsx';

const mockArticle: Article = {
  id: 1,
  title: 'Test Article',
  description: 'This is a test description.',
  content: 'This is the test content.',
  imageUrl: 'test-image-url',
  publishedAt: '2024-07-24T12:34:56Z',
} as any;

describe('ArticleCard', () => {
  it('renders correctly', () => {
    const tree = renderer.create(<ArticleCard article={mockArticle} />).toJSON();
    expect(tree).toMatchSnapshot();
  });

  it('should display the article title and published date', () => {
    const { getByText } = render(<ArticleCard article={mockArticle} />);
    expect(getByText('Test Article')).toBeTruthy();
    expect(getByText('2024-07-24')).toBeTruthy();
  });

  it('should display the article image', () => {
    const { getByAltText } = render(<ArticleCard article={mockArticle} />);
    const img = getByAltText('article-image Test Article');
    expect(img).toBeTruthy();
    expect(img.getAttribute('src')).toBe('test-image-url');
  });

  it('should open the dialog with article details when clicked', () => {
    const { getByText, getByRole } = render(<ArticleCard article={mockArticle} />);
    const card = getByText('Test Article');
    fireEvent.click(card);
    expect(getByRole('dialog')).toBeTruthy();
    expect(getByText('This is a test description.')).toBeTruthy();
    expect(getByText('This is the test content.')).toBeTruthy();
  });

  it('should close the dialog when the close button is clicked', async() => {
    const { getByText, getByRole, queryByRole } = render(<ArticleCard article={mockArticle} />);
    const card = getByText('Test Article');
    act(() => {
      fireEvent.click(card);
    })
    await new Promise((res) => {
      setTimeout(() => res(), 800);
    })
    const dialog = getByRole('dialog');
    expect(dialog).toBeTruthy();
    act(() => {
      fireEvent.keyDown(dialog, { key: 'Escape', code: 'Escape' });
    })

    await new Promise((res) => {
      setTimeout(() => res(), 800);
    })

    expect(queryByRole('dialog')).toBeNull();
  });
});
