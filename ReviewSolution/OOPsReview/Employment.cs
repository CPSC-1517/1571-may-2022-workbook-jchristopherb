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

        public string Title
        { 
            get;
            set;
        }
    }
}
