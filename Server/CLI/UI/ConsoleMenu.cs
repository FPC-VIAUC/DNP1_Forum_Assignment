namespace CLI.UI;

public class ConsoleMenu : ConsoleView{
    private List<ConsoleMenuItem> menuItems;
    private int? defaultChoice;
    
    public ConsoleMenu() : this(new List<ConsoleMenuItem>()){}

    public ConsoleMenu(List<ConsoleMenuItem> menuItems, int? defaultChoice = 0){
        menuItems.Insert(0, new ConsoleMenuItem("Return", null));
        this.menuItems = menuItems;
        this.defaultChoice = defaultChoice;
    }

    protected void AddMenuItem(ConsoleMenuItem menuItem){
        menuItems.Add(menuItem);
    }
    
    protected void AddMenuItems(List<ConsoleMenuItem> menuItems){
        this.menuItems.AddRange(menuItems);
    }

    public override async Task ShowViewAsync(){
        int choice = -1;
        while (choice != 0){
            choice = -1;
            Console.WriteLine();
            for(int i = 0; i < menuItems.Count; i++){
                Console.WriteLine($"{i}{(defaultChoice != null ? (i == defaultChoice ? "*" : "") : "")}) {menuItems[i].MenuText}");
            }
            
            while (choice < 0 || choice >= menuItems.Count){
                choice = await ReadIntAsync("Choice: ", defaultChoice);
                if (choice < 0 || choice >= menuItems.Count){
                    Console.WriteLine("Not a valid choice, try again...");
                }
            }

            ConsoleView? view = menuItems[choice].View;
            if (view != null && choice != 0){
                Console.WriteLine();
                await menuItems[choice].View.ShowViewAsync();
            }
        }
    }
}