// See https://aka.ms/new-console-template for more information
using OOPsReview.Data;


//  Console is a static class
//  you CANNOT create your own instance of a static class

Console.WriteLine("Hello, World!");

//  an INSTANCE CLASS needs to be created using the NEW COMMAND and the  CLASS CONSTRUCTOR
//  one needs to declare a variable of datatype Employment

Employment myEmp = new Employment("Level 5 Programmer", SupervisoryLevel.Supervisor, 10);    //  defaul constructor
Console.WriteLine(myEmp.ToString());    //  use the instance name to reference items within your class

myEmp.SetEmployeeResponsibilityLevel(SupervisoryLevel.DepartmentHead);
Console.WriteLine(myEmp.ToString());
