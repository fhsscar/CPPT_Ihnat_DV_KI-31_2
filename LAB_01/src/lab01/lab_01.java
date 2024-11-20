package lab01;

import java.io.*;
import java.util.*;

public class lab_01 {

    public static void main(String[] args) throws FileNotFoundException {

        int nRows;
        char[][] arr;
        String filler;
        Scanner in = new Scanner(System.in);
        File dataFile = new File("MyFile.txt");
        PrintWriter fout = new PrintWriter(dataFile);

        // Введення розміру матриці та символу-заповнювача
        System.out.print("Введіть розмір квадратної матриці (непарне число): ");
        nRows = in.nextInt();
        in.nextLine();  // Очистити новий рядок після вводу числа
        arr = new char[nRows / 2 + 1][nRows];  // Створюємо масив для непарних рядків

        System.out.print("\nВведіть символ-заповнювач: ");
        filler = in.nextLine();

        // Перевірка довжини символу-заповнювача
        if (filler.length() != 1) {
            System.out.println("Помилка: введіть один символ!");
            return;
        }

        char fillChar = filler.charAt(0);  // Символ для заповнення

        // Основний цикл для побудови та виводу шаблону
        for (int i = 0; i < nRows; i++) {
            // Якщо рядок парний, виводимо пробіли
            if (i % 2 == 0) {
                for (int j = 0; j < nRows; j++) {
                    System.out.print(" ");  // Друк пробілів
                    fout.print(" ");  // Запис у файл
                }
            } else {
                // Якщо рядок непарний, виводимо символи
                for (int j = 0; j < nRows; j++) {
                    arr[i / 2][j] = fillChar;  // Заповнюємо масив символами
                    System.out.print(arr[i / 2][j]);  // Виводимо символи
                    fout.print(arr[i / 2][j]);  // Запис у файл
                }
            }
            System.out.println();  // Перехід на новий рядок
            fout.println();  // Запис нового рядка у файл
        }

        // Завершення запису у файл
        fout.flush();
        fout.close();
        in.close();
    }
}