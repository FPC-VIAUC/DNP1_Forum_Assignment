namespace CLI.UI;

public class ConsoleMenuItem{
    public string MenuText{ get; set; }
    public ConsoleView? View{ get; set; }

    public ConsoleMenuItem(string menuText, ConsoleView? view){
        MenuText = menuText;
        View = view;
    }
}