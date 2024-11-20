package lab03;

import java.io.IOException; // Імпорт IOException

public class Main {
    public static void main(String[] args) {
        // Використання try-with-resources для автоматичного закриття myTuner
        try (TVTuner myTuner = new TVTuner(true, 5, 15)) {

            // Показати інформацію про телевізор з тюнером
            myTuner.showInfo();

            // Налаштування частоти тюнера
            myTuner.tuneToFrequency(101.5);
            System.out.println("Current Tuner Frequency: " + myTuner.getTunedFrequency());

            // Вимкнення телевізора
            myTuner.setPowerStatus(false);
            myTuner.showInfo();

        } catch (IOException e) {
            System.out.println("Input/output error: " + e.getMessage());
        }
    }
}
