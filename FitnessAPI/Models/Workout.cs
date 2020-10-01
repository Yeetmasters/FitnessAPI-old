using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessAPI.Models
{
    public enum Goals
    {
        BOTH, BULK, CUT
    }

    public enum Difficulty
    {
        BEGINNER, INTERMEDIATE, ADVANCED
    }
    public class Workout
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public Goals Goal { get; set; }
        public Difficulty Difficulty { get; set; }

        public virtual ICollection<User_Workout> UserWorkouts { get; set; }
    }
}
