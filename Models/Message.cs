using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models {

    public class Message {

        [Required]
        public string text { get; set; }
        public long Id { get; set; }
        public Message() {

        }

        public Message (string message) {
            text = message;
        }
    }
    
}