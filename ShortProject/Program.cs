using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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
        static User user = new User();
        static void Main(string[] args)
        {








        Register:

            Console.WriteLine("Wellcome");
            Console.WriteLine("Do you want to login or register?");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            Console.Write("Enter option number: ");
            string option = Console.ReadLine();


        Email:


            switch (option)
            {
                case "1":
                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();

                    try
                    {
                        User loggedInUser = userService.Login(email, password);
                        Console.Clear();
                        Console.WriteLine($"Welcome, {loggedInUser.Fullname}!");


                        DisplayMenu();
                    }
                    catch (NotFoundException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        goto Register;
                    }
                    
                    break;
                case "2":
                    PerformRegistration();
                    goto Register;
                case "3":
                    Console.WriteLine("Goodbye");
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    goto Register;
            }


            //static void PerformLogin()
            //{

            //    Console.Write("Email: ");
            //    string email = Console.ReadLine();
            //    Console.Write("Password: ");
            //    string password = Console.ReadLine();

            //    try
            //    {
            //        User loggedInUser = userService.Login(email, password);
            //        Console.WriteLine($"Welcome, {loggedInUser.Fullname}!");


            //        DisplayMenu();
            //    }
            //    catch (NotFoundException ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        continue;
            //    }

            //}

            static void PerformRegistration()
            {
                Console.Write("Fullname: ");
                string fullname = Console.ReadLine();
                Regex regexname = new Regex(@"^[A-Za-z\- ]+$");
                while (!regexname.IsMatch(fullname))
                {
                    Console.WriteLine("Please enter a valid name.");
                    fullname = Console.ReadLine();
                }


                Console.Write("Email: ");
                string email = Console.ReadLine();
                Regex regexemail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                while (!regexemail.IsMatch(email))
                {
                    Console.WriteLine("Please enter a valid email.");
                    email = Console.ReadLine();
                }


                Console.Write("Password: ");
                string password = Console.ReadLine();
                Regex regexpassword = new Regex(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$");
                while (!regexpassword.IsMatch(password))
                {
                    Console.WriteLine("Please enter a valid password.");
                    password = Console.ReadLine();
                }

                try
                {
                    userService.Register(fullname, email, password);
                    Console.Clear();
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
            Menu:
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

                    try
                    {



                        switch (input)
                        {
                            case "1":
                                ViewAllMedicines();
                                goto Menu;

                            case "2":
                                AddNewMedicine();

                                goto Menu;
                            case "3":
                                UpdateMedicine();
                                goto Menu;
                            case "4":
                                RemoveMedicine();
                                goto Menu;
                            case "5":
                                ViewAllCategories();
                                goto Menu;
                            case "6":
                                AddNewCategory();
                                goto Menu;
                            case "7":
                                Console.WriteLine("Goodbye");
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Invalid option. Please enter a number from 1 to 7.");
                                goto Menu;
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto Menu;
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
            addmdn:
                Medicine newMedicine = new Medicine();
                Console.Write("Enter medicine name: ");
                newMedicine.Name = Console.ReadLine();
                if (userService.NoSpace(newMedicine.Name))
                {
                    Console.WriteLine("Please enter a valid name.");
                    goto addmdn;
                }
                reEnterPrice:
                Console.Write("Enter price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Please enter a valid price.");
                    goto reEnterPrice;

                }
                newMedicine.Price = price;
                Console.Write("Enter category ID: ");
                //if (int.TryParse(Console.ReadLine(), out int categoryId))
                //{
                //    newMedicine.CategoryId = categoryId;
                //}
                int categoryId;
                while (!int.TryParse(Console.ReadLine(), out categoryId))
                {
                    Console.WriteLine("Please valid category Id");

                }

                newMedicine.CategoryId = categoryId;    


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
            ctgry:
                Category newCategory = new Category();
                Console.Write("Enter category name: ");
                newCategory.Name = Console.ReadLine();
                if (userService.NoSpace(newCategory.Name))
                {
                    Console.WriteLine("Please enter a valid category.");
                    goto ctgry;
                }
                categoryService.CreateCategory(newCategory);
                Console.WriteLine("Category added successfully.");
            }
        }

    }
}
