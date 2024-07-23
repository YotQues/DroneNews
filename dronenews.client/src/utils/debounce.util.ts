export function debounce<TFunc extends (...args: unknown[]) => void>(callback: TFunc, wait = 0) {
  let timeout: NodeJS.Timeout | undefined = undefined;
  return ((...args: unknown[]) => {
    timeout && clearTimeout(timeout);
    timeout = setTimeout(() => {
      callback(...args);
      timeout && clearTimeout(timeout);
      timeout = undefined;
    }, wait);
  }) as TFunc;
}
