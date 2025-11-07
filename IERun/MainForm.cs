using System.Text.Json;
using IERun.Models;

namespace IERun;

public class MainForm : Form {
    private readonly Config _config;

    public MainForm() {
        _config = LoadConfig();

        ConfigureForm();
        PopulateButtons();
    }

    private void ConfigureForm() {
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(Config.BUTTON_MARGIN * 2 + Config.BUTTON_WIDTH, Config.BUTTON_MARGIN * 2 + Config.BUTTON_HEIGHT * _config.Items.Count);
        MinimizeBox = false;
        MaximizeBox = false;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "IERun";
    }

    private static Config LoadConfig() {
        var path = Path.Combine(Application.StartupPath, "config.json");

        if (File.Exists(path)) {
            return JsonSerializer.Deserialize<Config>(File.ReadAllText(path)) ?? new Config();
        }

        return new Config();
    }

    private void PopulateButtons() {
        foreach (var item in _config.Items) {
            var button = new Button();
            button.Text = item.Title;
            button.Width = Config.BUTTON_WIDTH;
            button.Height = Config.BUTTON_HEIGHT;
            button.Location = new Point(Config.BUTTON_MARGIN, Config.BUTTON_MARGIN + (Config.BUTTON_HEIGHT + Config.BUTTON_MARGIN) * Controls.Count);
            button.Click += (_, _) => OpenSite(item.Url);
            Controls.Add(button);
        }
    }

    private static void OpenSite(Uri url) {
        var type = Type.GetTypeFromProgID("InternetExplorer.Application");

        if (type is null) {
            throw new Exception("Internet Explorer is not installed.");
        }

        dynamic? ie = Activator.CreateInstance(type);

        if (ie is null) {
            throw new Exception("Failed to create Internet Explorer instance.");
        }

        ie.Navigate(url);
        // ie.MenuBar = false;
        // ie.AddressBar = false;
        // ie.ToolBar = 0;
        // ie.Width = 10;
        // ie.Height = 10;
        // ie.Visible = true;

        while (ie.Busy) Thread.Sleep(1);

        var hwnd = (IntPtr)ie.HWND;
        Win32.ShowWindow(hwnd, Win32.SW_MAXIMIZE);
    }
}
