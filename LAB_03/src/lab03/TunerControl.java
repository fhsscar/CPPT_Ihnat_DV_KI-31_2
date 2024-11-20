package lab03;

/**
 * Інтерфейс TunerControl для керування тюнером.
 */
public abstract interface TunerControl {
    void tuneToFrequency(double frequency);
    double getTunedFrequency();
}
