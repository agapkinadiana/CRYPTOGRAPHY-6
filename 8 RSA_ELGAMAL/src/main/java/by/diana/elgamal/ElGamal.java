package by.diana.elgamal;

import java.math.BigInteger;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Random;

/**
 * Security of the ElGamal algorithm depends on the difficulty of computing discrete logs
 * in a large prime modulus
 *
 * - Theorem 1 : a in [Z/Z[p]] then a^(p-1) [p] = 1
 * - Theorem 2 : the order of an element split the order group
 */
public final class ElGamal {

    public static BigInteger TWO = new BigInteger("2");

    /**
     * Generate the public key and the secret key for the ElGamal encryption.
     *
     * @param n key size
     */
    public static List<List<BigInteger>> KeyGen(int n) {
        // (a) p - простое
        BigInteger p = getPrime(n, 40, new Random());
        // (b) g - g < p и является первообразным корнем числа р
        BigInteger g = randNum(p, new Random());
        // phi / 2 (p-1 / 2)
        BigInteger pPrime = p.subtract(BigInteger.ONE).divide(ElGamal.TWO);

        // g ^ (p-1 / 2) mod p == 1
        while (!g.modPow(pPrime, p).equals(BigInteger.ONE)) {
            if (g.modPow(pPrime.multiply(ElGamal.TWO), p).equals(BigInteger.ONE))
                g = g.modPow(TWO, p);
            else
                g = randNum(p, new Random());
        }

        // (c) x - x < p - рандомное
        BigInteger x = randNum(pPrime.subtract(BigInteger.ONE), new Random());
        // h = g^x mod p
        BigInteger h = g.modPow(x, p);
        // secret key is (p, x) and public key is (p, g, h)
        List<BigInteger> sk = new ArrayList<>(Arrays.asList(p, x));
        List<BigInteger> pk = new ArrayList<>(Arrays.asList(p, g, h));
        // [0] = pk, [1] = sk
        return new ArrayList<>(Arrays.asList(pk, sk));
    }

    /**
     * Encrypt ElGamal
     *
     * @param (p,g,h) public key
     * @param message message
     */
    public static List<BigInteger> Encrypt(BigInteger p, BigInteger g, BigInteger h, BigInteger message) {
        BigInteger pPrime = p.subtract(BigInteger.ONE).divide(ElGamal.TWO);
        BigInteger r = randNum(pPrime, new Random());
        // encrypt couple (g^r (mod p), m * h^r (mod p))
        return new ArrayList<>(Arrays.asList(g.modPow(r, p), message.multiply(h.modPow(r, p))));
    }

    /**
     * Decrypt ElGamal
     *
     * @param (p,x) secret key
     * @param (gr,mhr) (g^r, m * h^r)
     * @return the decrypted message
     */
    public static BigInteger Decrypt(BigInteger p, BigInteger x, BigInteger gr, BigInteger mhr) {
        // (a)^x)^-1) = gr^х mod p
        BigInteger hr = gr.modPow(x, p);
        // mhr * hr^-1 mod p
        return mhr.multiply(hr.modInverse(p)).mod(p);
    }

    /**
     * Return a prime p = 2 * p' + 1
     *
     * @param nb_bits   is the prime representation
     * @param certainty probability to find a prime integer
     * @param prg       random
     * @return p
     */
    public static BigInteger getPrime(int nb_bits, int certainty, Random prg) {
        BigInteger pPrime = new BigInteger(nb_bits, certainty, prg);
        // p = 2 * pPrime + 1
        BigInteger p = pPrime.multiply(TWO).add(BigInteger.ONE);

        while (!p.isProbablePrime(certainty)) {
            pPrime = new BigInteger(nb_bits, certainty, prg);
            p = pPrime.multiply(TWO).add(BigInteger.ONE);
        }
        return p;
    }

    /**
     * Return a random integer in [0, N - 1]
     *
     * @param N
     * @param prg
     * @return
     */
    public static BigInteger randNum(BigInteger N, Random prg) {
        return new BigInteger(N.bitLength() + 100, prg).mod(N);
    }
}

