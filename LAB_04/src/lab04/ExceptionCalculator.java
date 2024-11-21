package lab04;


import java.io.FileWriter;
import java.io.IOException;

/**
 * Клас для обчислення виразу y = cos(x) / tan(2x).
 * Забезпечує обробку виключень та збереження результатів у файл.
 */
public class ExceptionCalculator  {

    /**
     * Обчислює значення виразу y = cos(x) / tan(2x).
     *
     * @param x Значення змінної x.
     * @return Значення виразу y.
     * @throws ArithmeticException якщо тангенс занадто малий або рівний нулю.
     */
    public double calculate(double x) throws ArithmeticException  {
        double tan2x ;
        
        try {
            tan2x = Math.tan(2 * x);
            
            // Перевірка на нульове або дуже мале значення тангенса
            if (Math.abs(tan2x) < 1e-10) {
                throw new ArithmeticException();
            }
        } catch (ArithmeticException e) {
        	
            throw new ArithmeticException("Помилка: обчислення тангенса не вдалося.");
        }
        
        double y = Math.cos(x) / tan2x; // Обчислюємо y
        return y;
    }

    /**
     * Записує результат обчислення у файл.
     *
     * @param result Результат обчислення.
     * @param filename Ім'я файлу для запису результату.
     * @throws IOException якщо виникає помилка запису у файл.
     */
    public void saveResultToFile(double result, String filename) throws IOException {
        try (FileWriter writer = new FileWriter(filename)) {
            writer.write("Результат обчислення: " + result);
        } catch (IOException e) {
            throw new IOException("Помилка запису у файл.");
        }
    }
}
