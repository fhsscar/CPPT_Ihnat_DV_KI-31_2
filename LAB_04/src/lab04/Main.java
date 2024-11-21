package lab04;

import java.io.IOException;
import java.util.Scanner;

/**
 * Головний клас для демонстрації роботи ExpressionCalculator.
 */
public class Main {

    public static void main(String[] args) {
        ExceptionCalculator calculator = new ExceptionCalculator();
        Scanner scanner = new Scanner(System.in); // Створення об'єкта Scanner для введення
        String filename = "result.txt"; // Ім'я файлу для запису результату

        try {
            System.out.print("Введіть значення x: ");
            double x = scanner.nextDouble(); // Читання значення x

            // Обчислення виразу
            double result = calculator.calculate(x);
            System.out.println("Результат: " + result);

            // Запис результату у файл
            calculator.saveResultToFile(result, filename);
            System.out.println("Результат записано у файл: " + filename);

        } catch (ArithmeticException e) {
            System.err.println(e.getMessage());
        } catch (IOException e) {
            System.err.println(e.getMessage());
        } catch (Exception e) {
            System.err.println("Помилка введення: " + e.getMessage());
        } finally {
            scanner.close(); // Закриття сканера
        }
    }
}
