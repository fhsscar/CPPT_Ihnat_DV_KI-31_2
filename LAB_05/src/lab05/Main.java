package lab05;



import java.io.IOException;
import java.util.InputMismatchException;
import java.util.Scanner;

/**
 * Клас для тестування ExceptionCalculator.
 */
public class Main {
    public static void main(String[] args) {
        ExceptionCalculator calculator = new ExceptionCalculator();
        Scanner scanner = new Scanner(System.in); // Створюємо об'єкт Scanner для введення даних

        double x = 0;

        try {
            // Введення значення x
            System.out.print("Введіть значення x: ");
            x = scanner.nextDouble(); // Зчитуємо значення x з клавіатури

            // Обчислення виразу
            double result = calculator.calculate(x);
            System.out.println("Обчислений результат: " + result);

            String textFilename = "result.txt";
            String binaryFilename = "result.bin";

            // Запис та читання з текстового файлу
            calculator.saveResultToTextFile(result, textFilename);
            double readTextResult = calculator.readResultFromTextFile(textFilename);
            System.out.println("Результат з текстового файлу: " + readTextResult);

            // Запис та читання з двійкового файлу
            calculator.saveResultToBinaryFile(result, binaryFilename);
            double readBinaryResult = calculator.readResultFromBinaryFile(binaryFilename);
            System.out.println("Результат з двійкового файлу: " + readBinaryResult);

        } catch (InputMismatchException e) {
            System.err.println("Помилка введення: ви повинні ввести числове значення.");
        } catch (ArithmeticException e) {
            System.err.println("Помилка у математичних обчисленнях: " + e.getMessage());
        } catch (IOException e) {
            System.err.println("Помилка з файлом: " + e.getMessage());
        } finally {
            scanner.close(); // Закриваємо Scanner після використання
           
        }
    }
}
