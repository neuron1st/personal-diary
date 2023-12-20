using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonalDiary.BL.Entities.DiaryEntries;
using PersonalDiary.BL.Entities.DiaryEntries.Entities;
using PersonalDiary.WebAPI.Controllers.Entities.DiaryEntries.Entities;

namespace PersonalDiary.WebAPI.Controllers.Entities.DiaryEntries;

[ApiController]
[Route("[controller]")]
public class DiaryEntriesController : ControllerBase
{
    private readonly IDiaryEntriesManager _diaryEntriesManager;
    private readonly IDiaryEntriesProvider _diaryEntriesProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public DiaryEntriesController(
        IDiaryEntriesManager diaryEntriesManager,
        IDiaryEntriesProvider diaryEntriesProvider,
        IMapper mapper,
        ILogger logger)
    {
        _diaryEntriesManager = diaryEntriesManager;
        _diaryEntriesProvider = diaryEntriesProvider;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAllDiaryEntries()
    {
        var diaryEntries = _diaryEntriesProvider.GetDiaryEntries();
        return Ok(new DiaryEntriesListResponse()
        {
            DiaryEntries = diaryEntries.ToList()
        });
    }

    [HttpGet]
    [Route("filter")]
    public IActionResult GetDiaryEntries([FromQuery] DiaryEntriesFilter filter)
    {
        var diaryEntries = _diaryEntriesProvider.GetDiaryEntries(_mapper.Map<DiaryEntryModelFilter>(filter));
        return Ok(new DiaryEntriesListResponse()
        {
            DiaryEntries = diaryEntries.ToList()
        });
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetDiaryEntriesInfo([FromRoute] Guid id)
    {
        try
        {
            var diaryEntries = _diaryEntriesProvider.GetDiaryEntryInfo(id);
            return Ok(diaryEntries);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateDiaryEntry([FromBody] CreateDiaryEntryRequest request)
    {
        try
        {
            var diaryEntries = _diaryEntriesManager.CreateDiaryEntry(_mapper.Map<CreateDiaryEntryModel>(request));
            return Ok(diaryEntries);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateDiaryEntryInfo([FromRoute] Guid id, UpdateDiaryEntryRequest request)
    {
        try
        {
            var diaryEntries = _diaryEntriesManager.UpdateDiaryEntry(id, _mapper.Map<UpdateDiaryEntryModel>(request));
            return Ok(diaryEntries);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());
            return NotFound(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteDiaryEntry([FromRoute] Guid id)
    {
        try
        {
            _diaryEntriesManager.DeleteDiaryEntry(id);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());
            return NotFound(ex.Message);
        }
    }
}
