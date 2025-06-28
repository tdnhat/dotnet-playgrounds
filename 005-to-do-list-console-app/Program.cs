using _005_to_do_list_console_app.Services;

var todoService = new TodoService("todos.json");

// Example usage
var newItem = await todoService.CreateAsync("Learn C#", "Study the basics of C#");
Console.WriteLine($"Created Todo: {newItem.Title}");

var allItems = await todoService.GetAllAsync();
Console.WriteLine("All Todos:");
foreach (var item in allItems)
{
    Console.WriteLine(item.ToString());
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
