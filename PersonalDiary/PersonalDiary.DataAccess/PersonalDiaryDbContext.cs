using Microsoft.EntityFrameworkCore;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.DataAccess;

public class PersonalDiaryDbContext : DbContext
{
    public DbSet<AdminEntity> Admins { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<DiaryEntryEntity> DiaryEntries { get; set; }

    public DbSet<FolderEntity> Folders { get; set; }

    public DbSet<TagEntity> Tags { get; set; }

    public PersonalDiaryDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Admin
        modelBuilder.Entity<AdminEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<AdminEntity>().HasIndex(x => x.ExternalId).IsUnique();

        // User
        modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<UserEntity>().HasMany(x => x.DiaryEntries)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        // Diary Entry
        modelBuilder.Entity<DiaryEntryEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<DiaryEntryEntity>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<DiaryEntryEntity>().Property(x => x.IsPublic).HasDefaultValue(false);
        modelBuilder.Entity<DiaryEntryEntity>().HasMany(x => x.Tags)
            .WithMany(x => x.DiaryEntries)
            .UsingEntity(x => x.ToTable("tags_of_entries"));
        modelBuilder.Entity<DiaryEntryEntity>().HasMany(x => x.Folders)
            .WithMany(x => x.DiaryEntries)
            .UsingEntity(x => x.ToTable("folders_of_entries"));

        // Folder
        modelBuilder.Entity<FolderEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<FolderEntity>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<FolderEntity>().HasMany(x => x.Folders)
            .WithOne(x => x.ParentFolder)
            .HasForeignKey(x => x.ParentFolderId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TagEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<TagEntity>().HasIndex(x => x.ExternalId).IsUnique();
    }
}