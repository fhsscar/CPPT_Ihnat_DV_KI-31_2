from .tv import TV
import logging

class SmartTV(TV):
    def __init__(self, is_on=False, channel=1, volume=10, internet_connected=False):
        super().__init__(is_on, channel, volume)
        self.internet_connected = internet_connected
        logging.info("SmartTV initialized with internet connectivity")

    def connect_internet(self):
        if self.is_on:
            self.internet_connected = True
            logging.info("Internet connected")
        else:
            print("Turn ON the TV to connect to the internet")

    def disconnect_internet(self):
        if self.is_on:
            self.internet_connected = False
            logging.info("Internet disconnected")
        else:
            print("TV is OFF")

    def show_info(self):
        super().show_info()
        internet_status = "Connected" if self.internet_connected else "Disconnected"
        print(f"Internet: {internet_status}")
