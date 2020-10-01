using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessAPI.Models
{
    /*
     * User model holds personal attributes and sign in information.
     */
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; } // Primary Key
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }

        [Timestamp]
        public DateTime TimeCreated { get; set; }

        public virtual ICollection<User_Workout> UserWorkouts { get; set; }
    }
}
