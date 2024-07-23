import { Dialog, DialogContent, DialogTitle, Typography } from '@mui/material';
import { useState } from 'react';
import imgPlaceholder from '../../assets/image-placeholder.png';
import { Article } from '../../server/model';
import styles from './ArticleCard.module.css';

class IArticleCardProps {
  article: Article;
}

export function ArticleCard({ article }: IArticleCardProps) {
  const [imageError, setImageError] = useState(false);
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  return (
    <div onClick={() => setIsDialogOpen(true)} className={styles.card + ' gridItem'}>
      <div className={styles.image}>
        <img
          onError={() => setImageError(true)}
          src={article.imageUrl == null || imageError ? (imgPlaceholder as string) : article.imageUrl}
          alt={'article-image ' + article.title}
        />
      </div>
      <div className={styles.text}>
        <div>
          <Typography variant="subtitle1" className={styles.title}>
            {article.title}
          </Typography>
          {article.publishedAt && <Typography variant="subtitle2">{article.publishedAt.split('T')[0]}</Typography>}
        </div>
      </div>
      <Dialog open={isDialogOpen} onClose={() => setIsDialogOpen(false)} >
        <DialogTitle>{article.title}</DialogTitle>

        <DialogContent>
          <Typography variant="h6">{article.description}</Typography>
          <Typography variant="p">{article.content}</Typography>
        </DialogContent>
      </Dialog>
    </div>
  );
}
