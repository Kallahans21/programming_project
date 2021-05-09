using programming_project.model;
using programming_project.utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace programming_project.controllers
{
    class UserController
    {

        private static string[] userInformation = new string[2];
        private static bool isInvalidLogin = false;

        public void init()
        {
            /* The application is initialized with the auth */
            CreateFolder();
            auth();
        }

        public static void CreateFolder()
        {
            /*If folder doesn't exist, create new folder. If it exists, don't*/
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (Directory.Exists(path + "programming_project") == false)
                {
                    System.IO.Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "programming_project"));
                }

                System.IO.File.AppendAllText($"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "programming_project")}/user.txt", null);
                System.IO.File.AppendAllText($"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "programming_project")}/database.txt", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        /* Authtentication function | method */
        private static void auth()
        {
            /* The console is cleaned before any process */
            Console.Clear();
            /* A menu is displayed with the actions to be executed */
            Console.WriteLine("Que accion desea realizar: ");
            Console.WriteLine("1) Iniciar sesión.");
            Console.WriteLine("2) Registrar usuario.");

            /* If the login is invalid, an error message is displayed */
            if (isInvalidLogin)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ingresa un número valido");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
            }

            /* It is redirected to the corresponding method according to the selected option */
            int selected = Convert.ToInt32(Console.ReadLine());
            switch (selected)
            {
                case 1:
                    login();
                    break;
                case 2:
                    register();
                    break;
                default:
                    isInvalidLogin = true;
                    auth();
                    break;
            }
        }

        private static void login()
        {
            ///*Create folder and txt if user chose to login first*/
            //CreateFolder();
            /* Creation of array yo get user data */
            string[] userData = new string[2];

            string[] text = System.IO.File.ReadAllLines($"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "programming_project")}/user.txt");



            bool authtenticated = false, incorrectData = false;
            /* Validate if the information is correct */
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("- Iniciar sesión -");
                Console.ForegroundColor = ConsoleColor.White;

                /* If the login is invalid, an error message is displayed */
                if (incorrectData)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Usuario y/o contraseña invalida");
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                /* Option to return to the previous menu */
                Console.Write("¿Deseas regresar? (y/n)");
                switch (Console.ReadLine())
                {
                    case "y":
                        auth();
                        break;
                }

                /* The data is requested to be validated in the .txt file */
                Console.WriteLine("");
                Console.WriteLine("Ingresa tu usuario");
                userData[0] = Console.ReadLine();
                Console.WriteLine("Ingresa tu contraseña");
                userData[1] = Console.ReadLine();
                incorrectData = true;
                foreach (var item in text)
                {
                    if (item.Split('|')[0] == userData[0] && item.Split('|')[1] == userData[1])
                    {
                        userInformation[0] = item.Split('|')[0];
                        authtenticated = true;
                        incorrectData = false;
                    }
                }

            } while (!authtenticated);
            sesion();
        }

        private static void sesion()
        {
            Console.Clear();

            /* The globally logged in session is displayed */
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"¡Sesión iniciada como: {userInformation[0]}!");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Que accion desea realizar: ");
            Console.WriteLine("1) Agregar cliente.");
            Console.WriteLine("2) Mostrar todos los clientes.");
            Console.WriteLine("3) Buscar cliente.");
            Console.WriteLine("4) Regresar.");

            /* It is redirected to the corresponding method according to the selected option */
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    createUser();
                    break;
                case 2:
                    get();
                    break;
                case 3:
                    searchClient();
                    break;
                case 4:
                    auth();
                    break;
            }
        }

        private static void createUser()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"¡Sesión iniciada como: {userInformation[0]}!");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Ingresar nuevo cliente");
            Console.WriteLine("");
            string[] customerInformation = new string[5]; /* Position 5 is to evaluate how many times you have entered */
            string[] userData = new string[1];
            string[] infoToGet = { "nombre", "DUI cliente", "vehículo", "costo de reparación" };
            int index = 0;
            foreach (var item in infoToGet)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Ingrese el {item}: ");
                Console.ForegroundColor = ConsoleColor.White;
                customerInformation[index] = Console.ReadLine();
                index++;
            }
            customerInformation[4] = "1";

            /* Verify user */
            string[] registers = System.IO.File.ReadAllLines($"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "programming_project")}/database.txt");
            int match = 0;
            foreach (var item in registers)
            {
                if (customerInformation[1] == item.Split('|')[1]) { match++; }
            }
            if (match > 2) { customerInformation[3] = Convert.ToString(Convert.ToDouble(customerInformation[3]) - (Convert.ToDouble(customerInformation[3]) * 5) / 100); }
            if (match > 10) { customerInformation[3] = Convert.ToString(Convert.ToDouble(customerInformation[3]) - (Convert.ToDouble(customerInformation[3]) * 10) / 100); }

            foreach (var item in customerInformation) { userData[0] += $"{item}|"; }
            userData[0] = userData[0].TrimEnd('|');

            Console.WriteLine("");
            if (match > 2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Al cliente se le aplico un ${5}% de descuento, por sus {match} vistas anteriores");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"El nuevo costo de reparación es de {Math.Round(Convert.ToDouble(customerInformation[3]) * 0.95, 2)}");
            }
            else if (match > 10)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Al cliente se le aplico un ${10}% de descuento, por sus {match} vistas anteriores");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"El nuevo costo de reparación es de {Math.Round(Convert.ToDouble(customerInformation[3]) * 0.9, 2)}");
            }

            System.IO.File.AppendAllLines($"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "programming_project")}/database.txt", userData);

            Console.WriteLine("");
            Console.Write("¿Deseas agregar un cliente de nuevo? (y/n)");
            switch (Console.ReadLine())
            {
                case "y":
                    createUser();
                    break;
            }
            sesion();
        }

        private static void register()
        {
            Console.Clear();
            string[] userData = new string[1];
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("- Registrarte -");
            Console.ForegroundColor = ConsoleColor.White;

            /* Option to return to the previous menu */
            Console.Write("¿Deseas regresar? (y/n)");
            switch (Console.ReadLine())
            {
                case "y":
                    auth();
                    break;
            }

            /* The data is requested to be able to be registered in the .txt file */
            Console.WriteLine("");
            Console.WriteLine("Ingresa tu usuario");
            userData[0] += Console.ReadLine();
            Console.WriteLine("Ingresa tu clave");
            userData[0] += $"|{Console.ReadLine()}";
            System.IO.File.AppendAllLines($"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "programming_project")}/user.txt", userData);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Se creo el usuario correctamente, presiona cualquie tecla para iniciar sesión con tu nuevo usuario");
            Console.ForegroundColor = ConsoleColor.White;


            Console.ReadKey();
            login();
        }

        private static void get()
        {
            string[] data = System.IO.File.ReadAllLines($"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "programming_project")}/database.txt");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"¡Sesión iniciada como: {userInformation[0]}!");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Mostrando {data.Length} clientes");
            Console.WriteLine("");

            Table table = new Table();
            table.PrintLine();
            table.PrintRow("Nombre cliente", "DUI", "Vehiculo", "Reparación");
            table.PrintLine();
            foreach (var item in data)
            {
                table.PrintRow(item.Split("|")[0], item.Split("|")[1], item.Split("|")[2], item.Split("|")[3]);
                table.PrintLine();
            }
            Console.WriteLine("");
            Console.Write("¿Deseas mostrar todos los clientes de nuevo? (y/n)");
            switch (Console.ReadLine())
            {
                case "y":
                    get();
                    break;
            }
            sesion();
        }

        private static void searchClient()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"¡Sesión iniciada como: {userInformation[0]}!");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Para buscar un cliente debes de ingresar su número de DUI");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ingrese DUI: ");
            Console.ForegroundColor = ConsoleColor.White;
            string client = Console.ReadLine(), clienReceive = null;
            string[] data = System.IO.File.ReadAllLines($"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "programming_project")}/database.txt");
            Table table = new Table();
            foreach (var item in data)
            {
                if (item.Split("|")[1] == client)
                {
                    table.PrintLine();
                    table.PrintRow("Nombre cliente", "DUI", "Vehiculo", "Reparación");
                    table.PrintLine();
                    table.PrintRow(item.Split("|")[0], item.Split("|")[1], item.Split("|")[2], item.Split("|")[3]);
                    table.PrintLine();
                    clienReceive = item;
                }
            }
            if (string.IsNullOrEmpty(clienReceive))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se encontro ningun cliente con este DUI");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("");
            Console.Write("¿Deseas buscar otro cliente? (y/n)");
            switch (Console.ReadLine())
            {
                case "y":
                    searchClient();
                    break;
            }
            sesion();
        }

    }
}