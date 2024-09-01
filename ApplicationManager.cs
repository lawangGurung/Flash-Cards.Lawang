using System;
using Flash_Cards.Lawang.Views;

namespace Flash_Cards.Lawang;

public class ApplicationManager
{
    private Visualize _visual;
    public ApplicationManager()
    {
       _visual = new Visualize(); 
    }

    public void MainMenu()
    {
        _visual.ShowTitle("Flash-Cards");
    }
}
