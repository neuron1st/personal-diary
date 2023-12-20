using AutoMapper;
using Moq;
using NUnit.Framework;
using PersonalDiary.BL.Entities.Admins;
using PersonalDiary.BL.Entities.Admins.Entities;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.UnitTests.BL.Admins;

[TestFixture]
public class AdminsProviderTests
{
    [Test]
    public void GetAdminInfo_ValidId_ReturnsAdminModel()
    {
        // Arrange
        var adminId = Guid.NewGuid();
        var mockRepository = new Mock<IAdminRepository>();
        mockRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(new AdminEntity
        {
            ExternalId = adminId,
            UserName = "test",
            Email = "test@example.com"
        });

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<AdminModel>(It.IsAny<AdminEntity>())).Returns(new AdminModel
        {
            Id = adminId,
            UserName = "test",
            Email = "test@example.com"
        });

        var adminsProvider = new AdminsProvider(mockRepository.Object, mockMapper.Object);

        // Act
        var adminModel = adminsProvider.GetAdminInfo(adminId);

        // Assert
        Assert.NotNull(adminModel);
        Assert.That(adminModel.Id, Is.EqualTo(adminId));
    }

    [Test]
    public void GetAdminInfo_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var invalidId = Guid.NewGuid();
        var mockRepository = new Mock<IAdminRepository>();
        mockRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((AdminEntity)null);

        var mockMapper = new Mock<IMapper>();

        var adminsProvider = new AdminsProvider(mockRepository.Object, mockMapper.Object);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => adminsProvider.GetAdminInfo(invalidId));
        Assert.That(ex.Message, Is.EqualTo("not found"));
    }
}