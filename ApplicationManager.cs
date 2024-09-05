using System;
using System.Xml.Serialization;
using Flash_Cards.Lawang.Controller;
using Flash_Cards.Lawang.Models;
using Flash_Cards.Lawang.Views;

namespace Flash_Cards.Lawang;

public class ApplicationManager
{
    private Visualize _visual;
    private Validation _validation;
    private StackController _stackController;
    private FlashCardController _flashCardController;
    public ApplicationManager(Visualize visual, Validation validation, StackController stackController, FlashCardController flashCardController)
    {
        _visual = visual;
        _validation = validation;
        _stackController = stackController;
        _flashCardController = flashCardController;
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
                break;
            case 4:
                break;
            case 0:
                return true;
        }
        return false;

    }
   
}
