import math
import struct


class ExceptionCalculator:
    """
    Клас для обчислення виразу y = cos(x) / tan(2x).
    Забезпечує обробку виключень та збереження результатів у файл.
    """

    @staticmethod
    def calculate(x):
        """
        Обчислює значення виразу y = cos(x) / tan(2x).

        :param x: Значення змінної x.
        :return: Значення виразу y.
        :raises ValueError: якщо тангенс занадто малий або рівний нулю.
        """
        tan2x = math.tan(2 * x)
        # Перевірка на нульове або дуже мале значення тангенса
        if abs(tan2x) < 1e-10:
            raise ValueError("Error: unable to calculate tan(2x).")
        y = math.cos(x) / tan2x
        return y

    @staticmethod
    def save_result_to_text_file(result, filename):
        """
        Записує результат обчислення у текстовий файл.

        :param result: Результат обчислення.
        :param filename: Ім'я файлу для запису результату.
        :raises IOError: якщо виникає помилка запису у файл.
        """
        try:
            with open(filename, "w") as file:
                file.write(f"Result: {result}")
        except IOError as e:
            # Записуємо повідомлення про помилку в текстовий файл "error_log.txt"
            with open("error_log.txt", "a") as error_file:
                error_file.write(f"Error writing to text file '{filename}': {e}\n")
            # Повторно піднімаємо виняток для подальшої обробки, якщо це потрібно
            raise IOError("Error writing to file.")

    @staticmethod
    def save_result_to_binary_file(result, filename):
        """
        Записує результат обчислення у двійковий файл.

        :param result: Результат обчислення.
        :param filename: Ім'я файлу для запису результату.
        :raises IOError: якщо виникає помилка запису у файл.
        """
        try:
            with open(filename, "wb") as file:
                file.write(struct.pack("f", result))
        except IOError as e:
            # Записуємо повідомлення про помилку в текстовий файл "error_log.txt"
            with open("error_log.txt", "a") as error_file:
                error_file.write(f"Error writing to binary file '{filename}': {e}\n")
            raise IOError("Error writing to binary file.")

    @staticmethod
    def read_from_text_file(filename):
        """
        Зчитує результат обчислення з текстового файлу.

        :param filename: Ім'я файлу для зчитування.
        :return: Результат обчислення.
        """
        try:
            with open(filename, "r") as file:
                data = file.read()
            return data
        except IOError as e:
            with open("error_log.txt", "a") as error_file:
                error_file.write(f"Error reading from text file '{filename}': {e}\n")
            raise IOError("Error reading from file.")

    @staticmethod
    def read_from_binary_file(filename):
        """
        Зчитує результат обчислення з двійкового файлу.

        :param filename: Ім'я файлу для зчитування.
        :return: Результат обчислення.
        """
        try:
            with open(filename, "rb") as file:
                result = struct.unpack("f", file.read(4))[0]
            return result
        except IOError as e:
            with open("error_log.txt", "a") as error_file:
                error_file.write(f"Error reading from binary file '{filename}': {e}\n")
            raise IOError("Error reading from binary file.")
