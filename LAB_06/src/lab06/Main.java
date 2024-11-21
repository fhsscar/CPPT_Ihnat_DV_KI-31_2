package lab06;


/**
 * Клас-водій для тестування класу Shelf.
 */
public class Main {
    public static void main(String[] args) {
        // Створення полички для елементів різних типів
        Shelf<Object> shelf = new Shelf<>();
        
        // Додавання цілочисельних значень
        shelf.addItem(10);        // Додавання числа
        shelf.addItem(5);         // Додавання числа
        shelf.addItem(20);        // Додавання числа
        
        // Додавання рядкових значень
        shelf.addItem("Apple");   // Додавання рядка
        shelf.addItem("Banana");  // Додавання рядка
        shelf.addItem("Cherry");   // Додавання рядка

        // Перегляд елементів
        shelf.viewItems();
        
        // Пошук мінімального елемента серед чисел
        shelf.findMinNumbers();
        
        // Пошук мінімального елемента серед рядків
        shelf.findMinStrings();
        
        // Виводимо кількість чисел та рядків на поличці
        System.out.println("Curent number of numeric items on r=the shelf :"+shelf.countNumbers());
        System.out.println("Curent number of string items on the shelf :"+shelf.countStrings());
      
        
        // Видалення елемента за індексом
        shelf.removeItem(0); // Видалення першого елемента
        
        // Перегляд елементів після видалення
        shelf.viewItems();
        
        // Виводимо кількість чисел та рядків на поличці після видалення
        System.out.println("Current number of numeric items on the shelf after removal: " + shelf.countNumbers());
        System.out.println("Current number of string items on the shelf after removal: " + shelf.countStrings());
    }
}
