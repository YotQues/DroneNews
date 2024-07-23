export function getSearchParams<TObj extends object>(input: TObj): URLSearchParams {
  const output: URLSearchParams = new URLSearchParams();
  for (const [key, value] of Object.entries(input)) {
    if (value == null) {
      continue;
    }
    if (value instanceof Date) {
      output.set(key, value.toISOString());
      continue;
    }
    if (Array.isArray(value)) {
      output.set(key, value.join(','));
      continue;
    }

    if (typeof value === 'object') {
      output.set(key, JSON.stringify(value));
      continue;
    }

    if (typeof value === 'number') {
      output.set(key, value.toString());
      continue;
    }
    if (typeof value !== 'string') {
      continue;
    }

    value.trim() && output.set(key, value);
  }
  return output;
}