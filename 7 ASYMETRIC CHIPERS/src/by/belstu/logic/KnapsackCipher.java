package by.belstu.logic;

import java.math.BigInteger;
import java.util.Arrays;
import java.util.Random;

public class KnapsackCipher {

    public static final Random RND = new Random();
    public static final char ONE = '1';
    public static final char ZERO = '0';
    public static final int RADIX = 2;

    private int d[];
    private int e[];
    private int z;
    private int a;
    private int n;
    private int aInv;

    public KnapsackCipher(int z) {
        this.z = z;
        d = new int[z];
        e = new int[z];
        init();
        printInfo();
        Arrays.sort(d);
    }

    private void init() {
        // сверхвозрастающая последовательность d
        int sum = 0;
        int di;
        for (int i = 0; i < z; i++) {
            do {
                di = RND.nextInt(z) + sum;
            } while (di < sum);
            d[i] = ++di;
            sum += d[i];
        }

        a = RND.nextInt(sum);
        do {
            //значение n должно быть больше суммы всех чисел последовательности
            n = RND.nextInt(sum) + sum;
        } while (gcd(n, a) != 1); //должны быть взаимно мпростыми, т.е НОД (а, n) = 1

        aInv = modInverse(a, n);

        for (int i = 0; i < z; i++) {
            e[i] = (d[i] * a) % n; //получаем открытый ключ
        }
    }


    public int[] encrypt(byte[] data) {
        int[] result = new int[data.length];
        String binaryCode;
        int sum;
        // разбиваем сообщение на блоки, по размерам равныечислу элементов последовательности ключей (8)
        for (int i = 0; i < result.length; i++) {
            binaryCode = String.format("%8s",
                    Integer.toBinaryString(data[i])).replace(' ', '0');
            sum = 0;
            // 1 - элемент присутствует, 0 - нет
            // складывает те элементы из открытого ключа, где 1
            for (int j = 0; j < z; j++) {
                if (binaryCode.charAt(j) == ONE) {
                    sum += e[j];
                }
            }
            result[i] = sum;
        }
        return result;
    }

    public byte[] decrypt(int[] data) {
        StringBuilder binaryCode;
        int sum;
        byte[] result = new byte[data.length];
        for (int i = 0; i < result.length; i++) {
            sum = (data[i] * aInv) % n;  // ищем вес первого элемента
            binaryCode = new StringBuilder();
            for (int j = z - 1; j >= 0; j--) {  // проходим сверхвозр посл-ть d в обратном порядке
                // ищем какие элементы составляют вес, ставим 1 если входит
                if (d[j] <= sum) {
                    sum -= d[j];
                    binaryCode.append(ONE);
                } else {
                    binaryCode.append(ZERO);
                }
            }
            binaryCode.reverse().replace(0, 1, String.valueOf(ZERO)); // т.к с конца проходили, надо перевернуть
            result[i] = (byte) Integer.parseInt(binaryCode.toString(), RADIX);  // из 2 в 10
        }
        return result;
    }

    // вычисление НОД
    private int gcd(int a, int b) {
        return BigInteger.valueOf(a).gcd(BigInteger.valueOf(b)).intValue();
    }

    // вычисление обратного по модулю
    private int modInverse(int a, int m) {
        return BigInteger.valueOf(a)
                .modInverse(BigInteger.valueOf(m)).intValue();
    }

    void printInfo(){
        System.out.println("d: " + Arrays.toString(d));
        System.out.println("e: " + Arrays.toString(e));
        System.out.println("a = " + a);
        System.out.println("n = " + n);
        System.out.println("a^-1 = " + aInv);
    }
}
