// See https://aka.ms/new-console-template for more information
//  this class is by default in the namespace of the project: OOPsReview


//  Console is a static class
//  you CANNOT create your own instance of a static class

//  Console.WriteLine("Hello, World!");

//  an INSTANCE CLASS needs to be created using the NEW COMMAND and the  CLASS CONSTRUCTOR
//  one needs to declare a variable of class datatype: ex. Employment

//  using "Using Statement" means that one does NOT need to fully qualify on EACH usage of class
using OOPsReview.Data;

//  a "Fully Qualified Reference" to Employment
//  consists of namespace.classname
//  Example: OOPsReview.Data.Employment myEmp = new Employment("Level 5 Programmer", SupervisoryLevel.Supervisor, 10);

Employment myEmp = new Employment("Level 5 Programmer", SupervisoryLevel.Supervisor, 10);    //  default constructor
Console.WriteLine(myEmp.ToString());    //  use the instance name to reference items within your class
Console.WriteLine($"{myEmp.Title}, {myEmp.Level}, {myEmp.Years}");

myEmp.SetEmployeeResponsibilityLevel(SupervisoryLevel.DepartmentHead);  //  method

Console.WriteLine(myEmp.ToString());

//  testing     (simulate a Unit test)
//  Arrange     (setup of your test data)

Employment Job = null;    //  instance of Job

//  passing a reference variable to a method
//  a class is a reference data store
//  this passes the actual memoryaddress of the data store to the method
//  ANY changes done to the data store within the method WILL BE reflected
//      in the data store WHEN you return from the method

CreateJob(ref Job);     //  add reference here in argument
Console.WriteLine(Job.ToString());

//  passing value arguments to a Method AND receiving a value result back from the method
//  struct is a value data store
ResidentAddress Address = CreateAddress();
Console.WriteLine(Address.ToString());

//  Act     (execution of the test you wish to perform)
//  test that we can create a Person (composite instance)
Person me = null;   //  a variable capable of holding a Person instance
me = CreatePerson(Job, Address);

//  Access  (check your results)
//      Address is a struct so we need the ".ToString"
Console.WriteLine($"{me.FirstName} {me.LastName} lives at {me.Address.ToString()}" + $" having a job count of {me.NumberOfPositions}");

//  Dispalying all Jobs Start
Console.WriteLine("\nJobs:\n");

//  foreach
/*foreach (var item in me.EmploymentPositions)
{
    if (item.Years > 0)
        Console.WriteLine(item.ToString());
}*/

for(int i = 0; i < me.EmploymentPositions.Count; i++)
{
    Console.WriteLine(me.EmploymentPositions[i].ToString());
}

//  Dispalying all Jobs End

void CreateJob(ref Employment job)      //  add reference here in parameter
{
    //  since the class MAY throw exceptions, you should have user friendly  error handling
    try
    {
        job = new Employment(); //  default constructor;    new command takes a constructor as it's reference
        //  BECAUSE my properties have public set (mutators), I can "set" the value of the property
        //      from directly from the driver program
        job.Title = "Boss";
        job.Level = SupervisoryLevel.Owner;
        job.Years = 25.5;
        //  OR
        //  use the greedy constructor and set everything up all at once
        //  job = new Employment("Boss", SupervisoryLevel.Owner, 25.5);
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

//  create method for ResidentAddress
ResidentAddress CreateAddress()
{
    //  greedy constructor
    ResidentAddress address = new ResidentAddress(10706, "106 St", "", "", "Edmonton", "AB");

    return address;
}

//  create a person
Person CreatePerson(Employment job, ResidentAddress address)
{
    //Person me = new Person("Chris", "Bana", address, null);

    //  one could add the job(s) to the instance of Person (me) after the instance
    //      is created via the behaviour AddEmployment(Employment emp)
    //      me.AddEmployment(job)

    //  OR

    //  one could create a List<T> and add to the List<T> before creating the Person instance
    List<Employment> employments = new List<Employment>();
    employments.Add(job);   //  add an element to the List<T>
    Person me = new Person("Christopehr", "Bana", address, employments);  //  using the greedy constructor

    //  create additional job and load to Person
    Employment employment = new Employment("New Hire", SupervisoryLevel.Entry, 0.5);
    me.AddEmployment(employment);

    employment = new Employment("Team Head", SupervisoryLevel.TeamLeader, 5.2);
    me.AddEmployment(employment);

    employment = new Employment("Department IT Head", SupervisoryLevel.DepartmentHead, 6.8);
    me.AddEmployment(employment);
    return me;
}