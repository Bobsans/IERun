namespace IERun.Models;

[Serializable]
public class Config {
    public const int BUTTON_MARGIN = 5;
    public const int BUTTON_WIDTH = 350;
    public const int BUTTON_HEIGHT = 30;

    public List<MenuItem> Items { get; set; } = [];
}
