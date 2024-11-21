package lab05;

import java.io.*;
import java.nio.file.Files;
import java.nio.file.Paths;

/**
 * Клас для обчислення виразу y = cos(x) / tan(2x), а також
 * збереження та читання результатів у текстовому та двійковому форматах.
 */
public class ExceptionCalculator {

    /**
     * Обчислює значення виразу y = cos(x) / tan(2x).
     *
     * @param x Значення змінної x.
     * @return Значення виразу y.
     * @throws ArithmeticException якщо тангенс занадто малий або рівний нулю.
     */
    public double calculate(double x) throws ArithmeticException {
        double tan2x;
        tan2x = Math.tan(2 * x);

        // Перевірка на нульове або дуже мале значення тангенса
        if (Math.abs(tan2x) < 1e-10) {
            throw new ArithmeticException("Помилка: обчислення тангенса не вдалося.");
        }

        return Math.cos(x) / tan2x;
    }

    /**
     * Записує результат у текстовий файл.
     *
     * @param result   Результат обчислення.
     * @param filename Ім'я файлу для запису результату.
     * @throws IOException якщо виникає помилка запису у файл.
     */
    public void saveResultToTextFile(double result, String filename) throws IOException {
        try (FileWriter writer = new FileWriter(filename)) {
            writer.write("Результат обчислення: " + result);
           
      
        } catch (IOException e) 
        {
            throw new IOException("Помилка запису у текстовий файл.");
        
        }
    }
 

    /**
     * Зчитує результат з текстового файлу.
     *
     * @param filename Ім'я файлу для зчитування результату.
     * @return Прочитане значення результату.
     * @throws IOException якщо виникає помилка читання файлу.
     */
    public double readResultFromTextFile(String filename) throws IOException {
        try {
            String content = new String(Files.readAllBytes(Paths.get(filename)));
            return Double.parseDouble(content.split(": ")[1]);
           
        } catch (IOException e) {
            throw new IOException("Помилка читання з текстового файлу.");
        }
    }

    /**
     * Записує результат у двійковий файл.
     *
     * @param result   Результат обчислення.
     * @param filename Ім'я файлу для запису результату.
     * @throws IOException якщо виникає помилка запису у файл.
     */
    public void saveResultToBinaryFile(double result, String filename) throws IOException {
        try (DataOutputStream dos = new DataOutputStream(new FileOutputStream(filename))) {
            dos.writeDouble(result);
        } catch (IOException e) {
            throw new IOException("Помилка запису у двійковий файл.");
        }
    }

    /**
     * Зчитує результат з двійкового файлу.
     *
     * @param filename Ім'я файлу для зчитування результату.
     * @return Прочитане значення результату.
     * @throws IOException якщо виникає помилка читання файлу.
     */
    public double readResultFromBinaryFile(String filename) throws IOException {
        try (DataInputStream dis = new DataInputStream(new FileInputStream(filename))) {
            return dis.readDouble();
        } catch (IOException e) {
            throw new IOException("Помилка читання з двійкового файлу.");
        }
    }
}
