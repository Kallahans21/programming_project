using System.Collections.Generic;
using System;
using programming_project.controllers;

namespace programming_project
{
    class Program
    {
        static void Main(string[] args)
        {
            initialization();
        }

        static void initialization(){
            UserController user = new UserController();
            user.init();
        }
    }
}
