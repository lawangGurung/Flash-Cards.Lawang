using System;
using Flash_Cards.Lawang.Controller;
using Flash_Cards.Lawang.Models;
using Flash_Cards.Lawang.Views;

namespace Flash_Cards.Lawang;

public class ApplicationManager
{
    private Visualize _visual;
    private Validation _validation;
    private CodingController _codingController;
    public ApplicationManager(Visualize visual, Validation validation, CodingController codingController)
    {
        _visual = visual;
        _validation = validation;
        _codingController = codingController;
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

            var option = _validation.ChooseOption(listOfOptions, "Menu Options");
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
                ManageStacks();
                break;

            case 2:
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

    private void ManageStacks()
    {
        List<Option> listOfOption = new List<Option>()
        {
            new Option("Create Stack.", 1),
            new Option("Update Stack.", 2),
            new Option("Delete Stack.", 3),
            new Option("Exit.", 4)
        };
        bool exitOption = false;
        do
        {
            var option = _validation.ChooseOption(listOfOption, "STACK OPTIONS");
            switch (option.Value)
            {
                case 1:
                    CreateStack();


                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    exitOption = true;
                    break;
            }
            Console.Clear();
        } while (!exitOption);

    }

    private void CreateStack()
    {
        string? userInput = _validation.ValidateStackName();
        if (userInput == null)
        {
            return;
        }

        int rowsAffected = _codingController.CreateStack(userInput);

        _visual.RenderResult(rowsAffected); 
    }
}
