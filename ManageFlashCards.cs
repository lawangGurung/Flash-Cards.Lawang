using System;
using Flash_Cards.Lawang.Controller;
using Flash_Cards.Lawang.Models;
using Flash_Cards.Lawang.Views;

namespace Flash_Cards.Lawang;

public class ManageFlashCards
{

    private Validation _validation;
    private Visualize _visual;
    private FlashCardController _flashCardController;
    public ManageFlashCards(Validation validation, Visualize visual, FlashCardController flashCardController)
    {
        _validation = validation;
        _visual = visual;
        _flashCardController = flashCardController;
    }

    public void OperationMenu()
    {
       List<Option> listOfOption = new List<Option>()
       {
            new Option("Show Flash-Card", 1),
            new Option("Create Flash-Card", 2),
            new Option("Update Flash-Card", 3),
            new Option ("Delete Flash-Card", 4),
            new Option("Exit", 5)
       };

       bool exitOption = false; 
       do
       {
            var option = _validation.ChooseOption(listOfOption, "FLASH-CARD OPTION");
            switch (option.Value)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    exitOption = true;
                    break;
            }
            Console.Clear();

       }while(!exitOption);
    }
}
