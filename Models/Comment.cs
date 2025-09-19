using System;

namespace VulnerableCoreApp.Models
{
    public class Comment
    {
        // Vulnerable: Null pointer dereference
        public int GetTextLength()
        {
            string s = null;
            return s.Length; // null dereference
        }

        public String ID { get; set; }
        public String Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public String Text { get; set; }
    }
}