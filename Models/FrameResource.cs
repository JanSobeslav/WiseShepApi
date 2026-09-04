// FrameResource.cs
namespace JesonApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FrameTypeEnum
{
    verse,
    chorus,
    bridge,
    prechorus,
    intro,
    outro
}

public class FrameResource : BaseEntity
{
    public int Id { get; set; }
    public FrameTypeEnum FrameType { get; set; } = FrameTypeEnum.verse;

    // FK na LyricResource
    public int LyricResourceId { get; set; }
    
    [JsonIgnore]
    public LyricResource? LyricResource { get; set; }

    // JSON sloupec pro překlady
    public string TranslationsJson { get; set; } = "[]";

    [NotMapped]
    public List<string> Translations
    {
        get => JsonSerializer.Deserialize<List<string>>(TranslationsJson)!;
        set => TranslationsJson = JsonSerializer.Serialize(value);
    }

    public string OwnBackgroundImage { get; set; } = string.Empty;
}