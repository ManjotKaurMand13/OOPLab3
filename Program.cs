using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// namespace Lab3
namespace Lab3
{

    // class for Product
    class Product
    {
        // getter and setter
        public string Name { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }

        // constructor
        public Product(string name, double price, string code)
        {
            // if name is null or empty, throw exception
            try
            {
                if (name == null || name == "")
                {
                    throw new Exception("Name cannot be empty");
                }
                if (price < 0)
                {
                    throw new Exception("Price cannot be negative");
                }
                if (code == null || code == "")
                {
                    throw new Exception("Code cannot be empty");
                }
                Name = name;
                Price = price;
                Code = code;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    // class for vending machine
    class vendingMachine
    {
        // getter and setter
        public static int SerialNumber { get; set; }
        public Dictionary<int, int> MoneyFloat { get; set; }
        public Dictionary<Product, int> Inventory { get; set; }

        // declear Barcode as readonly
        public readonly string Barcode = "123456789";

        // constructor
        public vendingMachine(int serialNumber)
        {
            // if Barcode is null or empty, throw exception
            try
            {
                if (Barcode == null || Barcode == "")
                {
                    throw new Exception("Barcode cannot be empty");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            SerialNumber = serialNumber;
            MoneyFloat = new Dictionary<int, int>();
            Inventory = new Dictionary<Product, int>();
        }

        // static constructor
        static vendingMachine()
        {
            SerialNumber = 1;
            // increment the serial number each time a vending machine is created
            SerialNumber++;
        }

        // method to stock item into inventory
        public string StockItem(Product product, int quantity)
        {
            // check if the product is already in the inventory
            try
            {
                if (quantity < 0)
                {
                    throw new Exception("Quantity cannot be negative");
                }
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (Inventory.ContainsKey(product))
            {
                Inventory[product] += quantity;
            }
            else
            {
                Inventory.Add(product, quantity);
            }
            return "Product " + product.Name + " " + product.Code + " " + product.Price + " " + quantity;
        }

        // method to stock float into money float
        public string StockFloat(int moneyDenomination, int quantity)
        {
            // check if the money denomination is already in the money float
            if (MoneyFloat.ContainsKey(moneyDenomination))
            {
                MoneyFloat[moneyDenomination] += quantity;
            }
            else
            {
                MoneyFloat.Add(moneyDenomination, quantity);
            }
            return "Money " + moneyDenomination + " " + quantity;
        }

        // method to vend item from inventory
        public string VendItem(string code, List<int> money)
        {
            // check if the code is valid
            try
            {
                if (code == null || code == "")
                {
                    throw new Exception("Code cannot be empty");
                }

                if (money == null)
                {
                    throw new Exception("Money cannot be null");
                }

                if (money.Count == 0)
                {
                    throw new Exception("Money cannot be empty");
                }

                if (money.Count > 0)
                {
                    foreach (int i in money)
                    {
                        if (i < 0)
                        {
                            throw new Exception("Money cannot be negative");
                        }
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Product product = Inventory.Keys.FirstOrDefault(x => x.Code == code);
            if (product == null)
            {
                return "Error, no item with code " + code;
            }
            if (Inventory[product] == 0)
            {
                return "Error: Item is out of stock";
            }
            if (money.Sum() < product.Price)
            {
                return "Error: insufficient money provided";
            }
            double change = money.Sum() - product.Price;
            Inventory[product]--;
            return "Please enjoy your " + product.Name + " and take your change of $" + change;
        }

        // method to vend float from money float
        public string GetSerialNumber()
        {
            return "Serial Number: " + SerialNumber;
        }

        // method the barcode
        public string GetBarcode()
        {
            return "Barcode: " + Barcode;
        }

        // method of getting the inventory
        public string GetInventory()
        {
            string inventory = "";
            // check if the inventory is empty
            foreach (var item in Inventory)
            {
                inventory += "Product " + item.Key.Name + " " + item.Key.Code + " " + item.Key.Price + " " + item.Value + "\n";
            }
            return inventory;
        }

        // method of getting the money float
        public string GetMoneyFloat()
        {
            string moneyFloat = "";
            foreach (var item in MoneyFloat)
            {
                moneyFloat += "Money " + item.Key + " " + item.Value + "\n";
            }
            return moneyFloat;
        }

        // method to get total money in the machine
        public string GetTotalInventory()
        {
            int total = 0;
            foreach (var item in Inventory)
            {
                total += item.Value;
            }
            return "Total Inventory: " + total;
        }

        // main method
        static void Main(string[] args)
        {
            vendingMachine vm = new vendingMachine(1);
            vm.StockItem(new Product("Coke", 1.50, ""), 10);
            vm.StockItem(new Product("Pepsi", 1.50, "Pepsi"), 10);
            vm.StockItem(new Product("", 1.50, "Sprite"), 10);

            vm.StockFloat(1, 10);
            vm.StockFloat(5, 10);
            vm.StockFloat(10, 10);

            Console.WriteLine(vm.GetSerialNumber());
            Console.WriteLine(vm.GetBarcode());
            Console.WriteLine(vm.GetInventory());
            Console.WriteLine(vm.GetMoneyFloat());
            Console.WriteLine(vm.GetTotalInventory());
        }
    }
}


