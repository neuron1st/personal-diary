using PersonalDiary.BL.Entities.DiaryEntries.Entities;

namespace PersonalDiary.WebAPI.Controllers.Entities.DiaryEntries.Entities;

public class DiaryEntriesListResponse
{
    public List<DiaryEntryModel> DiaryEntries { get; set; }
}
