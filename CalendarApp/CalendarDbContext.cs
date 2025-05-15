// CalendarDbContext.cs
using Microsoft.EntityFrameworkCore;
using CalendarApp.Models;
using Microsoft.Extensions.Logging;
using System;

namespace CalendarApp.Data
{
    public class CalendarDbContext : DbContext
    {
        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<GroupMeetingParticipant> GroupMeetingParticipants { get; set; } // DbSet cho lớp Join

        // Connection string & Logger Factory
        private const string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=CalendarAppDB_EFCore31_Final;Trusted_Connection=True;"; // Đổi tên DB nếu cần
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        // OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString).UseLoggerFactory(MyLoggerFactory);
                //.EnableSensitiveDataLogging();
            }
        }

        // OnModelCreating - Cấu hình mối quan hệ và Seed Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Cấu hình One-to-Many thông thường (Cascade Delete OK) ---
            // User <-> Appointment
            modelBuilder.Entity<User>()
                .HasMany(u => u.Appointments).WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId).OnDelete(DeleteBehavior.Cascade);
            // User <-> Reminder
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reminders).WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            // Appointment <-> Reminder
            modelBuilder.Entity<Appointment>()
                .HasMany<Reminder>().WithOne(r => r.RelatedAppointment)
                .HasForeignKey(r => r.RelatedAppointmentId).OnDelete(DeleteBehavior.Cascade);

            // --- Cấu hình Many-to-Many qua GroupMeetingParticipant (Restrict Delete) ---
            // Khóa chính kết hợp
            modelBuilder.Entity<GroupMeetingParticipant>()
                .HasKey(gmp => new { gmp.GroupMeetingId, gmp.ParticipantId });
            // Quan hệ tới GroupMeeting
            modelBuilder.Entity<GroupMeetingParticipant>()
                .HasOne(gmp => gmp.GroupMeeting).WithMany(gm => gm.MeetingParticipants)
                .HasForeignKey(gmp => gmp.GroupMeetingId)
                .OnDelete(DeleteBehavior.Restrict); // <<<<<< QUAN TRỌNG
            // Quan hệ tới User (Participant)
            modelBuilder.Entity<GroupMeetingParticipant>()
                .HasOne(gmp => gmp.Participant).WithMany(u => u.MeetingParticipations)
                .HasForeignKey(gmp => gmp.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict); // <<<<<< QUAN TRỌNG

            // --- Seed Data ---
            var testUserId1 = 1; var testUserId2 = 2;
            var testUserId3 = 3; var testUserId4 = 4;
            modelBuilder.Entity<User>().HasData(
                new { Id = testUserId1, Username = "user1", Password = "123456" },
                new { Id = testUserId2, Username = "user2", Password = "654321" },
                new { Id = testUserId3, Username = "user3", Password = "111111" },
                new { Id = testUserId4, Username = "user4", Password = "000000" });

            var appointmentId1 = 1; var appointmentId2 = 2; var groupMeetingId3 = 3;
            modelBuilder.Entity<Appointment>().HasData(
                new { Id = appointmentId1, OwnerId = testUserId1, Name = "Meeting with Boss", Location = "Office A", StartTime = DateTime.Now.Date.AddDays(1).AddHours(9), EndTime = DateTime.Now.Date.AddDays(1).AddHours(10) },
                new { Id = appointmentId2, OwnerId = testUserId2, Name = "Project Deadline Prep", Location = "Home Office", StartTime = DateTime.Now.Date.AddDays(2).AddHours(14), EndTime = DateTime.Now.Date.AddDays(2).AddHours(16) });

            modelBuilder.Entity<GroupMeeting>().HasData(
                 new { Id = groupMeetingId3, OwnerId = testUserId1, Name = "Team Sync", Location = "Meeting Room 1", StartTime = DateTime.Now.Date.AddDays(1).AddHours(11), EndTime = DateTime.Now.Date.AddDays(1).AddHours(11).AddMinutes(30) });

            modelBuilder.Entity<Reminder>().HasData(
                new { Id = 1, UserId = testUserId1, RelatedAppointmentId = appointmentId1, TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(8).AddMinutes(45) },
                new { Id = 2, UserId = testUserId2, RelatedAppointmentId = appointmentId2, TriggerTime = DateTime.Now.Date.AddDays(2).AddHours(13).AddMinutes(30) });

            // Seed bảng join GroupMeetingParticipants
            modelBuilder.Entity<GroupMeetingParticipant>().HasData(
                 new { GroupMeetingId = groupMeetingId3, ParticipantId = testUserId1 },
                 new { GroupMeetingId = groupMeetingId3, ParticipantId = testUserId2 });
        }
    }
}