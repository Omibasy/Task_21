using System.ComponentModel.DataAnnotations;
using Task_20.Model.Interface;

namespace Task_20.Model.Data
{
    public class Person : IPerson
    {
        public Person() { }

        [Key]
        public int ID { get; set; }

        [StringLength(20)]
        public string Surname { get; set; }


        [StringLength(20)]
        public string Name { get; set; }


        [StringLength(20)]
        public string Patomic { get; set; }
    }
}
