using FAQ.Models;
using Microsoft.EntityFrameworkCore;

namespace FAQ.Data;

public class FaqContext : DbContext
{
    public FaqContext(DbContextOptions<FaqContext> options) : base(options) { }

    public DbSet<Faq> Faqs => Set<Faq>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Topic>().HasData(
            new Topic { TopicId = "bootstrap", Name = "Bootstrap" },
            new Topic { TopicId = "csharp", Name = "C#" },
            new Topic { TopicId = "js", Name = "JavaScript" }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = "general", Name = "General" },
            new Category { CategoryId = "hist", Name = "History" }
        );

        modelBuilder.Entity<Faq>().HasData(
            new Faq { FaqId = 1, Question = "What is Bootstrap?", Answer = "A CSS framework for creating responsive web apps for multiple screen sizes.", TopicId = "bootstrap", CategoryId = "general" },
            new Faq { FaqId = 2, Question = "What is C#?", Answer = "A general purpose object oriented language that uses a concise, Java-like syntax.", TopicId = "csharp", CategoryId = "general" },
            new Faq { FaqId = 3, Question = "What is JavaScript?", Answer = "A general purpose scripting language that executes in a web browser.", TopicId = "js", CategoryId = "general" },
            new Faq { FaqId = 4, Question = "When was Bootstrap first released?", Answer = "In 2011.", TopicId = "bootstrap", CategoryId = "hist" },
            new Faq { FaqId = 5, Question = "When was C# first released?", Answer = "In 2002.", TopicId = "csharp", CategoryId = "hist" },
            new Faq { FaqId = 6, Question = "When was JavaScript first released?", Answer = "In 1995.", TopicId = "js", CategoryId = "hist" }
        );
    }
}