using System;
using System.Collections.Generic;
using programming_project.model;
using programming_project.utils;

namespace programming_project.controllers
{
  class UserController
  {

    private static string[] userInformation = new string[2];
    private static bool isInvalidLogin = false;

    public void init()
    {
      /* The application is initialized with the auth */
      auth();
    }

    public static void getUsers()
    {
      /* Instace of UserModel to get properties */
      UserModel user = new UserModel();

      /* The list is created to be able to add objects */
      List<UserModel> userList = new List<UserModel>();
      userList.Add(user);
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
      /* Creation of array yo get user data */
      string[] userData = new string[2];
      string[] text = System.IO.File.ReadAllLines(@"database\user.txt");

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

      /* It is redirected to the corresponding method according to the selected option */
      switch (Convert.ToInt32(Console.ReadLine()))
      {
        case 1:
          /* login(); */
          break;
        case 2:
          get();
          break;
        case 3:
          searchClient();
          break;
      }
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

      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Se creo el usuario correctamente, presiona cualquie tecla para iniciar sesión con tu nuevo usuario");
      Console.ForegroundColor = ConsoleColor.White;
      System.IO.File.AppendAllLines(@"database\user.txt", userData);
      Console.ReadKey();
      login();
    }

    private static void get()
    {
      string[] data = System.IO.File.ReadAllLines(@"database\database.txt");
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
      string[] data = System.IO.File.ReadAllLines(@"database\database.txt");
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