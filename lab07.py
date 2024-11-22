def main():
    # Введення розміру матриці та символу-заповнювача
    n_rows = int(input("Введіть розмір квадратної матриці (непарне число): "))
    filler = input("\nВведіть символ-заповнювач: ")

    # Перевірка довжини символу-заповнювача
    if len(filler) != 1:
        print("Помилка: введіть один символ!")
        return

    # Відкриття файлу для запису
    with open("MyFile.txt", "w", encoding="utf-8") as fout:
        # Основний цикл для побудови та виводу шаблону
        for i in range(n_rows):
            if i % 2 == 0:
                # Якщо рядок парний, заповнюємо пробілами
                row_content = " " * n_rows
            else:
                # Якщо рядок непарний, заповнюємо символом-заповнювачем
                row_content = filler * n_rows

            print(row_content)
            fout.write(row_content + "\n")

if __name__ == "__main__":
    main()
