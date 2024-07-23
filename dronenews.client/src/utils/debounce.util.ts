export function debounce<TFunc extends (...args: unknown[]) => void>(callback: TFunc, wait = 0) {
  let timeout: number | undefined = undefined;
  return ((...args: unknown[]) => {
    timeout && window.clearTimeout(timeout);
    timeout = window.setTimeout(() => {
      callback(...args);
      timeout && window.clearTimeout(timeout);
      timeout = undefined;
    }, wait);
  }) as TFunc;
}
