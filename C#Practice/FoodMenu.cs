using System;

class OrderItem
{
    public string item {get; set;}
    public double itemQuantity {get; set;}
    public bool Extra {get; set;}

    public OrderItem(string ItemName, double ItemQty, bool isExtra)
    {
        this.item = ItemName;
        this.itemQuantity = ItemQty;
        this.Extra = isExtra;
    }
}

class Orders
{
    public string orderNumber {get; set;}
    public string CustomerType { get; set; }
    public double Subtotal { get; set; }
    public double Discount { get; set; }
    public double Total { get; set; }
    public double AmountPaid { get; set; }
    public double Change { get; set; }
    public List<OrderItem> Items { get; set; }
}

class Menu
{
    public void viewMenu()
    {
        Console.WriteLine("\n★====================✦===================★\n           MENU           \n★====================✦===================★\n");
        Console.WriteLine("\n★===================✦===================★\n           MAIN DISH           \n★====================✦===================★\n");
        Console.Write($">    {"EGGSILOG", -15}      -      P140\n>    {"TAPSILOG", -15}      -      P120\n>    {"LONGSILOG", -15}      -      P160\n>    {"SISIG", -15}      -      P200\n");

        Console.WriteLine("\n★===================✦===================★\n           DRINKS           \n★====================✦====================★\n");
        Console.Write($">    {"BEER", -15}      -      P60\n>    {"TANDUAY LIGHT", -15}      -      P80\n>    {"TANDUAY DARK", -15}      -      P100\n>    {"COKE", -15}      -      P25\n>    {"MOUNTAIN DEW", -15}      -      P25\n>    {"SPRITE", -15}      -      P25\n>    {"ROYAL", -15}      -      P25\n");

        Console.WriteLine($"\n★===================✦===================★\n           SIDES           \n★====================✦====================★\n");
        Console.Write($">    {"ICE CREAM", -15}      -      P20\n>    {"FRIES", -15}      -      P80\n>    {"NAGARAYA", -15}      -      P20\n>    {"BURGER", -15}      -      P50\n>    {"NUGGETS", -15}      -      P40\n");

        Console.WriteLine($"\n★===================✦===================★\n           EXTRA / ADD - ONS           \n★====================✦====================★\n");
        Console.Write($">    {"EXTRA KETCHUP", -15}      -      P2.50\n>    {"EXTRA GRAVY", -15}      -      P3.25\n>    {"EXTRA PLAIN RICE", -14}      -      P20.15\n>    {"EXTRA JAVA RICE", -15}      -      P15.20\n");

        Console.Write("\nPress any key to go back");
        Console.ReadKey();
    }
}

class Order
{   
    private double totalOrderSum = 0;
    private int orderCount = 0;
    public List<OrderItem> OrderedItem = new List<OrderItem>();
    public List<Orders> orderHistory = new List<Orders>();
    private Dictionary<string, double> menu = new Dictionary<string, double>
    {
        // MAIN DISH
        ["EGGSILOG"] = 140,
        ["TAPSILOG"] = 120,
        ["LONGSILOG"] = 160,
        ["SISIG"] = 200,

        // DRINKS
        ["BEER"] = 60,
        ["TANDUAY LIGHT"] = 80,
        ["TANDUAY DARK"] = 100,
        ["COKE"] = 25,
        ["MOUNTAIN DEW"] = 25,
        ["SPRITE"] = 25,
        ["ROYAL"] = 25,

        //SIDES
        ["ICE CREAM"] = 20,
        ["FRIES"] = 80,
        ["NAGARAYA"] = 20,
        ["BURGER"] = 50,
        ["NUGGETS"] = 40,

        //EXTRAS/ADD-ONS
        ["EXTRA KETCHUP"] = 2.50,
        ["EXTRA GRAVY"] = 3.25,
        ["EXTRA PLAIN RICE"] = 20.15,
        ["EXTRA JAVA RICE"] = 15.20,
    };

    public void computeOrder()
    {
        double addedSum = 0;
        foreach (OrderItem item in OrderedItem)
        {
            if (menu.ContainsKey(item.item))
            {
                double itemPrice = menu[item.item];

                double totalMulti = itemPrice * item.itemQuantity;                                                                                 
                addedSum += totalMulti;

                Console.Write($"\n> x{item.itemQuantity, -5} {item.item, -15} {totalMulti, 10}\n");
            }
        }
        Console.WriteLine($"\n------------------------------- --------------\n>    ITEM              TOTAL: {addedSum}\n");
        Console.WriteLine("\nPress any key to go back.");
        Console.ReadKey();
        totalOrderSum = addedSum;
    }   

