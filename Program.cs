using System;
using System.Collections.Generic;

class Character
{
    private int energy;  
    private Tuple<int, int> position;  

    public Character()
    {
        energy = 500;  
        position = new Tuple<int, int>(0, 0);  
    }

    public int GetEnergy()
    {
        return energy;  
    }

    public Tuple<int, int> GetPosition()
    {
        return position;  
    }

    public void Move(int x, int y)
    {
        energy -= 1;  
        position = new Tuple<int, int>(position.Item1 + x, position.Item2 + y);  
    }

    public void CollectCoffeeCup()
    {
        energy += 25;  
    }
}

class Game
{
    static void Main()
    {
        Character character = new Character();  

        List<Tuple<int, int>> coffeeCups = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(2, 3),  
            new Tuple<int, int>(5, 7)   
            
        };

        int mazeSize = 10;  
        char[,] maze = GenerateMaze(mazeSize);  

        while (character.GetEnergy() > 0)
        {
            Console.Clear();
            PrintMaze(maze, character.GetPosition());  
            Console.WriteLine("Энергия: " + character.GetEnergy());
            Console.WriteLine("Позиция: (" + character.GetPosition().Item1 + ", " + character.GetPosition().Item2 + ")");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (IsValidMove(maze, character.GetPosition().Item1, character.GetPosition().Item2 - 1))
                        character.Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    if (IsValidMove(maze, character.GetPosition().Item1, character.GetPosition().Item2 + 1))
                        character.Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    if (IsValidMove(maze, character.GetPosition().Item1 - 1, character.GetPosition().Item2))
                        character.Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    if (IsValidMove(maze, character.GetPosition().Item1 + 1, character.GetPosition().Item2))
                        character.Move(1, 0);
                    break;
            }

            
            Tuple<int, int> characterPosition = character.GetPosition();
            for (int i = 0; i < coffeeCups.Count; i++)
            {
                Tuple<int, int> coffeeCup = coffeeCups[i];
                if (coffeeCup.Item1 == characterPosition.Item1 && coffeeCup.Item2 == characterPosition.Item2)
                {
                    character.CollectCoffeeCup();  
                    coffeeCups.RemoveAt(i);  
                    break;  
                }
            }
        }

        Console.Clear();
        PrintMaze(maze, character.GetPosition());  
        Console.WriteLine("ГАМ ОВЕР");  
    }

    static char[,] GenerateMaze(int size)
    {
        char[,] maze = new char[size, size];

        

        return maze;
    }

    static void PrintMaze(char[,] maze, Tuple<int, int> characterPosition)
    {
        int size = maze.GetLength(0);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (characterPosition.Item1 == x && characterPosition.Item2 == y)
                    Console.Write("\u263A");  
                else
                    Console.Write(maze[x, y]);  
            }
            Console.WriteLine();
        }
    }

    static bool IsValidMove(char[,] maze, int x, int y)
    {
        int size = maze.GetLength(0);

        if (x < 0 || x >= size || y < 0 || y >= size)  // Проверка выхода за границы лабиринта
            return false;

        if (maze[x, y] == '#')  // Проверка стены
            return false;

        return true;
    }
}