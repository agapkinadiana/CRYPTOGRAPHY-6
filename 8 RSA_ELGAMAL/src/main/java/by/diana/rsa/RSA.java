package by.diana.rsa;

import by.diana.key.Cryptosystem;
import by.diana.key.Key;

import java.math.BigInteger;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Random;

/**
 * Security of RSA depends on the difficulty of factoring large integers.
 */
public final class RSA extends Cryptosystem {

    private Key secretKey;
    private Key publicKey;

    /**
     * Generate public and private key
     * return (d, p, q) & (e, N)
     *
     * @param modulus key size
     */
    @Override
    public void KeyGen(int modulus) {
        BigInteger p = new BigInteger(modulus, 40, new Random());
        BigInteger q = new BigInteger(modulus, 40, new Random());
        // phi = (p-1)*(q-1)
        BigInteger phi = p.subtract(BigInteger.ONE).multiply(q.subtract(BigInteger.ONE));

        // взаимно простое с phi
        BigInteger e = new BigInteger(phi.bitLength(), new Random());

        while (!e.gcd(phi).equals(BigInteger.ONE)) {
            e = new BigInteger(phi.bitLength(), new Random());
        }

        // d^-1 = e(mod phi)
        BigInteger d = e.modInverse(phi);
        //n = p*q
        BigInteger N = p.multiply(q);

        secretKey = new Key(new ArrayList<>(Arrays.asList(d, p, q)));
        publicKey = new Key(new ArrayList<>(Arrays.asList(e, N)));
    }

    /**
     * Encrypt the message
     *
     * @param message BigInteger message to encrypt
     * @return the encrypted message
     */
    @Override
    public BigInteger Encrypt(BigInteger message, Key publicKey) {
        //m^e mod n
        return message.modPow(publicKey.getKey().get(0), publicKey.getKey().get(1));
    }

    /**
     * Decrypt the message
     *
     * @param encrypted encrypted message to decrypt
     * @return the decrypted message
     */
    @Override
    public BigInteger Decrypt(BigInteger encrypted, Key secretKey) {
        //c^d mod p*q
        return encrypted.modPow(secretKey.getKey().get(0), secretKey.getKey().get(1).multiply(secretKey.getKey().get(2)));
    }

    public Key getPublicKey() {
        return publicKey;
    }

    public Key getSecretKey() {
        return secretKey;
    }
}
