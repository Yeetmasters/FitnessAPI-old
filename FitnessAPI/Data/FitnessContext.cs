using FitnessAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessAPI.Data
{
    public class FitnessContext : DbContext
    {
        // an empty constructor
        public FitnessContext() { }

        // base(options) calls the base class's constructor,
        // in this case, our base class is DbContext
        public FitnessContext(DbContextOptions<FitnessContext> options) : base(options) { }

        // Use DbSet<*TableName*> to query or read and 
        // write information about An Entity
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }
        /*
         * Not sure if I need this line:
         * public static System.Collections.Specialized.NameValueCollection AppSettings { get; 
         */

        // configure the database to be used by this context
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            // schoolSIMSConnection is the name of the key that
            // contains the has the connection string as the value
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("FitnessAppDBConnection"));
        }

        // configures key properties - primary, foreign, composites etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             * User table contraints and field attribute setup
             */
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.Property(e => e.UserId).HasColumnName("user_id"); // primary key

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Height)
                    .HasColumnName("height");
                entity.Property(e => e.Weight)
                    .HasColumnName("weight");

            });

            modelBuilder.Entity<Workout>(entity =>
            {
                entity.ToTable("Workout");
                entity.Property(e => e.WorkoutId).HasColumnName("workout_id"); // primary key

                entity.Property(e => e.WorkoutName)
                    .IsRequired()
                    .HasColumnName("workoutname")
                    .HasMaxLength(30);
                entity.Property(e => e.Goal)
                    .HasColumnName("goal");
                entity.Property(e => e.Difficulty)
                    .HasColumnName("difficulty");

            });

            modelBuilder.Entity<User_Workout>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.WorkoutId }); // primary composite key

                entity.ToTable("User_Workout");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.WorkoutId).HasColumnName("workout_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserWorkouts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__User_Workout__user");

                entity.HasOne(d => d.Workout)
                    .WithMany(p => p.UserWorkouts)
                    .HasForeignKey(d => d.WorkoutId)
                    .HasConstraintName("FK__User_Workout__workout");
            });
        }
    }
}

