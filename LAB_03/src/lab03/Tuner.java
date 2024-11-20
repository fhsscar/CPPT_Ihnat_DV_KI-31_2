package lab03;

/**
 * Клас Tuner відповідає за налаштування частот тюнера.
 */
public class Tuner {
    private double frequency;

    public Tuner() {
        this.frequency = 0.0; // Значення за замовчуванням
    }

    public void setFrequency(double frequency) {
        this.frequency = frequency;
    }

    public double getFrequency() {
        return frequency;
    }
}
