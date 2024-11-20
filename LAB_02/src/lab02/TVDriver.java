package lab02;


/**
 * 
 */
import java.io.IOException;
/**
 * 
 */
public class TVDriver {
	public static void main(String[] args) {
        // Створення телевізора з параметрами
        try (TV myTV = new TV(true, 1, 20)) {
            // Демонстрація функцій телевізора
            myTV.showInfo(); // Відобразити початкову інформацію
            myTV.changeChannel(5);
            myTV.changeVolume(50);
            myTV.showInfo(); // Відобразити інформацію після змін

            myTV.increaseVolume();; // Збільшити гучність на одиницю
            myTV.showInfo(); // Відобразити інформацію після збільшення

            myTV.decreaseVolume();; // Зменшити гучність на одиницю
            myTV.showInfo(); // Відобразити інформацію після зменшення

            myTV.turnOff();
            myTV.turnOn();
            myTV.changeVolume(30);
            myTV.changeChannel(10);
            myTV.showInfo(); // Відобразити інформацію після всіх змін
        } catch (IOException e) {
            System.out.println("Error: " + e.getMessage());
        }
    }
}

