using EventAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options) {
    public DbSet<Event> Events { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<EventParticipant> EventParticipants { get; set; }
    public DbSet<EventSpeaker> EventSpeakers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Speaker>().HasData(
            new Speaker { Id = 1, FirstName = "Anna", LastName = "Kowalska" },
            new Speaker { Id = 2, FirstName = "Jan", LastName = "Nowak" },
            new Speaker { Id = 3, FirstName = "Maria", LastName = "Wiśniewska" },
            new Speaker { Id = 4, FirstName = "Piotr", LastName = "Zieliński" },
            new Speaker { Id = 5, FirstName = "Ewa", LastName = "Dąbrowska" }
        );

        modelBuilder.Entity<Participant>().HasData(
            new Participant { Id = 1, FirstName = "Adam", LastName = "Kowal", Email = "adam1@mail.com" },
            new Participant { Id = 2, FirstName = "Beata", LastName = "Nowak", Email = "beata2@mail.com" },
            new Participant { Id = 3, FirstName = "Cezary", LastName = "Lis", Email = "cezary3@mail.com" },
            new Participant { Id = 4, FirstName = "Dorota", LastName = "Mazur", Email = "dorota4@mail.com" },
            new Participant { Id = 5, FirstName = "Edward", LastName = "Wójcik", Email = "edward5@mail.com" },
            new Participant { Id = 6, FirstName = "Filip", LastName = "Kaczmarek", Email = "filip6@mail.com" },
            new Participant { Id = 7, FirstName = "Grażyna", LastName = "Baran", Email = "grazyna7@mail.com" },
            new Participant { Id = 8, FirstName = "Henryk", LastName = "Sikora", Email = "henryk8@mail.com" },
            new Participant { Id = 9, FirstName = "Iwona", LastName = "Król", Email = "iwona9@mail.com" },
            new Participant { Id = 10, FirstName = "Jacek", LastName = "Wieczorek", Email = "jacek10@mail.com" },
            new Participant
                { Id = 11, FirstName = "Katarzyna", LastName = "Jankowska", Email = "katarzyna11@mail.com" },
            new Participant { Id = 12, FirstName = "Leszek", LastName = "Zając", Email = "leszek12@mail.com" },
            new Participant { Id = 13, FirstName = "Monika", LastName = "Pawlak", Email = "monika13@mail.com" },
            new Participant { Id = 14, FirstName = "Norbert", LastName = "Michalski", Email = "norbert14@mail.com" },
            new Participant { Id = 15, FirstName = "Olga", LastName = "Kubiak", Email = "olga15@mail.com" }
        );

        modelBuilder.Entity<Event>().HasData(
            new Event {
                Id = 1, Title = "AI Konferencja", Description = "Sztuczna inteligencja w praktyce",
                Date = DateTime.Today.AddDays(10), MaxPeople = 7
            },
            new Event {
                Id = 2, Title = "Chmura", Description = "Chmura i bezpieczeństwo", Date = DateTime.Today.AddDays(20),
                MaxPeople = 80
            },
            new Event {
                Id = 3, Title = "DevOps", Description = "Automatyzacja i CI/CD", Date = DateTime.Today.AddDays(30),
                MaxPeople = 60
            }
        );

        modelBuilder.Entity<EventSpeaker>().HasData(
            new EventSpeaker { EventId = 1, SpeakerId = 1 },
            new EventSpeaker { EventId = 1, SpeakerId = 2 },
            new EventSpeaker { EventId = 2, SpeakerId = 3 },
            new EventSpeaker { EventId = 2, SpeakerId = 4 },
            new EventSpeaker { EventId = 3, SpeakerId = 5 }
        );

        modelBuilder.Entity<EventParticipant>().HasData(
            new EventParticipant {
                EventId = 1, ParticipantId = 1, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 1, ParticipantId = 2, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 1, ParticipantId = 3, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 1, ParticipantId = 4, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 1, ParticipantId = 5, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 2, ParticipantId = 6, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 2, ParticipantId = 7, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 2, ParticipantId = 8, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 2, ParticipantId = 9, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 2, ParticipantId = 10, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 3, ParticipantId = 11, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 3, ParticipantId = 12, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 3, ParticipantId = 13, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 3, ParticipantId = 14, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            },
            new EventParticipant {
                EventId = 3, ParticipantId = 15, RegisterDate = DateTime.Today, Status = "Registered", CancelDate = null
            }
        );
    }
}