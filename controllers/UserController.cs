using System;
using System.Collections.Generic;
using programming_project.model;

namespace programming_project.controllers
{
    class UserController{
        
        public void init(){
          Console.WriteLine("Initialization project");
          getUsers();
        }

        public static void getUsers(){
          /* Instace of UserModel to get properties */
          UserModel user = new UserModel();

          /* The list is created to be able to add objects */
          List<UserModel> userList = new List<UserModel>();
          userList.Add(user);
        }
    }
}
