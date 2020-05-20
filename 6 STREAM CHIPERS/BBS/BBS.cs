using System;
namespace BBS
{
    public class BBS
    {
        long P;
        long Q;

        long X0;

        long N;

        public BBS(long p, long q, long seed)
        {
            P = p;
            Q = q;
            X0 = seed;
            N = P * Q;
        }
        //Get next random number
        public long getRandNum()
        {
            long nextRandNum = Convert.ToInt64(Math.Pow(X0, 2)) % N;
            X0 = nextRandNum;
            return nextRandNum;
        }
        // Get next random bit
        public int getRandBit()
        {
            return Convert.ToInt32(getRandNum() & 1);
        }
    }
}
