using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessAPI.Models
{
    /*
     * Intersection entity, resolves M:N relationship between program and user.
     * UserId and ProgramId form a composite key, which is set in the context file.
     */
    public class User_Workout
    {
        public int UserId { get; set; }
        public int WorkoutId { get; set; }

        public virtual User User { get; set; }
        public virtual Workout Workout{ get; set; }
    }
}
