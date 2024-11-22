from KI301.Ihnat.lab08.ExceptionCalculator import ExceptionCalculator




def main():
    filename_text = "result.txt"
    filename_binary = "result.bin"

    try:
        x = float(input("Enter the value of x: "))
        result = ExceptionCalculator.calculate(x)
        print(f"Result: {result}")

        # Запис результату у файли
        ExceptionCalculator.save_result_to_text_file(result, filename_text)
        ExceptionCalculator.save_result_to_binary_file(result, filename_binary)
        print(f"Result saved to files: {filename_text} and {filename_binary}")

        # Читання результатів з файлів
        print("Read result from text file:", ExceptionCalculator.read_from_text_file(filename_text))
        print("Read result from binary file:", ExceptionCalculator.read_from_binary_file(filename_binary))

    except ValueError as e:
        print("Calculation error:", e)
    except IOError as e:
        print("File operation error:", e)



if __name__ == "__main__":
    main()
