export const calculateRateMultiplier = (mid: number): number => {
  let multiplier = 1;

  while (mid * multiplier < 1 && multiplier < 100) {
    multiplier *= 10;
  }

  return multiplier;
};


