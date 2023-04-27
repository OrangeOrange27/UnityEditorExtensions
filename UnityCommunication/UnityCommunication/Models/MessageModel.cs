using System.ComponentModel.DataAnnotations;

namespace UnityCommunication.Models
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }
        public string? Msg { get; set; }
        public string? Token { get; set; }
    }
}
