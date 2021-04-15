using System;
using System.Collections.Generic;
using programming_project.model;
using System.IO;
using System.Threading.Tasks;

namespace programming_project.controllers
{
  class UserController
  {

    private static string[] userInformation = new string[2];

    public void init()
    {
      register();
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
      /* Creation of array yo get user data */
      string[] userData = new string[2];
      string[] text = System.IO.File.ReadAllLines(@"database\user.txt");

      bool authtenticated = false;
      /* Validate if the information is correct */
      do
      {
        Console.WriteLine("Enter username");
        userData[0] = Console.ReadLine();
        Console.WriteLine("Enter password");
        userData[1] = Console.ReadLine();

        foreach (var item in text)
        {
          if (item.Split('|')[0] == userData[0] && item.Split('|')[1] == userData[1])
          {
            userInformation[0] = item.Split('|')[0];
            authtenticated = true;
          }
        }

      } while (!authtenticated);

      /* if the information is correct, go to the next method */
      Console.WriteLine($"Tu usuario es {userInformation[0]}");
    }

    private static async void register()
    {
      string[] userData = new string[1];
      Console.WriteLine("Enter username");
      userData[0] += Console.ReadLine();
      Console.WriteLine("Enter password");
      userData[0] += $"|{Console.ReadLine()}";

      await File.AppendAllLinesAsync(@"database\user.txt", userData);
    }
  }
}