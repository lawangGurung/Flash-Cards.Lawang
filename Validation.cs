using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
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

    public Stack? UpdateStack(List<Stack> stackList)
    {
        if (stackList.Count() == 0)
        {
            return null;
        }
        AnsiConsole.MarkupLine("[green bold]Give the Id of the Stack you want to [yellow]Update [/]?.[/]");

        AnsiConsole.MarkupLine("[grey](press '0' to go back to menu)[/]");
        string? userInput = Console.ReadLine()?.Trim();
        int Id = 0;
        Stack? selectedStack;

        do
        {
            if (userInput == "0")
            {
                return null;
            }
            else if (!string.IsNullOrEmpty(userInput) && int.TryParse(userInput, out Id))
            {
                selectedStack = stackList.FirstOrDefault(x => x.Id == Id);
                if (selectedStack != null)
                {
                    return selectedStack;
                }

                AnsiConsole.MarkupLine("[red bold]Stack of this Id don't exist[/]");
                userInput = Console.ReadLine()?.Trim();
            }
            else
            {
                AnsiConsole.MarkupLine("[red bold]Please don't enter the empty or null value.[/]");
                userInput = Console.ReadLine()?.Trim();
            }
        } while (true);
    }

    public Stack? UpdateStackName(Stack stack)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[grey](Press '0' to go back.)[/]");
        AnsiConsole.MarkupLine($"[bold]Enter the new [red]Stack Name[/] for [green]Id[/] ({stack.Id}): [/]");
        string? userInput = Console.ReadLine()?.Trim();
        if (userInput == "0")
        {
            return null;
        }

        do
        {
            if (!string.IsNullOrEmpty(userInput))
            {
                stack.Name = userInput;
                return stack;
            }
            AnsiConsole.MarkupLine("[red bold]Please don't enter the empty or null value.[/]");
            userInput = Console.ReadLine()?.Trim();
        } while (true);
    }

    public int DeleteStack(List<Stack> stackList)
    {
        if (stackList.Count() == 0)
        {
            return 0;
        }
        AnsiConsole.MarkupLine("[green bold]Give the Id of the Stack you want to [red]Delete [/]?.[/]");

        AnsiConsole.MarkupLine("[grey](press '0' to go back to menu)[/]");
        string? userInput = Console.ReadLine()?.Trim();
        int Id;
        do
        {
            if (userInput == "0")
            {
                return 0;
            }
            else if (!string.IsNullOrEmpty(userInput) && int.TryParse(userInput, out Id))
            {
                if (stackList.Exists(s => s.Id == Id))
                {
                    return Id;
                }
                AnsiConsole.MarkupLine("[red bold]Stack of this Id don't exist[/]");
                userInput = Console.ReadLine()?.Trim();

            }
            else
            {
                AnsiConsole.MarkupLine("[red bold]Please don't enter the empty or null value.[/]");
                userInput = Console.ReadLine()?.Trim();
            }
        } while (true);

    }

}
