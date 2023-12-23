using FluentAssertions;
using NUnit.Framework;
using PersonalDiary.DataAccess.Entities;
using PersonalDiary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDiary.UnitTests.DataAccess.Repository;

[TestFixture]
[Category("Integration")]
public class DiaryEntryRepositoryTests : RepositoryTestsBaseClass
{
    [Test]
    public void GetAllDiaryEntriesTest()
    {
        // prepare
        using var context = DbContextFactory.CreateDbContext();
        var user = new UserEntity()
        {
            UserName = "User1",
            PasswordHash = "pepega",
            Email = "user1@gmail.com",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(user);
        context.SaveChanges();

        var entries = new DiaryEntryEntity[]
        {
            new DiaryEntryEntity()
            {
                Heading = "Entry1",
                Text = "Text for Entry1",
                IsPublic = true,
                UserId = user.Id,
                ExternalId = Guid.NewGuid()
            },
            new DiaryEntryEntity()
            {
                Heading = "Entry2",
                Text = "Text for Entry2",
                IsPublic = false,
                UserId = user.Id,
                ExternalId = Guid.NewGuid()
            }
        };
        context.DiaryEntries.AddRange(entries);
        context.SaveChanges();

        // execute
        var repository = new Repository<DiaryEntryEntity>(DbContextFactory);
        var actualEntries = repository.GetAll();

        // assert
        actualEntries.Should().BeEquivalentTo(entries, options => options.Excluding(x => x.User));
    }

    [Test]
    public void GetAllDiaryEntriesWithFilterTest()
    {
        // prepare
        using var context = DbContextFactory.CreateDbContext();
        var user = new UserEntity()
        {
            UserName = "User1",
            PasswordHash = "pepega",
            Email = "user1@gmail.com",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(user);
        context.SaveChanges();

        var entries = new DiaryEntryEntity[]
        {
            new DiaryEntryEntity()
            {
                Heading = "Entry1",
                Text = "Text for Entry1",
                IsPublic = true,
                UserId = user.Id,
                ExternalId = Guid.NewGuid()
            },
            new DiaryEntryEntity()
            {
                Heading = "Entry2",
                Text = "Text for Entry2",
                IsPublic = false,
                UserId = user.Id,
                ExternalId = Guid.NewGuid()
            }
        };
        context.DiaryEntries.AddRange(entries);
        context.SaveChanges();

        // execute
        var repository = new Repository<DiaryEntryEntity>(DbContextFactory);
        var actualEntries = repository.GetAll(x => x.IsPublic);

        // assert
        actualEntries.Should().BeEquivalentTo(entries.Where(x => x.IsPublic),
            options => options.Excluding(x => x.User));
    }

    [Test]
    public void SaveNewDiaryEntryTest()
    {
        // prepare
        using var context = DbContextFactory.CreateDbContext();
        var user = new UserEntity()
        {
            UserName = "User1",
            PasswordHash = "pepega",
            Email = "user1@gmail.com",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(user);
        context.SaveChanges();

        // execute
        var entry = new DiaryEntryEntity()
        {
            Heading = "Entry1",
            Text = "Text for Entry1",
            IsPublic = true,
            UserId = user.Id,
            ExternalId = Guid.NewGuid()
        };
        var repository = new Repository<DiaryEntryEntity>(DbContextFactory);
        repository.Save(entry);

        // assert
        var actualEntry = context.DiaryEntries.SingleOrDefault();
        actualEntry.Should().BeEquivalentTo(entry, options => options.Excluding(x => x.User)
            .Excluding(x => x.Id)
            .Excluding(x => x.CreationTime)
            .Excluding(x => x.ModificationTime));
        actualEntry.Id.Should().NotBe(default);
        actualEntry.CreationTime.Should().NotBe(default);
        actualEntry.ModificationTime.Should().NotBe(default);
    }

    [Test]
    public void UpdateDiaryEntryTest()
    {
        // prepare
        using var context = DbContextFactory.CreateDbContext();
        var user = new UserEntity()
        {
            UserName = "User1",
            PasswordHash = "pepega",
            Email = "user1@gmail.com",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(user);
        context.SaveChanges();

        var entry = new DiaryEntryEntity()
        {
            Heading = "Entry1",
            Text = "Text for Entry1",
            IsPublic = true,
            UserId = user.Id,
            ExternalId = Guid.NewGuid()
        };
        context.DiaryEntries.Add(entry);
        context.SaveChanges();

        // execute
        entry.Heading = "UpdatedEntry";
        entry.Text = "Updated text";
        entry.IsPublic = false;

        var repository = new Repository<DiaryEntryEntity>(DbContextFactory);
        repository.Save(entry);

        // assert
        var actualEntry = context.DiaryEntries.SingleOrDefault();
        actualEntry.Should().BeEquivalentTo(entry, options => options.Excluding(x => x.User));
    }

    [Test]
    public void DeleteDiaryEntryTest()
    {
        // prepare
        using var context = DbContextFactory.CreateDbContext();
        var user = new UserEntity()
        {
            UserName = "User1",
            PasswordHash = "pepega",
            Email = "user1@gmail.com",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(user);
        context.SaveChanges();

        var entry = new DiaryEntryEntity()
        {
            Heading = "Entry1",
            Text = "Text for Entry1",
            IsPublic = true,
            UserId = user.Id,
            ExternalId = Guid.NewGuid()
        };
        context.DiaryEntries.Add(entry);
        context.SaveChanges();

        // execute
        var repository = new Repository<DiaryEntryEntity>(DbContextFactory);
        repository.Delete(entry);

        // assert
        context.DiaryEntries.Count().Should().Be(0);
    }

    [SetUp]
    public void SetUp()
    {
        CleanUp();
    }

    [TearDown]
    public void TearDown()
    {
        CleanUp();
    }

    public void CleanUp()
    {
        using (var context = DbContextFactory.CreateDbContext())
        {
            context.DiaryEntries.RemoveRange(context.DiaryEntries);
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
        }
    }
}

