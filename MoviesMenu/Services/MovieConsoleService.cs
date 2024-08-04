using MoviesMenu.Models;
using MoviesMenu.Services;
using MoviesMenu;

internal class MovieConsoleService
{
    private readonly MovieService _movieService;

    public MovieConsoleService(MovieService movieService)
    {
        _movieService = movieService;
    }

    public void DisplayMenu(List<Option> menuOptions, int selectedIndex)
    {
        Menu.WriteMenu(menuOptions, menuOptions[selectedIndex]);
    }

    public void HandleMenuSelection(List<Option> menuOptions)
    {
        int index = 0;
        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (index + 1 < menuOptions.Count)
                {
                    index++;
                    DisplayMenu(menuOptions, index);
                }
            }
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                if (index - 1 >= 0)
                {
                    index--;
                    DisplayMenu(menuOptions, index);
                }
            }
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                menuOptions[index].Selected.Invoke();
                index = 0;
                DisplayMenu(menuOptions, index); // Redisplay menu after an option is executed
            }
        }
        while (keyInfo.Key != ConsoleKey.X);
    }

    public void ListAllMovies()
    {
        Console.Clear();
        Console.Write(_movieService.ListAllMovies());
        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.B)
            {
                break;
            }
        }
    }

    public void AddMovie()
    {
        Console.Clear();

        // Title
        Console.WriteLine("Enter movie title:");
        string? title = Console.ReadLine();

        // Director
        Console.WriteLine("Enter director name:");
        string director = Console.ReadLine();

        // Genre
        Console.WriteLine("Enter genre:");
        string genre = Console.ReadLine();

        // Release Year with type validation
        Console.WriteLine("Enter release year:");
        string releaseYear = Console.ReadLine();

        int releaseYearInputValue;
        bool releaseYearTypeSuccess = int.TryParse(releaseYear, out releaseYearInputValue);
        while (!releaseYearTypeSuccess)
        {
            Console.WriteLine($"Invalid Input. Enter release year again");
            releaseYear = Console.ReadLine();
            releaseYearTypeSuccess = int.TryParse(releaseYear, out releaseYearInputValue);
        }
        releaseYearInputValue = int.Parse(releaseYear);


        // Price with type validation
        Console.WriteLine("Enter price:");
        string price = Console.ReadLine();
        double priceInputValue;
        bool priceTypeSuccess = double.TryParse(price, out priceInputValue);
        while (!priceTypeSuccess)
        {
            Console.WriteLine($"Invalid Input. Enter price again");
            price = Console.ReadLine();
            priceTypeSuccess = double.TryParse(price, out priceInputValue);
        }
        priceInputValue = double.Parse(price);

        string results = _movieService.AddMovie(new Movie(0, title, director, genre, releaseYearInputValue, priceInputValue));
        Console.WriteLine("\n" + results);

        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        // press b to go back to main menu
        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.B)
            {
                break;
            }
        }
    } // end of AddMovie()

    public void ModifyMovie()
    {
        Console.Clear();
        Console.Write(_movieService.ListAllMovies());

        Console.WriteLine("\n\nEnter the ID of the movie to modify:");
        int id = int.Parse(Console.ReadLine());
        while (!_movieService.CheckMovieExists(id))
        {
            Console.WriteLine("\n\nMovie doesn't exist in list, enter a valid Id");
            id = int.Parse(Console.ReadLine());
        }

        // Title
        Console.WriteLine("Enter movie title:");
        string title = Console.ReadLine();

        // Director
        Console.WriteLine("Enter director name:");
        string director = Console.ReadLine();

        // Genre
        Console.WriteLine("Enter genre:");
        string genre = Console.ReadLine();

        // Release Year with type validation
        Console.WriteLine("Enter release year:");
        string releaseYear = Console.ReadLine();

        int releaseYearInputValue;
        bool releaseYearTypeSuccess = int.TryParse(releaseYear, out releaseYearInputValue);
        while (!releaseYearTypeSuccess)
        {
            Console.WriteLine($"Invalid Input. Enter release year again");
            releaseYear = Console.ReadLine();
            releaseYearTypeSuccess = int.TryParse(releaseYear, out releaseYearInputValue);
        }
        releaseYearInputValue = int.Parse(releaseYear);

        // price with type validation
        Console.WriteLine("Enter price:");
        string price = Console.ReadLine();
        double priceInputValue;
        bool priceTypeSuccess = double.TryParse(price, out priceInputValue);
        while (!priceTypeSuccess)
        {
            Console.WriteLine($"Invalid Input. Enter price again");
            price = Console.ReadLine();
            priceTypeSuccess = double.TryParse(price, out priceInputValue);
        }
        priceInputValue = double.Parse(price);

        string results = _movieService.ModifyMovie(new Movie(id, title, director, genre, releaseYearInputValue, priceInputValue));
        Console.WriteLine("\n" + results);

        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        // press b to go back to main menu
        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.B)
            {
                break;
            }
        }
    } // end of ModifyMovie()

    public void RemoveMovie()
    {
        Console.Clear();
        Console.Write(_movieService.ListAllMovies());

        Console.WriteLine("\n\nEnter the ID of the movie to delete: ");

        int id = int.Parse(Console.ReadLine());
        while (!_movieService.CheckMovieExists(id))
        {
            Console.WriteLine("\n\nMovie doesn't exist in list, enter a valid Id");
            id = int.Parse(Console.ReadLine());
        }

        string result = _movieService.RemoveMovie(id);
        Console.WriteLine(result);

        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        // press b to go back to main menu
        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.B)
            {
                break;
            }
        }
    } // end of removeMovie()
}
