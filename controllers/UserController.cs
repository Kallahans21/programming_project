using System;
using System.Collections.Generic;
using programming_project.model;

namespace programming_project.controllers
{
    class UserController{
        
        public void init(){
          auth();
        }

        public static void getUsers(){
          /* Instace of UserModel to get properties */
          UserModel user = new UserModel();

          /* The list is created to be able to add objects */
          List<UserModel> userList = new List<UserModel>();
          userList.Add(user);
        }

        /* Authtentication function | method */
        private static void auth(){
          /* Test data */
          string[] data = {"josue", "1234"};

          string[] userData = new string[2];
          
          do
          { 
              Console.WriteLine("Enter the user");
              userData[0] = Console.ReadLine();
              Console.WriteLine("Enter the password");
              userData[1] = Console.ReadLine();

          } while (data[0] != userData[0] || data[1] != userData[1]);

          test();
        }

        private static void test(){
          Console.WriteLine("Te logeaste correctamente");
        }
    }
}
