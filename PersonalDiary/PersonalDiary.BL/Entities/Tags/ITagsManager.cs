using PersonalDiary.BL.Entities.Tags.Entities;

namespace PersonalDiary.BL.Entities.Tags;

public interface ITagsManager
{
    TagModel CreateTag(CreateTagModel model);
    void DeleteTag(Guid id);
    TagModel UpdateTag(Guid id, UpdateTagModel model);
}
