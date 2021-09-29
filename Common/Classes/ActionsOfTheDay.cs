using System.ComponentModel.DataAnnotations;

namespace Common.Classes
{
    public class ActionsOfTheDay
    {
        [Key]
        public int Actions_Id { get; set; }

        [MaxLength(150), Required]
        public string Name { get; set; }
    }
}
