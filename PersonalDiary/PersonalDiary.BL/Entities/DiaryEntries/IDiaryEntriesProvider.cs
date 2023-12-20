using PersonalDiary.BL.Entities.DiaryEntries.Entities;

namespace PersonalDiary.BL.Entities.DiaryEntries;

public interface IDiaryEntriesProvider
{
    IEnumerable<DiaryEntryModel> GetDiaryEntries(DiaryEntryModelFilter? filter = null);
    DiaryEntryModel GetDiaryEntryInfo(Guid id);
}
