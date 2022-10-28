using System;
using System.Collections.Generic;

public class Users
{
    public List<User> ListUsers = new List<User>();
    public void AddUser(User NewUser)
    {
        ListUsers.Add(NewUser);
    }
    public void ShowUsers()
    {
        foreach (User user in ListUsers)
        {
            Console.WriteLine(user.Name);
        }
    }
    public bool CheckUserAllow(string login, string pass, ref User curUser)
    {
        foreach (User user in ListUsers)
        {
            if (user.Name == login && user.Pass == pass)
            {
                Console.WriteLine("Доступ разрешён");
                curUser = user;
                return true;
            }
        }
        Console.WriteLine("Введён неверный логин или пароль");
        return false;
    }
}
public class User
{
    public string Name;
    public string Pass;
    public List<Order> MyListOrder = new List<Order>();
    public bool IsAdnim = false;
    public User(string name, string pass)
    {
        Name = name;
        Pass = pass;
    }
    public void ShowMyOrder()
    {
        if (MyListOrder.Count > 0)
        {
            foreach (Order ord in MyListOrder)
            {
                ord.ShowProducts();
            }
        }
        else
        {
            Console.WriteLine("Нет оформленных заказов");
        }
    }
}
public class Product
{
    public string Name;
    public decimal Price;
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
    public void Print()
    {
        Console.WriteLine($"{Name} {Price}");
    }
}

public class Order
{
    public List<Product> Products = new List<Product>();
    public decimal FullPrice;
   
    public Order(List<Product> products)
    {

        foreach (var product in products)
        {
            Products.Add(new Product(product.Name, product.Price));
            FullPrice += product.Price;
        }
    }
    public void ShowProducts()
    {
        Console.WriteLine("Заказ №");
        foreach (Product product in Products)
        {
            product.Print();
        }
        Console.WriteLine($"Сумма заказа {FullPrice}");
    }
}

public class Store
{
    public List<Product> Products;
    public List<Product> Basket;
    public List<Order> Orders;
    public Store()
    {
        Products = new List<Product>
            {
                new Product("Хлеб", 25),
                new Product("Молоко", 100),
                new Product("Печенье", 50),
                new Product("Масло", 250),
                new Product("Йогурт", 300),
                new Product("Сок", 80)
            };

        Basket = new List<Product>();
        Orders = new List<Order>();
    }

    public void AddProductToCatalog(string name, int price)
    {
        Products.Add(new Product(name, price));
    }

    public void ShowCatalog()
    {
        Console.WriteLine("Каталог продуктов");
        ShowProducts(Products);
    }

    public void ShowBasket()
    {
        if (Basket.Count > 0)
        {
            Console.WriteLine("Содержимое корзины");
            ShowProducts(Basket);
        }
        else
        {
            Console.WriteLine("Корзина пуста");
        }
    }

    public void AddToBasket(int numberProduct)
    {
        if (numberProduct > 0 && numberProduct <= Products.Count)
        {
            Basket.Add(Products[numberProduct - 1]);
            Console.WriteLine($"Продукт {Products[numberProduct - 1].Name} успешно добавлен в корзину.");
            Console.WriteLine($"В корзине {Basket.Count} продуктов.");
        }
        else
        {
            Console.WriteLine("Такого товара нет!");
        }
    }

    public void ShowProducts(List<Product> products)
    {
        int number = 1;
        foreach (Product product in products)
        {
            Console.Write(number + ". ");
            product.Print();
            number++;
        }
    }
    public void CreateOrder(User curUser)
    {
        if (Basket.Count > 0)
        {
            Order order = new Order(Basket);
            Orders.Add(order);
            curUser.MyListOrder.Add(order);
            Basket.Clear();
            Console.WriteLine("Заказ создан");
        }
        else
        {
            Console.WriteLine("Корзина пуста, выберите товар");
        }
    }
}

class Program
{
    static void Main()
    {
        Users ListUsers = new Users();
        Store onlineStore = new Store();
        User currentUser = null;
        int numberAction;

        User Den = new User("Denis", "12345");
        User Petr = new User("Petr", "54321");

        ListUsers.AddUser(Den);
        Den.IsAdnim = true;

        ListUsers.AddUser(Petr);

        bool AllowAccess;

        do
        {
            Console.WriteLine("Здравствуйте. Введите логин:");
            string login = Console.ReadLine();
            Console.WriteLine("Введите пароль:");
            string pass = Console.ReadLine();
            AllowAccess = ListUsers.CheckUserAllow(login, pass, ref currentUser);


        } while (AllowAccess == false);



        Console.WriteLine("Здравствуйте.");
        do
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Показать каталог продуктов?");
            Console.WriteLine("2. Посмотреть карзину?");
            Console.WriteLine("3. Добавить продукт в карзину?");
            Console.WriteLine("4. Хотите оформить заказ?");
            Console.WriteLine("5. Посмотреть мои заказы?");
            if (currentUser.IsAdnim == true)
            {
                Console.WriteLine("6. Добавить продукт в каталог?");
            }
            Console.WriteLine("Если хотите выйти наберите 0.");
            Console.WriteLine("Выберите номер действия, которое хотите совершить.");



            numberAction = CheckUserUnsertDigit();

            switch (numberAction)
            {
                case 0:
                    {

                        break;
                    }
                case 1:
                    {
                        onlineStore.ShowCatalog();
                        break;
                    }
                case 2:
                    {
                        onlineStore.ShowBasket();
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Напишите номер продукта, которого нужно добавить в корзину");
                        int productNumber = CheckUserUnsertDigit();
                        onlineStore.AddToBasket(productNumber);
                        break;
                    }
                case 4:
                    {
                        onlineStore.CreateOrder(currentUser);
                        break;
                    }
                case 5:
                    {
                        currentUser.ShowMyOrder();
                        break;
                    }
                case 6:
                    {
                        if (currentUser.IsAdnim != true)
                        {
                            goto default;
                        }
                        else
                        {

                            string name;
                            do
                            {
                                Console.WriteLine("Введите название");
                                name = Console.ReadLine();
                                if (name.Length == 0)
                                {
                                    Console.WriteLine("Название не может быть меньше 1 символа");
                                }
                            }
                            while (name.Length == 0);

                            Console.WriteLine("Введите цену");

                            int price = CheckUserUnsertDigit();

                            if (price < 0)
                            {
                                Console.WriteLine("Цена не может быть ниже 0!");
                                break;
                            }


                            onlineStore.AddProductToCatalog(name, price);
                        }
                        break;
                    }
                default:
                    Console.WriteLine("Вы ввели несуществующее действие!\n" +
                                      "Выберите номер действия из списка");
                    break;
            }
        } while (IsExit(numberAction));

    }

    static bool IsExit(int answer)
    {
        return answer != 0;
    }
    static int CheckUserUnsertDigit()
    {
    Start:
        string numStudent = Console.ReadLine();
        if (numStudent != "")
        {
            foreach (char el in numStudent)
            {
                if (char.IsLetter(el))
                {
                    Console.WriteLine("Вы ввели букву, попробуйте снова");
                    goto Start;
                }
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод, попробуйте снова");
            goto Start;
        }

        if (Convert.ToInt32(numStudent) < 0)
        {
            Console.WriteLine("Некорректный ввод, попробуйте снова");
            goto Start;
        }
        return Convert.ToInt32(numStudent);
    }
}