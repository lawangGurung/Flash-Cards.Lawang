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

    public void RenderStackTable(List<Stack> listOfStack)
    {
        if(listOfStack.Count() == 0)
        {
            Panel nullPanel = new Panel(new Markup("[red bold]STACK TABLE IS EMPTY!!![/]"))
                .Border(BoxBorder.Heavy)
                .BorderColor(Color.IndianRed1_1)
                .Padding(1, 1, 1, 1)
                .Header("Result");

            
            AnsiConsole.Write(nullPanel);
            Console.ReadLine();
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Expand()
            .BorderColor(Color.Aqua)
            .ShowRowSeparators();



        table.AddColumns(new TableColumn[]
        {
            new TableColumn("[darkgreen]Id[/]").Centered(),
            new TableColumn("[darkcyan]Stack Name[/]").Centered()
        });

        foreach(var stack in listOfStack)
        {
            table.AddRow(
                new Markup($"[cyan1]{stack.Id}[/]").Centered(),
                new Markup($"[turquoise2]{stack.Name}[/]").Centered()
            );
        }

        AnsiConsole.Write(table);

    }
}
