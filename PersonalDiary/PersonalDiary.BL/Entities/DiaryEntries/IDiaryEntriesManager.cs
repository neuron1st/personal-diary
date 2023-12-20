using PersonalDiary.BL.Entities.DiaryEntries.Entities;

namespace PersonalDiary.BL.Entities.DiaryEntries;

public interface IDiaryEntriesManager
{
    DiaryEntryModel CreateDiaryEntry(CreateDiaryEntryModel model);
    void DeleteDiaryEntry(Guid id);
    DiaryEntryModel UpdateDiaryEntry(Guid id, UpdateDiaryEntryModel model);
}