import type {Source} from "./source.dto.ts";

export interface Article {
    id: number;
    title: string;
    description: string;
    content: string;
    originalUrl: string;
    imageUrl?: string;
    publishedAt: string;
    author?: string;
    source?: Source;
}

