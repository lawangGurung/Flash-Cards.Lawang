using System;
using System.Runtime.InteropServices;
using Flash_Cards.Lawang.Models;
using Spectre.Console;

namespace Flash_Cards.Lawang;

public class Validation
{
    public Option ChooseOption(List<Option> listOfOptions, string title)
    {
        AnsiConsole.Write(new Rule($"[blue]{title}[/]").LeftJustified().RuleStyle("red"));

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<Option>()
            .Title("\n[bold cyan underline]What [green]opertion[/] do you want to perform?[/]")
            .UseConverter<Option>(c => c.Display)
            .MoreChoicesText("[grey](press UP and DOWN key to navigate)[/]")
            .AddChoices(listOfOptions)
            .HighlightStyle(Color.Blue3)
            .WrapAround()
        );

        return selection;
    }

    public string? ValidateStackName()
    {
        AnsiConsole.MarkupLine("[green bold]Give the [cyan1]Stack Name[/]?.[/]");
        AnsiConsole.MarkupLine("[grey](press '0' to go back to menu)[/]");
        string? userInput = Console.ReadLine()?.Trim();

        do
        {
            if (userInput == "0")
            {
                return null;
            }
            else if (!string.IsNullOrEmpty(userInput))
            {
                return userInput;
            }
            else
            {
                AnsiConsole.MarkupLine("[red bold]Please don't enter the empty or null value[/]");
                userInput = Console.ReadLine()?.Trim();
            }

        } while (true);
    }

}
