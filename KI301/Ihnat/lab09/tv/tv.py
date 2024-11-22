import logging

class TV:
    def __init__(self, is_on=False, channel=1, volume=10):
        self.is_on = is_on
        self.channel = channel
        self.volume = volume
        logging.basicConfig(filename="tv_log.txt", level=logging.INFO)
        logging.info("TV initialized with default settings")

    def turn_on(self):
        if not self.is_on:
            self.is_on = True
            logging.info("TV turned ON")
        else:
            print("TV is already ON")

    def turn_off(self):
        if self.is_on:
            self.is_on = False
            logging.info("TV turned OFF")
        else:
            print("TV is already OFF")

    def change_channel(self, new_channel):
        if self.is_on:
            self.channel = new_channel
            logging.info(f"Channel changed to: {new_channel}")
        else:
            print("TV is OFF. Turn it ON first.")

    def change_volume(self, new_volume):
        if self.is_on:
            self.volume = new_volume
            logging.info(f"Volume changed to: {new_volume}")
        else:
            print("TV is OFF. Turn it ON first.")

    def show_info(self):
        status = "ON" if self.is_on else "OFF"
        print(f"TV Status: {status}, Channel: {self.channel}, Volume: {self.volume}")
