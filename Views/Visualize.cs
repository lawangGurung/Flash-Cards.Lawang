using System;
using Spectre.Console;

namespace Flash_Cards.Lawang.Views;

public class Visualize
{
    public void ShowTitle(string title)
    {
        var panel = new Panel(new FigletText($"{title}").Color(Color.Red))
            .BorderColor(Color.Aquamarine3)
            .PadTop(1)
            .PadBottom(1)
            .Header(new PanelHeader("[blue3 bold]APPLICATION[/]"))
            .Border(BoxBorder.Double)
            .Expand();

        AnsiConsole.Write(panel);        
    }
}
