using System;
using System.Collections.Generic;
using programming_project.model;
using System.IO;

namespace programming_project.controllers
{
  class UserController
  {

    private static string[] userInformation = new string[2];

    public void init()
    {
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
      Console.Clear();
      Console.WriteLine("Que accion desea realizar: ");
      Console.WriteLine("1) Iniciar sesión.");
      Console.WriteLine("2) Registrar usuario.");

      switch (Convert.ToInt32(Console.ReadLine()))
      {
        case 1:
          login();
          break;
        case 2:
          register();
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

      /* if the information is correct, go to the next method */
      /* Console.WriteLine($"Tu usuario es {userInformation[0]}"); */
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
  }
}