namespace JesonApi.Models;

public class LyricResource : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<FrameResource> Frames { get; set; } = new();
    public string BackgroundImage { get; set; } = string.Empty;
}
