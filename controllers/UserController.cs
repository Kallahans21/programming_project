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
          UserModel user = new UserModel();
          List<UserModel> userList = new List<UserModel>();
          userList.Add(user);
        }
    }
}
