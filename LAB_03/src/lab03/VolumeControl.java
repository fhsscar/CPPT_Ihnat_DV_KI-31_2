package lab03;

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