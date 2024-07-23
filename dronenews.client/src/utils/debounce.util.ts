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

export function debounceAsync<TFunc extends (...args: any[]) => Promise<any>>(callback: TFunc, wait = 0) {
  let timeout: number | undefined = undefined;
  return ((...args: unknown[]) =>
    new Promise((resolve, reject) => {
      timeout && window.clearTimeout(timeout);
      timeout = window.setTimeout(async () => {
        timeout && window.clearTimeout(timeout);
        try {
          const returnVal = await callback(...args);
          timeout = undefined;
          resolve(returnVal);
        } catch (e) {
          reject(e);
        }
      }, wait);
    })) as TFunc;
}
