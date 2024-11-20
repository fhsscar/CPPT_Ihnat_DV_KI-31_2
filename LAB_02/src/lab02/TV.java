package lab02;

import java.io.FileWriter;
import java.io.IOException;

/**
*Клас TV представляє телевізор з можливістю керування каналами, гучністю та станом живлення.
*/

public class TV implements AutoCloseable {
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
   public void showInfo() {
   	
   	  if (powerStatus.isON())
   	  {
   		  System.out.println("TV Status: " + powerStatus.isON());
   	        System.out.println("Current Channel: " + channelControl.getChannel());
   	        System.out.println("Current Volume: " + volumeControl.getVolume());
   	  }
   	  else {
   		  System.out.println("TV is turned OFF!");
   	  }
   	 }
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

   /**
    * Клас VolumeControl відповідає за регулювання гучності телевізора.
    */
   class VolumeControl {
       private int volume;
       private final int minVolume = 0;
       private final int maxVolume = 100;

       /**
        * Конструктор без параметрів. Ініціалізує гучність зі значенням за замовчуванням.
        */
       public VolumeControl() {
           this.volume = 0;
       }

       /**
        * Конструктор з параметром для встановлення початкової гучності.
        * @param volume початкова гучність
        */
       public VolumeControl(int volume) {
           this.volume = volume;
       }

       /**
        * Встановити гучність.
        * @param volume новий рівень гучності
        */
       public void setVolume(int volume) {
           if (volume >= minVolume && volume <= maxVolume) {
               this.volume = volume;
           } else {
               System.out.println("Invalid volume level!");
           }
       }

       /**
        * Отримати поточний рівень гучності.
        * @return поточна гучність
        */
       public int getVolume() {
           return volume;
       }

       /**
        * Вимкнути гучність (mute).
        */
       public void mute() {
           this.volume = 0;
       }

       /**
        * Збільшити гучність на одиницю.
        */
       public void increaseVolume() {
           if (volume < maxVolume) {
               this.volume++;
           }
       }

       /**
        * Зменшити гучність на одиницю.
        */
       public void decreaseVolume() {
           if (volume > minVolume) {
               this.volume--;
           }
       }
   }

   /**
    * Клас ChannelControl відповідає за керування каналами телевізора.
    */
   class ChannelControl {
       private int channel;

       /**
        * Конструктор без параметрів. Ініціалізує канал зі значенням за замовчуванням.
        */
       public ChannelControl() {
           this.channel = 0;
       }

       /**
        * Конструктор з параметром для встановлення початкового каналу.
        * @param channel початковий канал
        */
       public ChannelControl(int channel) {
           this.channel = channel;
       }

       /**
        * Встановити новий канал.
        * @param channel новий номер каналу
        */
       public void setChannel(int channel) {
           this.channel = channel;
       }

       /**
        * Отримати поточний канал.
        * @return поточний номер каналу
        */
       public int getChannel() {
           return channel;
       }
   }

   /**
    * Клас PowerStatus відповідає за стан живлення телевізора (увімкнено/вимкнено).
    */
   class PowerStatus {
       private boolean isON;

       /**
        * Конструктор без параметрів. Ініціалізує стан телевізора як вимкнений.
        */
       public PowerStatus() {
           this.isON = false;
       }

       /**
        * Конструктор з параметром для встановлення початкового стану живлення.
        * @param isON початковий стан (ввімкнений/вимкнений)
        */
       public PowerStatus(boolean isON) {
           this.isON = isON;
       }

       /**
        * Перевірити, чи телевізор увімкнений.
        * @return true, якщо телевізор увімкнений, інакше false.
        */
       public boolean isON() {
           return isON;
       }

       /**
        * Увімкнути телевізор.
        */
       public void turnON() {
           this.isON = true;
       }

       /**
        * Вимкнути телевізор.
        */
       public void turnOFF() {
           this.isON = false;
       }
   }

	
}