    public void viewOrder()
    {
        int i = 1;
        Console.Write("\n========================================\n               CURRENT ORDER               \n========================================\n");

        foreach (OrderItem item in OrderedItem)
        {
            double price = menu[item.item]; 
            if (item.Extra == true) continue;               
            Console.Write($"\n{i}.  {item.item}\n    Quantity: {item.itemQuantity}\n    Price: {price}\n    Total: {price * item.itemQuantity}\n");
            i++; 
        }

        foreach (OrderItem item in OrderedItem)
        {
            double price = menu[item.item];

            if (item.Extra != true) continue;
            Console.Write($"\n+   {item.item}\n    Quantity: {item.itemQuantity}\n    Price: {price}\n");
        }
        Console.WriteLine("\n---------------------------------------\n");
        Console.WriteLine($"SUBTOTAL: {totalOrderSum}");
        Console.Write("\n========================================\n");

        Console.Write("\nPress any key to go back.");
        Console.ReadKey();
    }

    public void checkout( )
    {
        bool repeat = true;
        string customerLabel = " ";
        double discountPercent = 0.00;

        while(repeat)
        {
            Console.Write("\n========================================\n              CUSTOMER TYPE              \n========================================\n");
            Console.Write("\n[1] Regular Costumer\n[2] Senior Citizen\n[3] PWD\nEnter customer type: ");
            int customer = Convert.ToInt32(Console.ReadLine());
            if (customer == 1)
            {
                customerLabel = "Regular";
                discountPercent = 0.00;
                repeat = false;
            }
            else if (customer == 2)
            {
                customerLabel = "Senior Citizen";
                discountPercent = 0.20;
                repeat = false;
            }
            else if (customer == 3)
            {
                customerLabel = "PWD";
                discountPercent = 0.20;
                repeat = false;
            } else
            {
                Console.WriteLine("\nInvalid input. Try again.");
                repeat = true;
            }
        }

        double discountAmount = totalOrderSum * discountPercent;
        double finalTotal = totalOrderSum - discountAmount;

        Console.Write("\n========================================\n              CHECKOUT              \n========================================\n");
        Console.Write($"\nCustomer Type: {customerLabel}\n");
        Console.Write("\n-----------------------------------------\n");
        foreach (OrderItem item in OrderedItem)
        {
            double itemPrice = menu[item.item];
            double totalMulti = itemPrice * item.itemQuantity;

            Console.Write($"\n> x{item.itemQuantity, -5} {item.item, -15} {totalMulti, -15}\n");
        }
        Console.Write("\n-----------------------------------------\n");
        Console.Write($"\nSUBTOTAL: {totalOrderSum}\n");
        Console.Write($"\n{customerLabel}: {discountPercent}\n");
        Console.Write("\n-----------------------------------------\n");
        Console.Write($"\nTOTAL: {finalTotal}\n");

        double payment = 0;
        while (payment < finalTotal)
        {

            Console.Write("Enter Payment: ");
            payment = Convert.ToDouble(Console.ReadLine());

            if(payment < finalTotal)
            {
                Console.Write("\n========================================\n              INSUFFICIENT PAYMENT              \n========================================\n");
                Console.Write($"\nAmount: {finalTotal}");
                Console.Write($"\nPayment: {payment}");
                Console.Write($"\nRemaining: {finalTotal - payment}");
                Console.WriteLine("\nPlease enter sufficient payment.");
            }
        }

        double change = payment - finalTotal;
        orderCount++;

        string orderNumb = $"{orderCount:D4}";

        // RECEIPT
        Console.Write("\n========================================\n              KOPI BAR RECEIPT              \n========================================\n");
        Console.Write($"\nORDER NUMBER: #{orderNumb}\n");
        Console.Write("\n-----------------------------------------\n");
        foreach (OrderItem item in OrderedItem)
        {
            double itemPrice = menu[item.item];
            double totalMulti = itemPrice * item.itemQuantity;

            Console.Write($"\n> x{item.itemQuantity, -5} {item.item, -15} {totalMulti, 15}\n");
        }
        Console.Write("\n-----------------------------------------\n");
        Console.Write($"\nSUBTOTAL: {totalOrderSum}\n");
        Console.Write($"\n{customerLabel}: {discountPercent}\n");
        Console.Write("\n-----------------------------------------\n");
        Console.Write($"\nTOTAL: {finalTotal, -35}\n");
        Console.Write($"\nPAYMENT: {payment, -35}\n");
        Console.Write($"\nCHANGE: {change, -35}\n");
        Console.Write("\n========================================\n              THANK YOU COME AGAIN!              \n========================================\n");

        Orders history = new Orders
        {
            CustomerType = customerLabel,
            Subtotal = totalOrderSum,
            AmountPaid = payment,
            Total = finalTotal,
            Change = change,
            orderNumber = orderNumb,
            Items = new List<OrderItem>(OrderedItem)
        };

        orderHistory.Add(history);

        OrderedItem.Clear(); 
        totalOrderSum = 0;
        Console.Write("\nPress any key to exit.");
        Console.ReadKey();
    }

