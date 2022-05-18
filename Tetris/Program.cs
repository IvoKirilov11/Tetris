class Program
{
    static int TetrisRows = 20;
    static int TetrisCols = 10;
    static int InfoCols = 10;
    static int ConsoleRows = 1 + TetrisRows + 1;
    static int ConsoleCols = 1 + TetrisCols + 1 + InfoCols + 1;
    static List<bool[,]> TetrisFigures = new List<bool[,]>()
    {
                new bool[,] // ----
                {
                    { true, true, true, true }
                },
                new bool[,] // O
                {
                    { true, true },
                    { true, true }
                },
                new bool[,] // T
                {
                  { false, true, false },
                  { true, true, true },
                },
                new bool[,] // S
                {
                    { false, true, true, },
                    { true, true, false, },
                },
                new bool[,] // Z
                {
                    { true, true, false },
                    { false, true, true },
                },
                new bool[,] // J
                {
                    { true, false, false },
                    { true, true, true }
                },
                new bool[,] // L
                {
                    { false, false, true },
                    { true, true, true }
                },
    };



    // State
    static int HighScore = 0;
    static int Score = 0;
    static int Frame = 0;
    static int Level = 1;
    static int FramesToMoveFigure = 16;
    static bool[,] CurrentFigure = null;
    static int CurrentFigureRow = 0;
    static int CurrentFigureCol = 0;
    static bool[,] TetrisField = new bool[TetrisRows, TetrisCols];
    static Random Random = new Random();

    static void Main(string[] args)
    {
        Console.Title = "Tetris v1.0";
        Console.CursorVisible = false;
        Console.WindowHeight = ConsoleRows + 1;
        Console.WindowWidth = ConsoleCols;
        Console.BufferHeight = ConsoleRows + 1;
        Console.BufferWidth = ConsoleCols;



        while (true)
        {

            Frame++;
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.Escape)
                {
                    // Environment.Exit(0);
                    return; // Becase of Main()
                }
                if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                {
                    if (CurrentFigureCol >= 1)
                    {
                        CurrentFigureCol--;
                    }

                }
                if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                {
                    if (CurrentFigureCol < TetrisCols - CurrentFigure.GetLength(1))
                    {
                        CurrentFigureCol++;
                    }
                }
                if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                {
                    Frame = 1;
                    Score++;
                    CurrentFigureRow++;
                    // TODO: Move current figure down
                }
                if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                {
                    // TODO: Implement 90-degree rotation of the current figure
                }
            }
            if(Frame % FramesToMoveFigure == 0)
            {
                CurrentFigureRow++;
                Frame = 0;
            }
            if (Collision(CurrentFigure))
            {
                AddCurrentFigureToTetrisField();
                CurrentFigure = TetrisFigures[Random.Next(0, TetrisFigures.Count)];
                CurrentFigureRow = 0;
                CurrentFigureCol = 0;
            }

                // Redraw UI
            DrawBorder();
            DrawInfo();
            DrawTetrisField();
            DrawCurrentFigure();
            
            Thread.Sleep(40);
        }

    }

    static void AddCurrentFigureToTetrisField()
    {
        for (int row = 0; row < CurrentFigure.GetLength(0); row++)
        {
            for (int col = 0; col < CurrentFigure.GetLength(1); col++)
            {
                if (CurrentFigure[row, col])
                {
                    TetrisField[CurrentFigureRow + row, CurrentFigureCol + col] = true;
                }
            }
        }
    }

    static bool Collision(bool[,] figure)
    {
        if (CurrentFigureCol > TetrisCols - figure.GetLength(1))
        {
            return true;
        }
        if (CurrentFigureRow + CurrentFigure.GetLength(0) == TetrisRows)
        {
            return true;
        }
        for (int row = 0; row < figure.GetLength(0); row++)
        {
            for (int col = 0; col < figure.GetLength(1); col++)
            {
                if (figure[row, col] &&
                    TetrisField[CurrentFigureRow + row + 1, CurrentFigureCol + col])
                {
                    return true;
                }

            }
        }
        return false;
    }

    static void DrawBorder()
    {
        Console.SetCursorPosition(0, 0);
        string line = "╔";
        line += new string('═', TetrisCols);
        /* for (int i = 0; i < TetrisCols; i++)
        {
            line += "═";
        } */

        line += "╦";
        line += new string('═', InfoCols);
        line += "╗";
        Console.Write(line);

        for (int i = 0; i < TetrisRows; i++)
        {
            string middleLine = "║";
            middleLine += new string(' ', TetrisCols);
            middleLine += "║";
            middleLine += new string(' ', InfoCols);
            middleLine += "║";
            Console.Write(middleLine);
        }
        string endLine = "╚";
        endLine += new string('═', TetrisCols);
        endLine += "╩";
        endLine += new string('═', InfoCols);
        endLine += "╝";
        Console.Write(endLine);
    }
    static void DrawInfo()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
        }

        Write("Score:", 1, 3 + TetrisCols);
        Write(Score.ToString(), 2, 3 + TetrisCols);
        Write("Frame:", 4, 3 + TetrisCols);
        Write(Frame.ToString(), 5, 3 + TetrisCols);
        Write("Position:", 13, 3 + TetrisCols);
        Write($"{CurrentFigureRow}, {CurrentFigureCol}", 14, 3 + TetrisCols);
        Write("Keys:", 16, 3 + TetrisCols);
        Write($"  ^ ", 18, 3 + TetrisCols);
        Write($"<   > ", 19, 3 + TetrisCols);
        Write($"  v  ", 20, 3 + TetrisCols);

    }

    static void DrawTetrisField()
    {
        for (int row = 0; row < TetrisField.GetLength(0); row++)
        {
            string line = "";
            for (int col = 0; col < TetrisField.GetLength(1); col++)
            {
                if (TetrisField[row, col])
                {
                    line += "*";
                }
                else
                {
                    line += " ";
                }
            }

            Write(line, row + 1, 1);
        }
    }
    static void DrawCurrentFigure()
    {
        
        for (int row = 0; row < CurrentFigure.GetLength(0); row++)
        {
            for (int col = 0; col < CurrentFigure.GetLength(1); col++)
            {
                if (CurrentFigure[row,col])
                {
                    Write("*", row + 1 + CurrentFigureRow, col + 1 + CurrentFigureCol);
                }
            }
        }
    }
    static void Write(string text, int row, int col, ConsoleColor color = ConsoleColor.Green)
    {
        Console.ForegroundColor = color;
        Console.SetCursorPosition(col, row);
        Console.WriteLine(text);
        Console.ResetColor();
    }

}