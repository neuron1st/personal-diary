using PersonalDiary.BL.Entities.Tags.Entities;

namespace PersonalDiary.BL.Entities.Tags;

public interface ITagsProvider
{
    IEnumerable<TagModel> GetTags(TagModelFilter? filter = null);
    TagModel GetTagInfo(Guid id);
}
