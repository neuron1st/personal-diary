using FluentAssertions;
using NUnit.Framework;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.UnitTests.Repository;

[TestFixture]
[Category("Integration")]
public class UserRepositoryTests : RepositoryTestsBaseClass
{
    [Test]
    public void GetAllUsersTest()
    {
        // prepare
        using var context = DbContextFactory.CreateDbContext();
        var users = new UserEntity[]
        {
            new UserEntity()
            {
                Login = "User1",
                PasswordHash = "pepega",
                Email = "user1@gmail.com",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Login = "User2",
                PasswordHash = "pepega",
                Email = "user2@gmail.com",
                ExternalId = Guid.NewGuid()
            }
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        // execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll();

        // assert
        actualUsers.Should().BeEquivalentTo(users);
    }

    [Test]
    public void GetAllUsersWithFilterTest()
    {
        // prepare
        using var context = DbContextFactory.CreateDbContext();
        var users = new UserEntity[]
        {
            new UserEntity()
            {
                Login = "User1",
                PasswordHash = "pepega",
                Email = "user1@gmail.com",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Login = "User2",
                PasswordHash = "zuzuga",
                Email = "user2@gmail.com",
                ExternalId = Guid.NewGuid()
            }
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        // execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll(x => x.Login == "User2");

        // assert
        actualUsers.Should().BeEquivalentTo(users.Where(x => x.Login == "User2"));
    }

    [Test]
    public void SaveNewUserTest()
    {
        // prepare
        using var context = DbContextFactory.CreateDbContext();

        // execute
        var user = new UserEntity()
        {
            Login = "User1",
            PasswordHash = "pepega",
            Email = "user1@gmail.com",
            ExternalId = Guid.NewGuid()
        };
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(user);

        // assert
        var actualUser = context.Users.SingleOrDefault();
        actualUser.Should().BeEquivalentTo(user, options => options
            .Excluding(x => x.Id)
            .Excluding(x => x.ExternalId)
            .Excluding(x => x.CreationTime)
            .Excluding(x => x.ModificationTime));
        actualUser.Id.Should().NotBe(default);
        actualUser.ExternalId.Should().NotBe(Guid.Empty);
        actualUser.CreationTime.Should().NotBe(default);
        actualUser.ModificationTime.Should().NotBe(default);
    }

    [Test]
    public void UpdateUserTest()
    {
        // prepare
        using var context = DbContextFactory.CreateDbContext();

        var user = new UserEntity()
        {
            Login = "User1",
            PasswordHash = "pepega",
            Email = "user1@gmail.com",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(user);
        context.SaveChanges();

        // execute
        user.Login = "user123";
        user.PasswordHash = "pepe";
        user.Email = "user123@gmail.com";
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(user);

        // assert
        var actualUser = context.Users.SingleOrDefault();
        actualUser.Should().BeEquivalentTo(user);
    }

    [Test]
    public void DeleteUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        var user = new UserEntity()
        {
            Login = "User1",
            PasswordHash = "pepega",
            Email = "user1@gmail.com",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(user);
        context.SaveChanges();

        //execute

        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Delete(user);

        //assert
        context.Users.Count().Should().Be(0);
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
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
        }
    }
}
