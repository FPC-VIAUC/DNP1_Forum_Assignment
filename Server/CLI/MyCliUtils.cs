namespace CLI.UI;

public static class MyCliUtils{
    // 0 always means return
    public static async Task<int> GetChoiceAsync(List<string> choices, int? defaultChoice){
        choices.Insert(0, "Return");
        for(int i = 0; i < choices.Count; i++){
            Console.WriteLine($"{i}{(defaultChoice != null ? (i == defaultChoice ? "*" : "") : "")}) {choices[i]}");
        }
        
        int choice = -1;
        while (choice < 0 || choice >= choices.Count){
            choice = await ReadIntAsync("Choice: ", defaultChoice);
            if (choice < 0 || choice >= choices.Count){
                Console.WriteLine("Not a valid choice, try again...");
            }
        }
        
        return choice;
    }

    public static async Task<int> GetChoiceAsync(List<string> choices){
        return await GetChoiceAsync(choices, 0);
    }

    public static async Task<int> ReadIntAsync(string? hint, int? defaultChoice){
        int? num = null;
        while(num == null){
            string input = await ReadStringAsync(hint, defaultChoice?.ToString());
            if (int.TryParse(input, out int inputNum)){
                num = inputNum;
            } else {
                Console.WriteLine("Not a valid integer, try again...");
            }
        }
        return num ?? 0; // num will never be null but this shuts up the compiler
    }
    
    public static async Task<int> ReadIntAsync(string? hint){
        return await ReadIntAsync(hint, null);
    }

    public static async Task<int> ReadIntAsync(){
        return await ReadIntAsync(null, null);
    }

    public static async Task<string> ReadStringAsync(string? hint, string? defaultString){
        string? s = null;
        while (s == null){
            Console.Write(hint ?? "");
            s = Console.ReadLine();
            if (defaultString != null && string.IsNullOrEmpty(s))
                s = defaultString;
            else if (s == null){
                Console.WriteLine("Not a valid string, try again...");
            }
        }
        return s;
    }

    public static async Task<string> ReadStringAsync(string? hint){
        return await ReadStringAsync(hint, null);
    }

    public static async Task<string> ReadStringAsync(){
        return await ReadStringAsync(null, null);
    }
}