package lab06;


import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/**
 * Параметризований клас "Поличка". 
 * 
 * @param <T> тип елементів, які зберігаються на поличці. 
 */
public class Shelf<T> {
    private List<T> shelf;

    /** 
     * Конструктор класу "Поличка", який ініціалізує список.
     */
    public Shelf() {
        shelf = new ArrayList<>();
    }

    /** 
     * Додає елемент на поличку. 
     * 
     * @param item елемент для додавання
     */
    public void addItem(T item) {
        shelf.add(item);
        System.out.println(item + " added to the shelf.");
    }

    /** 
     * Видаляє елемент з полички за індексом.
     * 
     * @param index індекс елемента для видалення
     * @return видалений елемент або null, якщо індекс некоректний
     */
    public T removeItem(int index) {
        if (index >= 0 && index < shelf.size()) {
            T item = shelf.remove(index);
            System.out.println(item + " removed from the shelf.");
            return item;
        } else {
            System.out.println("Invalid index. The shelf contains " + shelf.size() + " items.");
            return null;
        }
    }

    /** 
     * Знаходить мінімальний елемент на поличці, якщо всі елементи - числа.
     * 
     * @return мінімальний елемент або null, якщо поличка порожня або елементи не Comparable
     */
    public T findMinNumbers() {
        if (shelf.isEmpty()) {
            System.out.println("The shelf is empty, no items to search.");
            return null;
        }
        
        List<Double> numbers = new ArrayList<>();
        for (T item : shelf) {
            if (item instanceof Number) {
                numbers.add(((Number) item).doubleValue());
            }
        }

        if (numbers.isEmpty()) {
            System.out.println("No numeric items to compare.");
            return null;
        }

        double minNumber = Collections.min(numbers);
        System.out.println("The minimum numeric item on the shelf: " + minNumber);
        return (T) (Number) minNumber; // Повертаємо як Number
    }

    /** 
     * Знаходить мінімальний елемент на поличці, якщо всі елементи - рядки.
     * 
     * @return мінімальний елемент або null, якщо поличка порожня або елементи не Comparable
     */
    public T findMinStrings() {
        if (shelf.isEmpty()) {
            System.out.println("The shelf is empty, no items to search.");
            return null;
        }
        
        List<String> strings = new ArrayList<>();
        for (T item : shelf) {
            if (item instanceof String) {
                strings.add((String) item);
            }
        }

        if (strings.isEmpty()) {
            System.out.println("No string items to compare.");
            return null;
        }

        String minString = Collections.min(strings);
        System.out.println("The minimum string item on the shelf: " + minString);
        return (T) minString;
    }

    /** 
     * Повертає кількість чисел на поличці.
     * 
     * @return кількість чисел
     */
    public int countNumbers()
    {
    	int count=0;
    	for(T item : shelf)
    	{
    		if(item instanceof Number)
    			count++;
    	}
    	return count;
    }
 

    /** 
     * Повертає кількість рядків на поличці.
     * 
     * @return кількість рядків
     */
 
    public int countStrings()
    {
    	int count=0;
    	for(T item: shelf)
    	{
    		if(item instanceof String)
    		{
    			count ++;
    		}
    	}
    	return count;
    }

    /** 
     * Виводить всі елементи на поличці.
     */
    public void viewItems() {
        System.out.println("Items on the shelf: " + shelf);
    }
}
