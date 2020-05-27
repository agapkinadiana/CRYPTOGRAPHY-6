package by.diana.rsa;

import java.io.InputStream;
import java.security.KeyFactory;
import java.security.PrivateKey;
import java.security.PublicKey;
import java.security.spec.KeySpec;
import java.security.spec.PKCS8EncodedKeySpec;
import java.security.spec.X509EncodedKeySpec;
import java.util.Base64;
import javax.crypto.Cipher;

public class Rsa4096 {

    private KeyFactory keyFactory;
    private PrivateKey privateKey;
    private PublicKey publicKey;

    public Rsa4096(String privateKeyClassPathResource, String publicKeyClassPathResource) throws Exception {
        setKeyFactory();
        setPrivateKey(privateKeyClassPathResource);
        setPublicKey(publicKeyClassPathResource);
    }

    protected void setKeyFactory() throws Exception {
        this.keyFactory = KeyFactory.getInstance("RSA");
    }

    protected void setPrivateKey(String classpathResource)
    throws Exception {
        // получаем доступ к файлу с закрытым ключом
        InputStream is = this
            .getClass()
            .getClassLoader()
            .getResourceAsStream(classpathResource);

        // и считываем байты файла в новую строку
        String stringBefore = new String(is.readAllBytes());
        is.close();

        // форматируем строку, т.к нельзя использовать напрямую - будет ошибка
        String stringAfter = stringBefore
            .replaceAll("\\n", "")
            .replaceAll("-----BEGIN PRIVATE KEY-----", "")
            .replaceAll("-----END PRIVATE KEY-----", "")
            .trim();

        byte[] decoded = Base64
            .getDecoder()
            .decode(stringAfter);

        // PKCS8EncodedKeySpec ожидает, что закрытый ключ будет одной строкой текста со всеми удаленными комментариями
        KeySpec keySpec = new PKCS8EncodedKeySpec(decoded);

        // PKCS8EncodedKeySpec и KeyFactory используются для создания PrivateKey
        privateKey = keyFactory.generatePrivate(keySpec);
    }


    protected void setPublicKey(String classpathResource)
    throws Exception {

        InputStream is = this
            .getClass()
            .getClassLoader()
            .getResourceAsStream(classpathResource);

        String stringBefore = new String(is.readAllBytes());
        is.close();
        
        String stringAfter = stringBefore
            .replaceAll("\\n", "")
            .replaceAll("-----BEGIN PUBLIC KEY-----", "")
            .replaceAll("-----END PUBLIC KEY-----", "")
            .trim()
        ;

        byte[] decoded = Base64
            .getDecoder()
            .decode(stringAfter);

        // Класс X509EncodedKeySpec ожидает, что открытый ключ будет одной строкой текста со всеми удаленными комментариями
        KeySpec keySpec = new X509EncodedKeySpec(decoded);

        // X509EncodedKeySpec и KeyFactory используются для создания PublicKey.
        publicKey = keyFactory.generatePublic(keySpec);
    }


    public String encryptToBase64(String plainText) {
        String encoded = null;
        try {
            Cipher cipher = Cipher.getInstance("RSA");
            cipher.init(Cipher.ENCRYPT_MODE, publicKey);
            byte[] encrypted = cipher.doFinal(plainText.getBytes());
            // будет возвращена строка в кодировке Base64
            encoded = Base64.getEncoder().encodeToString(encrypted);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return encoded;
    }

    public String decryptFromBase64(String base64EncodedEncryptedBytes) {
        String plainText = null;
        try {
            final Cipher cipher = Cipher.getInstance("RSA");
            cipher.init(Cipher.DECRYPT_MODE, privateKey);
            // расшифровать строку до ее первоначального значения из Base64
            byte[] decoded = Base64
                .getDecoder()
                .decode(base64EncodedEncryptedBytes);
            byte[] decrypted = cipher.doFinal(decoded);
            plainText = new String(decrypted);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return plainText;
    }
}