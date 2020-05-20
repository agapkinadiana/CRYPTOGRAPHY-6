package by.belstu.main;

import by.belstu.logic.KnapsackCipher;
import by.belstu.view.Printer;

import java.util.Scanner;
import java.util.Arrays;

public class Main {

    public static final Scanner SCANNER = new Scanner(System.in);
    public static final Printer PRINTER = new Printer();

    public static void main(String[] args) {
        PRINTER.print("Enter text: ");
        String text = SCANNER.nextLine();

        byte[] bytes = text.getBytes();
        PRINTER.println("Text bytes: " + Arrays.toString(bytes));

        KnapsackCipher knapsackCipher = new KnapsackCipher(8);

        int[] encryptedBytes = knapsackCipher.encrypt(bytes);
        PRINTER.println("Encrypted bytes: " + Arrays.toString(encryptedBytes));

        byte[] decryptedBytes = knapsackCipher.decrypt(encryptedBytes);
        PRINTER.println("Decrypted bytes: " + Arrays.toString(decryptedBytes));

        String decryptedString = new String(decryptedBytes);
        PRINTER.println("Decrypted text: " + decryptedString);
    }
}
