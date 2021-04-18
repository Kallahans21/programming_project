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
      /* auth(); */
      get();
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
      Console.Clear();
      Console.WriteLine("Que accion desea realizar: ");
      Console.WriteLine("1) Iniciar sesión.");
      Console.WriteLine("2) Registrar usuario.");

      if (isInvalidLogin)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Ingresa un número valido");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.White;
      }

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
        if (incorrectData)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("Usuario y/o contraseña invalida");
          Console.WriteLine("");
          Console.ForegroundColor = ConsoleColor.White;
        }
        Console.Write("¿Deseas regresar? (y/n)");
        switch (Console.ReadLine())
        {
          case "y":
            auth();
            break;
        }
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
      prueba();
    }

    private static void prueba()
    {
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Credenciales correctas");
      Console.ForegroundColor = ConsoleColor.White;
      Console.ReadLine();
    }

    private static void register()
    {
      Console.Clear();
      string[] userData = new string[1];
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("- Registrarte -");
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("¿Deseas regresar? (y/n)");
      switch (Console.ReadLine())
      {
        case "y":
          auth();
          break;
      }
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
      Table table = new Table();
      table.PrintLine();
      Console.ForegroundColor = ConsoleColor.Green;
      table.PrintRow("Nombre cliente", "DUI", "Vehiculo", "Reparación");
      Console.ForegroundColor = ConsoleColor.White;
      table.PrintLine();
      foreach (var item in data)
      {
        table.PrintRow(item.Split("|")[0], item.Split("|")[1], item.Split("|")[2], item.Split("|")[3]);
        table.PrintLine();
      }
    }
  }
}