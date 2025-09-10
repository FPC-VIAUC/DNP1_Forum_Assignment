namespace CLI.UI;

public static class MyCliUtils{
    // 0 always means return
    public static async Task<int> GetChoiceAsync(List<string> choices, int? defaultChoice){
        int choice = -1;
        choices.Insert(0, "Return");
        while (choice < 0 || choice > choices.Count){
            for(int i = 0; i < choices.Count; i++){
                Console.WriteLine($"{i}{(defaultChoice != null ? (i == defaultChoice ? "*" : "") : "")}) {choices[i]}");
            }
            Console.Write("Choice: ");

            choice = await ReadIntAsync(defaultChoice);
            if (choice < 0 || choice > choices.Count){
                Console.WriteLine("Not a valid choice...");
            }
        }
        return choice;
    }

    public static async Task<int> GetChoiceAsync(List<string> choices){
        return await GetChoiceAsync(choices, 0);
    }

    public static async Task<int> ReadIntAsync(int? defaultChoice){
        int? num = null;
        while(num == null){
            string input = await ReadStringAsync(defaultChoice?.ToString());
            if (int.TryParse(input, out int inputNum)){
                num = inputNum;
            } else {
                Console.WriteLine("Not a valid integer, try again...");
            }
        }
        return num ?? 0; // num will never be null but this shuts up the compiler
    }

    public static async Task<int> ReadIntAsync(){
        return await ReadIntAsync(null);
    }

    public static async Task<string> ReadStringAsync(string? defaultString){
        string? s = null;
        while (s == null){
            s = Console.ReadLine();
            if (defaultString != null && string.IsNullOrEmpty(s))
                s = defaultString;
            else if (s == null){
                Console.WriteLine("Not a valid string, try again...");
            }
        }
        return s;
    }

    public static async Task<string> ReadStringAsync(){
        return await ReadStringAsync(null);
    }
}