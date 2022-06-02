using System;
using InterviewApp;

/* All changes made must be backwards compatible with this code */

var userService = new UserService();
var result = await userService.AddUser("Bob", "Roberts", "bob.roberts@email.com",
    new DateTime(2000, 03, 25), 1000);
Console.WriteLine("User added " + (result ? "successful" : "unsuccessful"));