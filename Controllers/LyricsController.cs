using Microsoft.AspNetCore.Mvc;
using JesonApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JesonApi.Controllers;



[ApiController]
[Route("api/lyrics")]
public class LyricsController : ControllerBase
{

    private readonly AppDbContext _context;
        public LyricsController(AppDbContext context)
    {
        _context = context;
    }
    private readonly List<LyricResource> lyricsList = new List<LyricResource>
        {
           new LyricResource { Id = 1, Name = "10,000 Reasons", Author = "Matt Redman", Description = "This is test desc", Frames = new List<FrameResource>{new FrameResource {Id = 1, FrameType = FrameTypeEnum.verse, Translations = new()}}, BackgroundImage = "some-image-path.cz" },

    new LyricResource { Id = 2, Name = "Way Maker", Author = "Sinach", Description = "Worship song about God's miracles", Frames = new(), BackgroundImage = "waymaker-bg.jpg" },

    new LyricResource { Id = 3, Name = "Oceans", Author = "Hillsong UNITED", Description = "Popular worship song about faith", Frames = new(), BackgroundImage = "oceans-bg.jpg" },

    new LyricResource { Id = 4, Name = "Great Are You Lord", Author = "All Sons & Daughters", Description = "Song about God's greatness", Frames = new(), BackgroundImage = "great-bg.jpg" },

    new LyricResource { Id = 5, Name = "Build My Life", Author = "Pat Barrett", Description = "Song about building life on God", Frames = new(), BackgroundImage = "build-bg.jpg" },

    new LyricResource { Id = 6, Name = "Reckless Love", Author = "Cory Asbury", Description = "Song about unconditional love", Frames = new(), BackgroundImage = "reckless-bg.jpg" },

    new LyricResource { Id = 7, Name = "What a Beautiful Name", Author = "Hillsong Worship", Description = "Song about the name of Jesus", Frames = new(), BackgroundImage = "beautiful-bg.jpg" },

    new LyricResource { Id = 8, Name = "Living Hope", Author = "Phil Wickham", Description = "Song about salvation and hope", Frames = new(), BackgroundImage = "hope-bg.jpg" }
        };

   [HttpGet]
    public async Task<IActionResult> GetLyrics()
    {
        var lyrics = await _context.Lyrics
            // .Include(l => l.Frames) // načte i frames
            .ToListAsync();

        return Ok(lyrics);
    }


[HttpGet("{id}")]
public async Task<IActionResult> GetLyricsDetail(int id)
{
    var lyric = await _context.Lyrics
        .AsNoTracking()          // nemusíš sledovat entity
        .Select(l => new 
        {
            l.Id,
            l.Name,
            l.Author,
            l.Description,
            l.Frames,
            l.BackgroundImage,
            // další pole z LyricResource, ale bez Frames
        })
        .FirstOrDefaultAsync(l => l.Id == id);

    if (lyric == null)
        return NotFound();

    return Ok(lyric);
}

[HttpPost]
public async Task<IActionResult> CreateLyrics([FromBody] LyricResource newLyrics)
{
    if (newLyrics == null)
        return BadRequest();

    // Přidáme do DB
    _context.Lyrics.Add(newLyrics);
    await _context.SaveChangesAsync(); // INSERT + automaticky uloží i Frames díky navigaci

    // Vrátíme 201 Created + URI nové položky
    return CreatedAtAction(
        nameof(GetLyricsDetail),
        new { id = newLyrics.Id },
        newLyrics
    );
}


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLyrics(int id, [FromBody] LyricResource updatedLyrics)
    {
        if (updatedLyrics == null)
            return BadRequest();

        var existing = await _context.Lyrics.FirstOrDefaultAsync(l => l.Id == id);
        if (existing == null)
            return NotFound();

        // Aktualizace polí přímo v DB
        existing.Name = updatedLyrics.Name;
        existing.Author = updatedLyrics.Author;
        existing.Description = updatedLyrics.Description;
        existing.BackgroundImage = updatedLyrics.BackgroundImage;

        await _context.SaveChangesAsync();

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLyrics(int id)
    {
        var existing = await _context.Lyrics.FirstOrDefaultAsync(l => l.Id == id);
        if (existing == null)
            return NotFound();

        _context.Lyrics.Remove(existing);
        await _context.SaveChangesAsync();

        return NoContent(); // 204 = úspěšně smazáno
    }

    [HttpGet("{id}/frame")]
public async Task<IActionResult> GetFrames(int id)
{
    var lyricExists = await _context.Lyrics.AnyAsync(l => l.Id == id);
    if (!lyricExists)
        return NotFound();

    var frames = await _context.Frames
        .Where(f => f.LyricResourceId == id)
        .ToListAsync();

    return Ok(frames);
}

     [HttpPost("{id}/frame")]
public async Task<IActionResult> AddFrame(int id, [FromBody] FrameResource newFrame)
{
    var lyricExists = await _context.Lyrics.AnyAsync(l => l.Id == id);
    if (!lyricExists) return NotFound();

    newFrame.LyricResourceId = id;
    _context.Frames.Add(newFrame);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetFrames), new { id = id }, newFrame);
}

    [HttpPut("{id}/frame/{frameId}")]
    public async Task<IActionResult> UpdateFrame(int id, int frameId, [FromBody] FrameResource updatedFrame)
    {
        if (updatedFrame == null)
            return BadRequest();

        var frame = await _context.Frames
            .FirstOrDefaultAsync(f => f.Id == frameId && f.LyricResourceId == id);

        if (frame == null) return NotFound();

        // Aktualizujeme hodnoty
        frame.FrameType = updatedFrame.FrameType;
        frame.Translations = updatedFrame.Translations;
        frame.OwnBackgroundImage = updatedFrame.OwnBackgroundImage;

        await _context.SaveChangesAsync();

        return NoContent(); // 204 – úspěšně aktualizováno
    }

    [HttpDelete("{id}/frame/{frameId}")]
    public async Task<IActionResult> DeleteFrame(int id, int frameId)
    {
        var frame = await _context.Frames
            .FirstOrDefaultAsync(f => f.Id == frameId && f.LyricResourceId == id);

        if (frame == null) return NotFound();

        _context.Frames.Remove(frame);
        await _context.SaveChangesAsync();

        return NoContent(); // 204 – úspěšně smazáno
    }

}