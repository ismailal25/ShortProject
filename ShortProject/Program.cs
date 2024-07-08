using ShortProject.Exceptions;
using ShortProject.Models;
using ShortProject.Services;

namespace ShortProject
{
    public class Program
    {
        static UserService userService = new UserService();
        static CategoryService categoryService = new CategoryService();
        static MedicineService medicineService = new MedicineService();
        static void Main(string[] args)
        {



            // Add at least one user
            User initialUser = new User { Id = 1, Fullname = "Ismayil Aliyev", Email = "isoali@gmail.com", Password = "isoali123" };
            userService.AddUser(initialUser);

            // Login or Register user
            Console.WriteLine("Do you want to login or register?");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.Write("Enter option number: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    PerformLogin();
                    break;
                case "2":
                    PerformRegistration();
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }


            static void PerformLogin()
            {
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                try
                {
                    User loggedInUser = userService.Login(email, password);
                    Console.WriteLine($"Welcome, {loggedInUser.Fullname}!");

                   
                    DisplayMenu();
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            static void PerformRegistration()
            {
                Console.Write("Fullname: ");
                string fullname = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                try
                {
                    userService.Register(fullname, email, password);
                    Console.WriteLine("Registration successful. You can now login.");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            static void DisplayMenu()
            {
                bool exit = false;

                while (!exit)
                {
                    Console.WriteLine();
                    Console.WriteLine("Menu options:");
                    Console.WriteLine("1. View all medicines");
                    Console.WriteLine("2. Add a new medicine");
                    Console.WriteLine("3. Update a medicine");
                    Console.WriteLine("4. Remove a medicine");
                    Console.WriteLine("5. View all categories");
                    Console.WriteLine("6. Add a new category");
                    Console.WriteLine("7. Exit");
                    Console.Write("Enter option number: ");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            ViewAllMedicines();
                            break;
                        case "2":
                            AddNewMedicine();
                            break;
                        case "3":
                            UpdateMedicine();
                            break;
                        case "4":
                            RemoveMedicine();
                            break;
                        case "5":
                            ViewAllCategories();
                            break;
                        case "6":
                            AddNewCategory();
                            break;
                        case "7":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please enter a number from 1 to 7.");
                            break;
                    }
                }
            }

            static void ViewAllMedicines()
            {
                var medicines = medicineService.GetAllMedicines();
                foreach (var medicine in medicines)
                {
                    Console.WriteLine($"Medicine ID: {medicine.Id}, Name: {medicine.Name}, Price: {medicine.Price}, Category ID: {medicine.CategoryId}");
                }
            }

            static void AddNewMedicine()
            {
                Medicine newMedicine = new Medicine();
                Console.Write("Enter medicine name: ");
                newMedicine.Name = Console.ReadLine();
                Console.Write("Enter price: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    newMedicine.Price = price;
                }
                Console.Write("Enter category ID: ");
                if (int.TryParse(Console.ReadLine(), out int categoryId))
                {
                    newMedicine.CategoryId = categoryId;
                }
               
                medicineService.CreateMedicine(newMedicine);
                Console.WriteLine("Medicine added successfully.");
            }

            static void UpdateMedicine()
            {
                Console.Write("Enter medicine ID to update: ");
                if (int.TryParse(Console.ReadLine(), out int updateId))
                {
                    Medicine medicineToUpdate = medicineService.GetMedicineById(updateId);
                    Console.Write("Enter new name: ");
                    medicineToUpdate.Name = Console.ReadLine();
                    Console.Write("Enter new price: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                    {
                        medicineToUpdate.Price = newPrice;
                    }
                    medicineService.UpdateMedicine(updateId, medicineToUpdate);
                    Console.WriteLine("Medicine updated successfully.");
                }
            }

            static void RemoveMedicine()
            {
                Console.Write("Enter medicine ID to remove: ");
                if (int.TryParse(Console.ReadLine(), out int removeId))
                {
                    medicineService.RemoveMedicine(removeId);
                    Console.WriteLine("Medicine removed successfully.");
                }
            }

            static void ViewAllCategories()
            {
                var categories = DB.Categories;
                foreach (var category in categories)
                {
                    Console.WriteLine($"Category ID: {category.Id}, Name: {category.Name}");
                }
            }

            static void AddNewCategory()
            {
                Category newCategory = new Category();
                Console.Write("Enter category name: ");
                newCategory.Name = Console.ReadLine();
                categoryService.CreateCategory(newCategory);
                Console.WriteLine("Category added successfully.");
            }
        }

    }   }
