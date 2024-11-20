package lab03;

import java.io.FileWriter;
import java.io.IOException;

/**
*Клас TV представляє телевізор з можливістю керування каналами, гучністю та станом живлення.
*/

public abstract class TV extends Object implements AutoCloseable {
	   protected VolumeControl volumeControl;
	    protected ChannelControl channelControl;
	    protected PowerStatus powerStatus;
   private FileWriter logger; // Поле для логування

   /**
    * Конструктор без параметрів. Ініціалізує телевізор зі значеннями за замовчуванням.
    * @throws IOException якщо виникає помилка при відкритті лог-файлу.
    */
   public TV() throws IOException {
       this.volumeControl = new VolumeControl();
       this.channelControl = new ChannelControl();
       this.powerStatus = new PowerStatus();
       // Відкриття лог-файлу для запису
       this.logger = new FileWriter("tv_log.txt", true);
       logAction("TV initialized with default settings.");
   }

   /**
    * Конструктор з параметрами для ініціалізації телевізора з певним станом.
    * @param inisON початковий стан телевізора (ввімкнений/вимкнений)
    * @param inChannel початковий канал
    * @param inVolume початкова гучність
    * @throws IOException якщо виникає помилка при роботі з лог-файлом.
    */
   public TV(boolean inisON, int inChannel, int inVolume) throws IOException {
       this(); // Виклик конструктора без параметрів для ініціалізації logger
       this.powerStatus = new PowerStatus(inisON);
       if (!inisON) {
           System.out.println("The TV is off! Turning on the TV...");
           this.powerStatus.turnON();
           logAction("TV turned ON during initialization.");
       }
       this.channelControl = new ChannelControl(inChannel);
       this.volumeControl = new VolumeControl(inVolume);
       logAction("TV initialized with channel: " + inChannel + ", volume: " + inVolume);
   }

   /**
    * Метод для логування дій телевізора.
    * @param action дія, яка буде записана у файл
    * @throws IOException якщо виникає помилка при записі у лог-файл.
    */
   private void logAction(String action) throws IOException {
       logger.write(action + "\n");
       logger.flush(); // Для негайного запису в файл
   }

   /**
    * Увімкнути телевізор.
    * @throws IOException якщо виникає помилка при роботі з лог-файлом.
    */
   public void turnOn() throws IOException {
       if (!powerStatus.isON()) {
           powerStatus.turnON();
           logAction("TV turned ON.");
       } else {
           System.out.println("TV is already ON!");
       }
   }

   /**
    * Вимкнути телевізор.
    * @throws IOException якщо виникає помилка при роботі з лог-файлом.
    */
   public void turnOff() throws IOException {
       if (powerStatus.isON()) {
           powerStatus.turnOFF();
           logAction("TV turned OFF.");
       } else {
           System.out.println("TV is already OFF!");
       }
   }

   /**
    * Змінити канал телевізора.
    * @param newChannel новий номер каналу
    * @throws IOException якщо виникає помилка при роботі з лог-файлом.
    */
   public void changeChannel(int newChannel) throws IOException {
       if (powerStatus.isON()) {
           channelControl.setChannel(newChannel);
           logAction("Channel changed to: " + newChannel);
       } else {
           System.out.println("TV is turned OFF!");
       }
   }

   /**
    * Змінити гучність телевізора.
    * @param newVolume новий рівень гучності
    * @throws IOException якщо виникає помилка при роботі з лог-файлом.
    */
   public void changeVolume(int newVolume) throws IOException {
       if (powerStatus.isON()) {
           volumeControl.setVolume(newVolume);
           logAction("Volume changed to: " + newVolume);
       } else {
           System.out.println("TV is turned OFF!");
       }
   }
   /**
    * Метод для відображення інформації про телевізор.
    */
   public abstract void showInfo();
   /**
    * Збільшити гучність телевізора на одиницю.
    * @throws IOException якщо виникає помилка при роботі з лог-файлом.
    */
   public void increaseVolume() throws IOException {
       if (powerStatus.isON()) {
           volumeControl.increaseVolume();
           logAction("Volume increased to: " + volumeControl.getVolume());
       } else {
           System.out.println("TV is turned OFF!");
       }
   }

   /**
    * Зменшити гучність телевізора на одиницю.
    * @throws IOException якщо виникає помилка при роботі з лог-файлом.
    */
   public void decreaseVolume() throws IOException {
       if (powerStatus.isON()) {
           volumeControl.decreaseVolume();
           logAction("Volume decreased to: " + volumeControl.getVolume());
       } else {
           System.out.println("TV is turned OFF!");
       }
   }
   public void setPowerStatus(boolean isON) {
       this.powerStatus.setPowerStatus(isON);
   }

   /**
    * Закрити лог-файл.
    * @throws IOException якщо виникає помилка при роботі з лог-файлом.
    */
   public void closeLogger() throws IOException {
       logAction("Logger closed.");
       logger.close();
   }

   /**
    * Метод для коректного закриття ресурсів.
    * @throws IOException якщо виникає помилка при закритті лог-файлу.
    */
   @Override
   public void close() throws IOException {
       closeLogger();
   }
}
