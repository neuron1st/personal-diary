using AutoMapper;
using Moq;
using NUnit.Framework;
using PersonalDiary.BL.Entities.DiaryEntries;
using PersonalDiary.BL.Entities.DiaryEntries.Entities;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.UnitTests.BL.DiaryEntries
{
    [TestFixture]
    public class DiaryEntriesProviderTests
    {
        [Test]
        public void GetDiaryEntries_InvalidFilter_ReturnsEmptyList()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<DiaryEntryEntity>>();
            mockRepository.Setup(r => r.GetAll())
                          .Returns(new List<DiaryEntryEntity>());

            var mockMapper = new Mock<IMapper>();
            var diaryEntriesProvider = new DiaryEntriesProvider(mockRepository.Object, mockMapper.Object);

            // Act
            var diaryEntryModels = diaryEntriesProvider.GetDiaryEntries();

            // Assert
            Assert.NotNull(diaryEntryModels);
            Assert.That(diaryEntryModels, Is.Empty);
        }

        [Test]
        public void GetDiaryEntryInfo_ValidId_ReturnsDiaryEntryModel()
        {
            // Arrange
            var diaryEntryId = Guid.NewGuid();
            var mockRepository = new Mock<IRepository<DiaryEntryEntity>>();
            mockRepository.Setup(r => r.GetById(It.IsAny<Guid>()))
                          .Returns(new DiaryEntryEntity { ExternalId = diaryEntryId });

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<DiaryEntryModel>(It.IsAny<DiaryEntryEntity>()))
                      .Returns((DiaryEntryEntity entity) => new DiaryEntryModel { Id = diaryEntryId });

            var diaryEntriesProvider = new DiaryEntriesProvider(mockRepository.Object, mockMapper.Object);

            // Act
            var diaryEntryModel = diaryEntriesProvider.GetDiaryEntryInfo(diaryEntryId);

            // Assert
            Assert.NotNull(diaryEntryModel);
            Assert.That(diaryEntryModel.Id, Is.EqualTo(diaryEntryId));
        }

        [Test]
        public void GetDiaryEntryInfo_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var mockRepository = new Mock<IRepository<DiaryEntryEntity>>();
            mockRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((DiaryEntryEntity)null);

            var mockMapper = new Mock<IMapper>();
            var diaryEntriesProvider = new DiaryEntriesProvider(mockRepository.Object, mockMapper.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => diaryEntriesProvider.GetDiaryEntryInfo(invalidId));
            Assert.That(ex.Message, Is.EqualTo("not found"));
        }
    }
}
