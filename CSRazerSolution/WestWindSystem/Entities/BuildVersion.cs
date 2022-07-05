using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace WestWindSystem.Entities
{

    //  we need to point this entity definition to the sql table that it represents.
    //  in sql nvarchar(), varchar, nchar, char is declared as a STRING in your ENTITY definition
    //  to do this, we use an annotation.
    //  annotations are placed IMMEDIATELY before the item in the definition it refers to.

    [Table("BuildVersion")]
    
    public class BuildVersion
    {
        //  in sql nvarchar(), varchar, nchar, char is declared as a STRING in your ENTITY definition
        //  sql BIT IS A BOOL IN c#

        //   names of class properties DO NOT need to match the attribute name on your AQL table (entity)
        //  HOWEVER, if you use different names then ORDER is IMPORTANT in this entity class, It MUST match the order in sql table.
        //  IF YOUR PROPERTY names match then the order within this class is not important. HOWEVER, it is much easier to match your sql table to your entity if you maintain the same order fir data.
        //  annotation to indicate the primary key / property relationship
        //  3 syntax forms for the pkey annotation


        //  IDENTITY() pkey in sql
        //  a) Key("nameOfSQLAttribute")
         
        //  Your sql pkey is NOT a IDENTITY() pkey in sql
        //  b) Key("nameOfSQLAttribute",DatabaseGeneratedOptions(DatabaseGeneratedOption.None))

        //  Your sql pkey is a compound pkey in sql
        //  YOu will need to add the annotation in front of each part of the compound key attribute / property to form the correct pkey structure
        //  c)
        //  [Key]
        //  [Column(Order=n)]

        //  if you have a foreign and your attribute / property names are the same
        //  the system will already known about the fkey relationship; therefore you DO NOT use the annotation [ForeignKey]
        [Key]
        
        public int Id { get; set; }

        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public DateTime ReleaseDate { get; set; }

        // you can create a property within your entity that is NOT a data attribute in your sql table.
        //  if you do, use the [NotMapped} annotation

        //  Example
        //  assume you two separate properties FirstName and LastName you wish to force the program to consistently concatenate the properties in their code you wish to make it easier for the programmer by doing the concatenation for them
        //  create a read-only property (just a get) and return the concatenation
        //  the program can then use the property to concatenate the two names in the code
        //  The system realizes that this is NOT a database field.

        //  [NotMapped}
        //  public string FullName { get { return FirstName + " " + LastName; } }
    }
}
