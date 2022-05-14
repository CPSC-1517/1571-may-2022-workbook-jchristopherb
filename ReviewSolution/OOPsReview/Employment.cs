using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview.Data
{
    public class Employment
    {
        //  An instance of this class will hold data about a person's employment
        //  The code of this class is the definition of that data
        //  The characteristics (data) of the class will be:
        //  Title, SupervisoryLevel, Years Of Employment within the company

        //  There are 4 components of a class definition:
        //  1. Data Fields (data members)
        //  2. Property
        //  3. Constructor
        //  4. Behaviour (method)

        //  Data Fields
        //      are storage areas in your class
        //      these are treated as variables
        //      these ,ay be public, private, public readonly

        private string _Title;          //  where the storage of data; the declared storage data area
        private double _Years;

        //  Property
        //      these are access techniques to retrieve or set data in your class without touching the storage data field

        //  A property is associated wuth a single instance of data
        //  A property is public so it can be access by an outside user of class
        //  A property MUST have a GET
        //  A property MAY have a SET
        //      if no SET, the property is not changeable by the user; readonly.
        //      commonly used for calculated data of the class
        //      the SET can be either public or private
        //          public: the user can alter the contents of the data
        //          private: only code within the class can alter the contents of the

        //  FULLY IMPLEMENTED PROPERTY
        //      a) a declared storage data area (data field)
        //      b) a declared property signature (access return data type and property name)
        //      c) a coded accessor (GET) coding block : public
        //      d) an optional coded mutator (SET) coding block: can be public or private
        //          **if the set is private, the only way to place data into this property is via the constructor or a behavioour (method)

        //  When:
        //      a) if you are storing the associate data in an explicitly declared data field
        //      b) if you are doing validation on incoming data
        //      c) creating a property that generates output from other data sources within the class (readonly property) ; this property would ONLY have an accessor (GET) 

        //  WE USE FULLY IMPLEMENTED PROPERTY IF WE NEED TO HAVE A VALIDATION !!!!!!


        public string Title             //  property
        {
            //  a property is associated with a single piece of data
            get
            {
                // accessor
                // the GET "coding block" will return the contents of a data field(s)
                // the return has syntax of RETURN expression
                return _Title;          // return the declared storage data
            }
            set
            {
                // mutator
                // the SET "coding block" receives an incoming value and places into the associated data fields
                // during the setting, you MIGHT wish to validate the incoming data
                // during the setting, you MIGHT wish to do some type of logical processing using the data to set another field
                // the incoming piece of data is refered to using the keyword "value"

                // ensure that the incoming data is not null or empty or just whitespace
                // if (string.IsNullOrWhiteSpace(value)) - catches all three cases ^

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Title is a required piece of data.");
                }

                //  data is considered valid.
                //  value will be stored to the data storage
                _Title = value;
            }
        }

        //  AUTO IMPLEMENTED PROPERTY
        //      these properties differ only in syntax
        //      each property is responsible for a single piece of data
        //      these properties do NOT reference a declared private data member
        //      the system generates an internal storage area of the return data type
        //      the system manages the internal storage for the accessor and mutator
        //      THERE IS NO ADDITIONAL LOGIC APPLIED TO THE DATA VALUE!!!!!!!!!!!!

        //  using an enum for this field will automatically restrict the data values this property can contain

        //  SYNTAX: accessType returnDataType propertyName {get; [private] set;}

        public SupervisoryLevel Level { get; set; }

        public double Years
        { 
            get { return _Years; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"Years value {value} is invalid. Must be 0 or greater");
                }

                _Years = value;
            }
        }

        //  CONSTRUCTOR
        //      this is used to initialize the physical object (instance) during its creation.
        //      the result of creation is to ensure that the coder gets an instance in a "known state" - you can guarantee that objects in this will meet the specification of the objects
        //      if your class definition has NO constructor coded, the data members AND/OR auto implemented properties are set to the C# default data type value

        //      you can code one or more constructors in your class definition
        //      IF YOU CODE A CONSTRUCTOR FOR THE CLASS, YOU ARE RESPONSIBLE FOR ALL CONSTRUCTORS USED BY THE CLASS !!!
        //      generally, if you are going to code your own constructor(s) you code two types;
        //          Default:    this constructor does NOT take in any parameters
        //                      this constructor mimics the default system constructor
        //                      ()
        //          Greedy:     this constructor has a list if parameters, one for each property, declare for incoming data
        //                      (a,b,c,d)

        //  SYNTAX: accessType className([list of parameters]) {constructor code block}

        //  IMPORTANT:  The constructore DOES NOT have a return dataType
        //              You DO NOT call a constructor directly, it is called using the ** new command => new className(...); **

        //  DEFAULT CONSTRUCTOR:

        public Employment()
        { 
            //  constructor body
            //  a) can be empty: values will be set to C# defaults
            //  b) you COULD assign literal values to your properties with this constructor

            //  the values that you give your class data members/properties CAN be assigned directly to a data member.
            //  HOWEVER:    if you have a validated properties, you SHOULD consider saving your data values via the property

            //  You CAN code your validation logic within your constructors BECAUSE objects run your constructor when it is created
            //  Placing your logic in the constructor could be done if your property has a private set OR if your public data member is a READONLY data member
            //  Private SETs and READONLY data members CAN NOT have their data altered directly
            Level = SupervisoryLevel.TeamMember;
            Title = "Unknown";
        }


        //  GREEDY CONSTRUCTOR:
        public Employment(string title, SupervisoryLevel level, double years)
        {

            //  constructor body
            //      a) a parameter for each property
            //      b) you COULD code your validation in this constructor
            //      c) validation for public readonly data members
            //      d) validation for properties with a private set MUST be done here if not done in the property

            Title = title;
            Level = level;
            Years = years;  //  eventually the data will be placed in _Years
        }
    }
}