    public void OrderHistory()
    {
        Console.Write("\n========================================\n              ORDER HISTORY              \n========================================\n");
        foreach (Orders  order in orderHistory)
        {
            Console.Write($"\nOrder: {order.orderNumber}");
            Console.Write($"\nCustomer Type: {order.CustomerType}");
            Console.Write($"\nTotal: {order.Total}");
            Console.Write("\n-----------------------------------------\n");
        }
        
        Console.Write("\n========================================\n");
        Console.Write($"Total completed orders: {orderHistory.Count}");

        Console.Write("\nPress any key to exit.\n");
        Console.ReadKey();
    }

    public void updateOrder()
    {
        Console.Write("\n========================================\n               UPDATE ORDER               \n========================================\n");

        if (OrderedItem.Count == 0)
        {
            Console.WriteLine("\nThere's currently no order.\n Press any key to go back.");
            Console.ReadKey();
        } else
        {
            Console.Write("Enter item to update: ");
            string updateItem = Console.ReadLine().ToUpper();

            for (int i = 0; i < OrderedItem.Count; i++)
            {
                if (updateItem == OrderedItem[i].item)
                {
                    Console.Write($"\nCurrent Quantity: {OrderedItem[i].itemQuantity}");

                    Console.Write("\nEnter new quantity: ");
                    int newQty = Convert.ToInt32(Console.ReadLine());

                    OrderedItem[i].itemQuantity = newQty;

                    Console.WriteLine("\nUpdated Quantity succesfully.");
                    computeOrder();
                    break;
                }
            }
        }
    }

    public void cancelItem()
    {
        int index = 1;
        foreach (OrderItem item in OrderedItem)
        {
            Console.Write($"\n [{index}]      {item.item, -15}     x{item.itemQuantity}\n");
            index++;
        }

        Console.Write("Enter item to cancel: ");
        int cancelItem = Convert.ToInt32(Console.ReadLine());
        int indexItem = cancelItem - 1;

        if (indexItem >= 0 && indexItem < OrderedItem.Count)
        {
            string itemName = OrderedItem[indexItem].item;

            OrderedItem.RemoveAt(indexItem);

            Console.WriteLine($"\nSuccesfully removed {itemName}");
            computeOrder();
        }
    }

    public void cancelEntireOrder()
    {
        bool repeat = true;
        Console.Write("Are you sure you want to cancel this order?\n[Y] Yes\n[N] No\nEnter Choice:");
        string choice = Console.ReadLine();

        while (repeat) 
        {
            if(choice.ToUpper() == "Y")
            {
                OrderedItem.Clear();
                Console.WriteLine("\n ORDER HAS BEEN SUCCESFULLY REMOVED.");
                computeOrder();
                repeat = false;
            } else if (choice.ToUpper() == "N")
            {
                repeat = false;
            } else
            {
                Console.Write("\nInvalid input. Please try again.");
                repeat = true;
            }
        }
    }

