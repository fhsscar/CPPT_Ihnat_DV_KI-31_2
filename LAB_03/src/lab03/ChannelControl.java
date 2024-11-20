package lab03;

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