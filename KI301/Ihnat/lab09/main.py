from tv.tv import TV
from tv.smart_tv import SmartTV

def main():
    my_tv = TV()
    my_tv.turn_on()
    my_tv.change_channel(5)
    my_tv.change_volume(20)
    my_tv.show_info()

    smart_tv = SmartTV()
    smart_tv.turn_on()
    smart_tv.connect_internet()
    smart_tv.change_channel(10)
    smart_tv.change_volume(15)
    smart_tv.show_info()

if __name__ == "__main__":
    main()
