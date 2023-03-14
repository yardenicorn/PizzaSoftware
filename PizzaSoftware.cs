using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThePizzaSoftware
{
    public static class PizzaSoftware
    {
        // This list contains a list of all the pizzas the costumer ordered and inside that list - a list of the toppings on each pizza
        static List<List<string>> PizzasList = new List<List<string>>();
        public static void Main()
        {
            // Greeting message
            Console.WriteLine("Hello and Welcome to The Unicorn Pizza!");

            // Get the number of pizzas from the user
            int pizzasnum = ReadUserInput("How many pizzas would you like to order?", new List<string>());

            // Checks if today is Sunday (because if it does, it's Gamers Sunday...)
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                while (pizzasnum < 5)
                {
                    Console.WriteLine("It's Gamers Sunday! We do not accept orders under 5 pizzas. Sorry!");
                    pizzasnum = ReadUserInput("How many pizzas would you like to order?", new List<string>());
                }
            }
            

            // Get all pizzas orders
            GetPizzas(pizzasnum);

            // Allow the user to change the order
            RemovePizza();

            // Order summery
            Console.WriteLine("Your order:");
            CostumersOrder();
            Console.WriteLine("Thank you for buying at The Unicorn Pizza!");

            // Pause app until any key.
            Console.ReadKey();
        }


        /// <summary>
        /// Recive pizzas order for user 
        /// </summary>
        /// <param name="pizzasnum">Number of pizzas to get from user</param>
        public static void GetPizzas(int pizzasnum)
        {
            // Get all pizzas from the user
            for (int i = 0; i < pizzasnum; i++)
            {
                Console.WriteLine($"Create pizza number {i + 1}:");
                List<string> nextPizza = GetSinglePizza();
                PizzasList.Add(nextPizza);
                PrintPizza($"Pizza number {i + 1} has:", nextPizza);
            }
        }


        // Function that checks if the costumer wants to remove pizza from his order
        private static void RemovePizza()
        {
            string toRemove = "Is there any pizza you would like to remove from your order?";
            int whichPizza = ReadUserInput(toRemove, new List<string> { "Yes", "No" });

            // Keep asking the user if he wants to remove pizzas untils he is done
            while (whichPizza == 1)
            {
                CheckTheList();

                if (PizzasList.Count == 0)
                {
                    break;
                }

                whichPizza = ReadUserInput(toRemove, new List<string> { "Yes", "No" });
            }

        }


        /// <summary>
        /// Print a single pizza to console 
        /// </summary>
        /// <param name="pizzaName"> Name for the pizza</param>
        /// <param name="pizza"> List of toppings</param>
        public static void PrintPizza(string pizzaName, List<string> pizza)
        {
            Console.WriteLine(pizzaName);
            foreach (var item in pizza)
            {
                Console.WriteLine($"     {item}");
            }
        }

        

        // Function that checks which pizza the costumer wants to remove by the pizza's number in the list
        private static void CheckTheList()
        {
            Console.WriteLine("Please enter the number of the pizza you want to remove");
            CostumersOrder();
            int remove = Convert.ToInt32(Console.ReadLine());
            PizzasList.RemoveAt(remove - 1);
        }

        /// <summary>
        /// This method gets an input from the user and makes sure its a valid number
        /// </summary>
        /// <param name="message">
        /// Message to user
        /// </param>
        /// <param name="options">
        /// List of valid options - can be empty
        /// </param>
        /// <returns>
        /// The number the user had input - a valid number in the limits of the list.
        /// if the list is empty - the return value can be any positive number above 0.
        /// </returns>
        public static int ReadUserInput(string message, List<string> options)
        {
            Console.WriteLine(message);

            // Print all toppings options (happens ONLY if the function gets a list param)
            int index = 1;
            foreach (var item in options)
            {
                Console.WriteLine($"{index++}. {item}");
            }
            
            // Get the number of topping from the costumer
            // Check if the input is a number
            bool IsNumeric = Int32.TryParse(Console.ReadLine(), out int UserInput);

            // While input is incorrect - get another input
            // Either input is not a number 
            // Or number is below 1 (Zero or below are not allowed)
            // Or Number is greater then the number of valid options ONLY in case options is not empty
            while (!IsNumeric  || UserInput < 1 || (options.Count > 0 && UserInput > options.Count))
            {
                Console.WriteLine("Please enter valid numbers only");
                IsNumeric = Int32.TryParse(Console.ReadLine(), out UserInput);
            }

            // if input is ok - return user input
            return UserInput;
        }

        // Creates the pizza the costumer wants
        private static List<string> GetSinglePizza()
        {
            List<string> PizzaToppings = new List<string> {"extra cheese", "green olives", "black olives", "mushrooms", "onions",
                                                           "garlic", "corn", "pepperoni", "tuna", "ham", "Goomba Sauce", "I'm done!"};

            string toppingsMessage = "What toppings would you like on your pizza? (please enter the number of the topping you would like";

            int topping = ReadUserInput(toppingsMessage, PizzaToppings);

            List<string> OnePizza = new List<string>();

            // Get toppings from costumer until he is done
            while (topping != PizzaToppings.Count)
            {
                OnePizza.Add(PizzaToppings[topping - 1]);
                Console.WriteLine($"Adding {PizzaToppings[topping - 1]} to your pizza, anything else?");

                // Get the next costumer selection
                topping = ReadUserInput(toppingsMessage, PizzaToppings);
            }

            return OnePizza;
        }

        // Print the Costumer's order
        public static void CostumersOrder()
        {
            // Iterate all pizzas
            int i = 1;
            foreach (var pizza in PizzasList)
            {
                // Print current pizza
                PrintPizza($"Pizza number {i++}" , pizza);
                Console.WriteLine();
            }
        }

    }
}
