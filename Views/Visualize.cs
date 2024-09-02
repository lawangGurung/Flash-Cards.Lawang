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
    public void RenderResult(int rowsAffected)
    {
        if (rowsAffected == 1)
        {
            ShowResult("green", rowsAffected);
        }
        else
        {
            ShowResult("red", rowsAffected);
        }
    }
    private void ShowResult(string color, int rowsAffected)
    {
        Panel panel = new Panel(new Markup($"[{color} bold]{rowsAffected} rows Affected[/]\n[grey](Press 'Enter' to Continue.)[/]"))
                        .Padding(1, 1, 1, 1)
                        .Header("Result")
                        .Border(BoxBorder.Rounded);

        AnsiConsole.Write(panel);
        Console.ReadLine();
    }
}
