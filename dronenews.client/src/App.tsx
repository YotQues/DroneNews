import { TextField } from '@mui/material';
import MuiButton from '@mui/material/Button';
import CircularProgress from '@mui/material/CircularProgress';
import style from './App.module.css';
import { ArticleCard } from './Components/ArticleCard/ArticleCard.tsx';
import { useArticles } from './server/hooks';

export function App() {
  const { isFetching, articles, fetchNextPage, setSearch, isEndOfData, page } = useArticles();

  return (
    <div className={style.appContainer}>
      <div className={style.appBar}>
        <h1>Drone.News!</h1>
      </div>
      <div className={style.pageContainer}>
        <div className={style.flexRow} style={{ justifyContent: 'center', marginBottom: '25px' }}>
          <TextField label="Search" onChange={(e) => setSearch(e.target.value)} />
        </div>
        <div className="gridList">{isFetching && page == 1 ? <></> : articles.map((a) => <ArticleCard key={a.id} article={a} />)}</div>
        <div className={style.buttonContainer}>
          {isFetching ? (
            <CircularProgress />
          ) : (
            <MuiButton disabled={isEndOfData} onClick={() => fetchNextPage()}>
              LoadMore
            </MuiButton>
          )}
        </div>
      </div>
    </div>
  );
}

/*
*

 */

/*
*
* ` <Autocomplete
          onInputChange={(_, value) => setSourceSearch(value)}
          getOptionLabel={(o) => o.url ?? 'Source'}
          isOptionEqualToValue={(op, val) => op.id === val.id}
          value={source}
          options={sources ?? []}
          onChange={(_, value) =>{
            debugger
            setSource(value as any)
          }}
          renderOption={(params, option) => <MenuItem value={option.id} >{option.url}</MenuItem>}
          renderInput={(params) => (
            <TextField
              {...params}
              InputProps={{
                endAdornment: (
                  <>
                    {sourcesLoading ? <CircularProgress color="inherit" size={20} /> : null} {params.InputProps.endAdornment}
                  </>
                )
              }}
              label="Source"
            />
          )}
        ></Autocomplete>
*
* */