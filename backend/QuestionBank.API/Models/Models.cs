using System;
using System.Collections.Generic;

namespace Questionbanknew.Models
{
    // For user registration
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User";  // Default role = User
    }

    // For user login
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    // User model (database table)
    public class UserModel
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }

    // Single option for a question
    public class OptionModel
    {
        public string OptionLabel { get; set; } = string.Empty; // Example: "A", "B"
        public string OptionText { get; set; } = string.Empty;  // Example: "Paris"
    }

    // Question with options
    public class QuestionModel
    {
        public int QuestionID { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string QuestionText { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty;
        public int CreatedBy { get; set; }

        public List<OptionModel> Options { get; set; } = new List<OptionModel>();
    }
}
