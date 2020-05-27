package by.diana;

import by.diana.elgamal.ElGamal;
import by.diana.key.Key;
import by.diana.rsa.RSA;

import java.math.BigInteger;
import java.nio.charset.StandardCharsets;
import java.util.List;
import java.util.Random;
import java.util.Scanner;

public class Main {
    public static final Scanner SCANNER = new Scanner(System.in);

    public static void main(String[] args) {
	    // write your code here
        RSA rsa = new RSA();
        rsa.KeyGen(1024);

        System.out.print("Enter text: ");
        String text = SCANNER.nextLine();

        BigInteger messageToEncrypt = new BigInteger(text.getBytes(StandardCharsets.US_ASCII));
        System.out.println("To encrypt: " + messageToEncrypt);

        Key myPublicKey = rsa.getPublicKey();
        System.out.println("Public key: " + myPublicKey);
        BigInteger cipher = rsa.Encrypt(messageToEncrypt, myPublicKey);
        System.out.println("RSA: " + cipher);

        Key myPrivateKey = rsa.getSecretKey();
        System.out.println("Private key: " + myPrivateKey);
        BigInteger dmessage = rsa.Decrypt(cipher, myPrivateKey);
        System.out.println("Decrypted message: " + dmessage);

        System.out.println(myPublicKey.getKey().get(1).isProbablePrime(40));
        System.out.println(myPrivateKey.getKey().get(0).isProbablePrime(40));

        System.out.println("===== ElGamal Test =====");
        BigInteger p = ElGamal.getPrime(50, 40, new Random());
        BigInteger g = ElGamal.randNum(p, new Random());
        BigInteger pPrime = p.subtract(BigInteger.ONE).divide(ElGamal.TWO);

//        System.out.println(pPrime.isProbablePrime(40));

//        System.out.println("g^2 % p = " + g.modPow(ElGamal.TWO, p));
//        System.out.println("g^p' % p = " + g.modPow(pPrime, p));
//        System.out.println("g^(2p') % p = " + g.modPow(pPrime.multiply(ElGamal.TWO), p));

        List<List<BigInteger>> pksk = ElGamal.KeyGen(200);
        System.out.println("To encrypt: " + messageToEncrypt);
        List<BigInteger> encrypt = ElGamal.Encrypt(pksk.get(0).get(0), pksk.get(0).get(1), pksk.get(0).get(2), messageToEncrypt);
        System.out.println("ElGamal: " + encrypt);
        BigInteger decrypt = ElGamal.Decrypt(pksk.get(1).get(0), pksk.get(1).get(1), encrypt.get(0), encrypt.get(1));
        System.out.println("Decrypted message: " + decrypt);
    }
}
