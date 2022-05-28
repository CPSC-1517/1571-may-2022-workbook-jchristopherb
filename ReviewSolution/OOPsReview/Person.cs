using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview.Data
{
    public class Person
    {
        //  example of a composite class
        //  a composite class uses other classes/structs in its definition
        //  a composite class is recognize with the phrase "has a" class
        //  Examples:
        //  this class of Person "has a" redisent address
        //  this class has a List<T> where <T> represents a datatype and in this class <T> is a
        //      collection of Employment instances

        //  review video on inheritance and composition

        //  each instance of this class will represent an individual
        //  this class will define the following characteristics of a person
        // First Name, Last Name,  the current resident address, list of positions


        private string _FirstName;
        private string _LastName;

        public string FirstName
        {
            get { return _FirstName; }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("First name is required.");
                }
                _FirstName = value;
            }
        }
        public string LastName
        {
            get { return _LastName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Last name is required.");
                }
                _LastName = value;
            }
        }

        //  composition actually uses the other struct/class as a property/field within 
        //      the definition of the class being specified (created)
        //  in this example, Address is a field (data member)

        //  this field is NOT a property
        //  the data type is a developer defined data type (struct)

        public ResidentAddress Address;

        public List<Employment> EmploymentPositions { get; private set; }

        //  this property will compile cleanly
        //  this property will return a value IF EmploymentPositions has an instace of List<T>
        //  this property WILL ABORT IF EmploymentPositions has NOT be set to an insatnce of List<T>

        public int NumberOfPositions { get { return EmploymentPositions.Count; } }

        /* // Option 1
         * 
         * public Person()
        {
            //  the system will automatically assign default system values
            //      to our datamembers accordingly to their datatype
            //  strings -> null
            //  objects -> null
            //  firstname and lastname has validation coiding a null value

            FirstName = "Unknown";
            LastName = "Unknown";

            //  if one tried to reference an instance's data and the instance is null
            //      THEN one would get a NULL EXCEPTION
            //  even though you have no instances to store, you will at least have
            //      some place to put the data ONCE it is supplied

            EmploymentPositions = new List<Employment>();
        }*/

        //  Option 2
        //  DO not code a "Default" constructor
        //  Code ONLY the "Greedy" constructor
        //  if only a greedy constructor exists for the class, the ONLY way to possibly
        //      create an instance for the class within the program would be to use the
        //      constructor when the class instance is created

        public Person(string firstname, string lastnmae, ResidentAddress address, List<Employment> employmentpositions)
        {
            FirstName = firstname;
            LastName = lastnmae;
            Address = address;
            if (employmentpositions != null)
                EmploymentPositions = employmentpositions;
            else
                //  this will allow a NULL parameter value and the class to have an empty List<T>
                EmploymentPositions = new List<Employment>();
        }

        public void ChangeName(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }

        public void AddEmployment(Employment employment)
        {
            if (employment == null)
            {
                throw new ArgumentNullException("You muct supply an employment record for it to be added to this person");
            }

            EmploymentPositions.Add(employment);
        }

    }
}
