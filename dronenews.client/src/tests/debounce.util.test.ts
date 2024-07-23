import { debounce } from '../utils/debounce.util.ts';

jest.useFakeTimers();

describe('debounce', () => {
  let callback: jest.Mock;
  let debouncedFunction: (...args: unknown[]) => void;
  let wait: number;

  beforeEach(() => {
    callback = jest.fn();
    wait = 100;
    debouncedFunction = debounce(callback, wait);
  });

  it('should call the callback after the specified wait time', () => {
    debouncedFunction();
    expect(callback).not.toHaveBeenCalled();
    jest.advanceTimersByTime(wait);
    expect(callback).toHaveBeenCalled();
  });

  it('should reset the wait time if called again before the wait time expires', () => {
    debouncedFunction();
    jest.advanceTimersByTime(wait / 2);
    debouncedFunction();
    jest.advanceTimersByTime(wait / 2);
    expect(callback).not.toHaveBeenCalled();
    jest.advanceTimersByTime(wait / 2);
    expect(callback).toHaveBeenCalledTimes(1);
  });

  it('should call the callback with the correct arguments', () => {
    debouncedFunction('arg1', 'arg2');
    jest.advanceTimersByTime(wait);
    expect(callback).toHaveBeenCalledWith('arg1', 'arg2');
  });

  it('should not call the callback if the wait time has not passed', () => {
    debouncedFunction();
    jest.advanceTimersByTime(wait - 10);
    expect(callback).not.toHaveBeenCalled();
  });
});
