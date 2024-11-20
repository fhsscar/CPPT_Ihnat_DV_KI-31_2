package lab03;

/**
 * Клас PowerStatus відповідає за стан живлення телевізора (увімкнено/вимкнено).
 */
public class PowerStatus { // Зробіть клас публічним, щоб його можна було імпортувати
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

    /**
     * Встановити стан живлення.
     * @param isON новий стан (ввімкнений/вимкнений)
     */
    public void setPowerStatus(boolean isON) {
        this.isON = isON;
    }
}
