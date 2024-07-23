import type {SourceDto} from "./source.dto.ts";

export interface ArticleDto {
    id: number;
    title: string;
    description: string;
    content: string;
    originalUrl: string;
    imageUrl?: string;
    publishedAt: Date;
    author?: string;
    source?: SourceDto;
}

