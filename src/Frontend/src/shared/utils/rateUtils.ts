export const calculateRateMultiplier = (mid: number): number => {
  if(mid > 1 || Math.floor(mid * 1000000) % 100 === 0 ){
    return 1;
  }

  return 100;
};


