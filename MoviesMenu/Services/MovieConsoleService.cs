﻿using MoviesMenu.Models;
using MoviesMenu.Services;
using MoviesMenu;

internal class MovieConsoleService
{
    private readonly MovieService _movieService;

    // constructor shorthand
    public MovieConsoleService(MovieService movieService) => _movieService = movieService;

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
                break;
        }
    }

    public void AddMovie()
    {
        Console.Clear();

        Console.WriteLine("Enter movie title:");
        string? title = Console.ReadLine();

        Console.WriteLine("Enter director name:");
        string? director = Console.ReadLine();

        Console.WriteLine("Enter genre:");
        string? genre = Console.ReadLine();

        int releaseYear;
        while (true)
        {
            Console.WriteLine("Enter release year:");
            if (int.TryParse(Console.ReadLine(), out releaseYear))
                break;
            
            Console.WriteLine("Invalid input. Please enter a valid release year.");
        }

        double price;
        while (true)
        {
            Console.WriteLine("Enter price:");
            if (double.TryParse(Console.ReadLine(), out price))
                break;

            Console.WriteLine("Invalid input. Please enter a valid price.");
        }

        string? result = _movieService.AddMovie(new Movie(0, title, director, genre, releaseYear, price));
        Console.WriteLine("\n" + result);

        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.B)
                break;
        }
    } // end of AddMovie()


    public void ModifyMovie()
    {
        Console.Clear();
        Console.Write(_movieService.ListAllMovies());

        int id;
        while (true)
        {
            Console.WriteLine("\n\nEnter the ID of the movie to modify:");
            if (int.TryParse(Console.ReadLine(), out id) && _movieService.CheckMovieExists(id))
                break;

            Console.WriteLine("Invalid input or movie doesn't exist. Please enter a valid movie ID.");
        }

        Console.WriteLine("Enter movie title:");
        string? title = Console.ReadLine();

        Console.WriteLine("Enter director name:");
        string? director = Console.ReadLine();

        Console.WriteLine("Enter genre:");
        string? genre = Console.ReadLine();

        int releaseYear;
        while (true)
        {
            Console.WriteLine("Enter release year:");
            if (int.TryParse(Console.ReadLine(), out releaseYear))
                break;

            Console.WriteLine("Invalid input. Please enter a valid release year.");
        }

        double price;
        while (true)
        {
            Console.WriteLine("Enter price:");
            if (double.TryParse(Console.ReadLine(), out price))
                break;

            Console.WriteLine("Invalid input. Please enter a valid price.");
        }

        string results = _movieService.ModifyMovie(new Movie(id, title, director, genre, releaseYear, price));
        Console.WriteLine("\n" + results);

        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.B)            
                break;
        }
    } // end of ModifyMovie()



    public void RemoveMovie()
    {
        Console.Clear();
        Console.Write(_movieService.ListAllMovies());

        Console.WriteLine("\n\nEnter the ID of the movie to delete: ");

        int id;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out id) && _movieService.CheckMovieExists(id))
                break;
            
            Console.WriteLine("Invalid input or movie doesn't exist. Please enter a valid movie ID.");
        }

        string result = _movieService.RemoveMovie(id);
        Console.WriteLine(result);

        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        // press b to go back to main menu
        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.B)
                break;
        }
    } // end of RemoveMovie()

}
