namespace IERun.Models;

[Serializable]
public class MenuItem {
    public required Uri Url { get; set; }
    public required string Title { get; set; }
}
