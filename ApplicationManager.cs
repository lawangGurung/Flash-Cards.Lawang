using System;
using System.Xml.Serialization;
using Flash_Cards.Lawang.Controller;
using Flash_Cards.Lawang.Models;
using Flash_Cards.Lawang.Views;
using Spectre.Console;

namespace Flash_Cards.Lawang;

public class ApplicationManager
{
    private Visualize _visual;
    private Validation _validation;
    private StackController _stackController;
    private FlashCardController _flashCardController;
    private StudyController _studyController;
    public ApplicationManager(Visualize visual,
        Validation validation,
        StackController stackController,
        FlashCardController flashCardController,
        StudyController studyController)
    {
        _visual = visual;
        _validation = validation;
        _stackController = stackController;
        _flashCardController = flashCardController;
        _studyController = studyController;
    }

    public void Start()
    {
        Console.Clear();
        bool exitApp = false;
        do
        {
            _visual.ShowTitle("Flash-Cards");

            List<Option> listOfOptions = new List<Option>()
            {
                new Option("Manage Stacks", 1),
                new Option("Manage Flash Cards", 2),
                new Option("Study", 3),
                new Option("View Study Session Data", 4),
                new Option("Exit", 0)
            };

            var option = _validation.ChooseOption(listOfOptions, "Menu Options", "[bold cyan underline]What [green]opertion[/] do you want to perform?[/]");
            exitApp = performOperation(option);
            Console.Clear();
        } while (!exitApp);

        _visual.ShowTitle("Have a nice day!!");

    }

    private bool performOperation(Option option)
    {

        switch (option.Value)
        {
            case 1:
                Console.Clear();
                var manageStack = new ManageStacks(_validation, _visual, _stackController);
                manageStack.OperationMenu();
                break;

            case 2:
                Console.Clear();
                var stackList = _stackController.GetAllStacks();
                var manageFlashCard = new ManageFlashCards(_validation, _visual, _flashCardController, stackList);
                manageFlashCard.OperationMenu();
                break;

            case 3:
                do
                {
                    Console.Clear();
                    var selectedOption = SelectStack();
                    if (selectedOption.Value == 0)
                    {
                        break;
                    }
                    var flashCards = _flashCardController.GetAllFlashCard(selectedOption);
                    if (flashCards.Count == 0)
                    {
                        Console.Clear();
                        RenderEmptyFlashCardResult();
                        Console.ReadLine();
                    }
                    else
                    {
                        StudySession(flashCards, selectedOption);
                    }
                }while(true);


                // StudySession(flashCards, stacks);
                break;
            case 4:
                break;
            case 0:
                return true;
        }
        return false;

    }

    private void RenderEmptyFlashCardResult()
    {
        Panel nullPanel = new Panel(new Markup("[red bold]FLASH-CARD TABLE IS EMPTY!!![/]"))
                .Border(BoxBorder.Heavy)
                .BorderColor(Color.IndianRed1_1)
                .Padding(1, 1, 1, 1)
                .Header("Result");


        AnsiConsole.Write(nullPanel);
    }



    private Option SelectStack()
    {
        var stacks = _stackController.GetAllStacks();
        List<Option> listOfStack = new List<Option>();

        foreach (var stack in stacks)
        {
            listOfStack.Add(new Option(stack.Name, stack.Id));
        }

        listOfStack.Add(new Option("Go back to menu", 0));
        return _validation.ChooseOption(listOfStack, "CHOOSE A STACK", "STUDY - SESSION");
    }
    private void StudySession(List<FlashCard> flashCards, Option selectedStack)
    {
        var random = new Random();
        int index = 0;

        int score = 0;
        int sessionCount = 0;
        int rightAnswer = 0;
        do
        {
            Console.Clear();
            index = random.Next(0, flashCards.Count);
            var randFlashCard = flashCards[index];
            _visual.RenderQuestion(randFlashCard, selectedStack);

            Console.WriteLine("\n");

            var answer = _validation.ValidateUserAnswer();

            if (answer == null)
            {
                Console.WriteLine("\n");
                AnsiConsole.MarkupLine("[cyan bold]Exiting Study Session.[/]");
                AnsiConsole.MarkupLine($"[green bold]You got [cyan]{rightAnswer}[/] out of [blue]{sessionCount}[/][/]");
                AnsiConsole.MarkupLine($"[bold]Your total score: {score}[/]");

                
                Console.ReadLine();
                if (sessionCount > 0)
                {
                    Console.Clear();
                    int affectedRow = _studyController.CreateStudySession(score, selectedStack.Value);
                    _visual.RenderResult(affectedRow);

                }
                return;
            }

            sessionCount++;
            if (answer.ToLower() == randFlashCard.Back.ToLower())
            {

                score += 5;
                rightAnswer++;
                Console.WriteLine("\n");
                AnsiConsole.Markup($"[green bold]Your answer was correct! {answer}[/]");
            }
            else
            {
                Console.WriteLine("\n");

                AnsiConsole.MarkupLine("[red bold]Your answer was wrong[/]");
                AnsiConsole.MarkupLine($"[yellow bold]You answered: {answer}[/]");
                AnsiConsole.MarkupLine($"[green bold]Correct answer is: {randFlashCard.Back}[/]");
                AnsiConsole.MarkupLine("[bold]Press Enter to continue[/]");
            }

            Console.ReadLine();
        } while (true);


    }

}