    public void newOrder()
    {
        bool itemRepeat = true;
        bool repeat = true;
        bool extraItemRepeat = true;
        while(repeat)
        {
            try
            {
                    
                    Console.WriteLine("\n★====================✦===================★\n           NEW ORDER!           \n★====================✦===================★\n");
                    Console.Write("Enter item: ");
                    string ITEM = Console.ReadLine();

                    Console.Write("Enter quantity: ");
                    int ITEM_QUANTITY = Convert.ToInt32(Console.ReadLine());

                    addItem(ITEM, ITEM_QUANTITY);
                    
                    Console.Write("Add another item? Y/N: ");
                    string ITEM_ADD = Console.ReadLine();

                    if (ITEM_ADD == "N" || ITEM_ADD == "n")
                    {
                        Console.Write("\nAdd extra/add-ons? Y/N: ");
                        string addExtra = Console.ReadLine();

                        if (addExtra == "Y" ||addExtra == "y")
                        {
                            while(extraItemRepeat)
                            {
                                Console.Write("\n[1] EXTRA KETCHUP\n[2] EXTRA GRAVY\n[3] EXTRA PLAIN RICE\n[4] EXTRA JAVA RICE\n[5] No Extra\nEnter choice: ");
                                int ExtraItem = Convert.ToInt32(Console.ReadLine());

                                Console.Write("\nEnter Quantity: ");
                                int ExtraQuantity = Convert.ToInt32(Console.ReadLine());

                                switch (ExtraItem)
                                {
                                    case 1: 
                                    {
                                        addExtraItem("EXTRA KETCHUP", ExtraQuantity);
                                        break;
                                    }
                                    case 2:
                                    {
                                        addExtraItem("EXTRA GRAVY", ExtraQuantity);
                                        break;
                                    }
                                    case 3:
                                    {
                                        addExtraItem("EXTRA PLAIN RICE", ExtraQuantity);
                                        break;
                                    }
                                    case 4:
                                    {
                                        addExtraItem("EXTRA JAVA RICE", ExtraQuantity);
                                        break;
                                    }
                                    case 5: {
                                        computeOrder();
                                        repeat = false;
                                        break;
                                    }
                                }

                                Console.Write("\nAdd more extra/add-ons? Y/N: ");
                                string moreExtra = Console.ReadLine();

                                if (moreExtra == "Y" || moreExtra == "y")
                                {
                                    extraItemRepeat = true;
                                } else if(moreExtra == "N" || moreExtra == "n")
                                {
                                    repeat = false;
                                    extraItemRepeat = false;
                                    computeOrder();
                                } else
                                {
                                    Console.WriteLine("Please enter a valid input.");
                                    extraItemRepeat = true;
                                }
                            }
                        }else if(addExtra == "N" || addExtra == "n")
                        {
                            computeOrder();
                            repeat = false;
                        }

                    } else if (ITEM_ADD == "Y" || ITEM_ADD == "y")
                    {
                        repeat = true;
                    }
                    else
                    {
                        Console.WriteLine("Enter a valid response.");
                    }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"\nError! Please enter a valid quantity.\nError: {e.Message}");
            }
        }
    }

    public void addItem(string itemName, int itemQuantity)
    {
        string itemN = itemName.ToUpper();

        Console.WriteLine($"{itemQuantity} {itemN} added.");

        OrderedItem.Add(new OrderItem(itemN, itemQuantity, false));
    }

    public void addExtraItem(string itemName, int itemQuantity)
    {
        string ExtraItemN = itemName.ToUpper();

        Console.WriteLine($"{itemQuantity} {ExtraItemN} added.");

        OrderedItem.Add(new OrderItem(ExtraItemN, itemQuantity, true));
    }
}

class FoodMenu
{
    public static void Main()
    {
        Menu system = new Menu();
        Order order = new Order();

        bool loop = true;
            while (loop)
        {
            try
            {
                Console.WriteLine("\n★====================✦===================★\n           KOPI BAR           \n★====================✦===================★\n");
                Console.WriteLine("[1] Menu\n[2] New Order\n[3] View Order\n[4] Update Order\n[5] Cancel Item\n[6] Cancel Entire Order\n[7] Checkout\n[8] Order History\n[9] Exit\n");
                Console.WriteLine("\n★====================✦=================== ★========================================★ ====================✦===================★\n");
                Console.Write("Enter Choice: ");
                int CASHIER_OPTION = Convert.ToInt32(Console.ReadLine());

                switch (CASHIER_OPTION)
                {
                    case 1:
                        {
                            system.viewMenu();
                            break;
                        }
                    case 2:
                        {
                            order.newOrder();
                            break;
                        }
                    case 3:
                        {
                            order.viewOrder();
                            break;
                        }
                    case 4:
                        {
                            order.updateOrder();
                            break;
                        }
                    case 5:
                        {
                            order.cancelItem();
                            break;
                        }
                    case 6:
                        {
                            order.cancelEntireOrder();
                            break;
                        }
                    case 7:
                        {
                            order.checkout();
                            break;
                        }
                    case 8:
                        {
                            order.OrderHistory();
                            break;
                        }
                    case 9:
                        {
                            loop = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Something went wrong.");
                            Console.WriteLine("Try again");
                            loop = true;
                            break;
                        }
                }
            } catch (FormatException e)
            {
                Console.WriteLine($"\n{e.Message}");
            }
        }
    }
}