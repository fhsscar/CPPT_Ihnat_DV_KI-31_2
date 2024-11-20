package lab03;

import java.io.IOException;

/**
 * Підклас TVWithTuner представляє телевізор з тюнером.
 */
public class TVTuner extends TV implements TunerControl,TunerControl2 {
    private Tuner tuner;

    public TVTuner(boolean isON, int inChannel, int inVolume) throws IOException {
        super(); // Викликаємо конструктор TV
        this.powerStatus = new PowerStatus(isON);
        this.channelControl = new ChannelControl(inChannel);
        this.volumeControl = new VolumeControl(inVolume);
        this.tuner = new Tuner();
    }

    @Override
    public void showInfo() {
        System.out.println("TV With Tuner Info:");
        if (powerStatus.isON()) {
            System.out.println("Power: ON");
            System.out.println("Channel: " + channelControl.getChannel());
            System.out.println("Volume: " + volumeControl.getVolume());
            System.out.println("Current Tuner Frequency: " + tuner.getFrequency());
        } else {
            System.out.println("Power: OFF");
        }
    }

    @Override
    public void tuneToFrequency(double frequency) {
        tuner.setFrequency(frequency);
        System.out.println("Tuned to frequency: " + frequency);
    }

    @Override
    public double getTunedFrequency() {
        return tuner.getFrequency();
    }
}