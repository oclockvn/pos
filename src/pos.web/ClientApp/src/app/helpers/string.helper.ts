const trimLeading = (s: string, c: string): string => {
  if (s.length && s.charAt(0) === c) {
    return s.substring(1);
  }

  return s;
};

const trimEnding = (s: string, c: string): string => {
  if (s.length && s.charAt(s.length - 1) === c) {
    return s.substring(0, s.length - 1);
  }

  return s;
};

export default {
  trimLeading,
  trimEnding,
};
