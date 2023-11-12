using Microsoft.EntityFrameworkCore;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.DataAccess;

public class PersonalDiaryDbContext : DbContext
{
    public DbSet<AdminEntity> Admins { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<DiaryEntryEntity> DiaryEntries { get; set; }

    public DbSet<FolderEntity> Folders { get; set; }
    public DbSet<FolderOfEntryEntity> FoldersOfEntry { get; set; }

    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<TagOfEntryEntity> TagsOfEntry { get; set; }

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
            .WithOne(x => x.DiaryEntry)
            .HasForeignKey(x => x.DiaryEntryId);
        modelBuilder.Entity<DiaryEntryEntity>().HasMany(x => x.Folders)
            .WithOne(x => x.DiaryEntry)
            .HasForeignKey(x => x.DiaryEntryId);

        // Folder
        modelBuilder.Entity<FolderEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<FolderEntity>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<FolderEntity>().HasMany(x => x.FoldersOfEntry)
            .WithOne(x => x.Folder)
            .HasForeignKey(x => x.FolderId);
        modelBuilder.Entity<FolderEntity>().HasMany(x => x.Folders)
            .WithOne(x => x.ParentFolder)
            .HasForeignKey(x => x.ParentFolderId)
            .OnDelete(DeleteBehavior.NoAction);

        // Tag
        modelBuilder.Entity<TagEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<TagEntity>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<TagEntity>().HasMany(x => x.Tags)
            .WithOne(x => x.Tag)
            .HasForeignKey(x => x.TagId);

        // Folder of entry
        modelBuilder.Entity<FolderOfEntryEntity>().HasKey(x => new { x.FolderId, x.DiaryEntryId });

        // Tag of entry
        modelBuilder.Entity<TagOfEntryEntity>().HasKey(x => new { x.TagId, x.DiaryEntryId });
    }
}