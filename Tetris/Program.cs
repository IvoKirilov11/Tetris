class Program
{
    static int TetrisRows = 20;
    static int TetrisCols = 10;
    static int InfoCols = 10;
    static int ConsoleRows = 1 + TetrisRows + 1;
    static int ConsoleCols = 1 + TetrisCols + 1 + InfoCols + 1;
    static List<bool[,]> TetrisFigures = new List<bool[,]>()
    {

    };



    // State
    static int Score = 0;
    static int Frame = 0;
    static int FramesToMoveFigure = 15;
    static int CurrentFigureIndex = 2;
    static int CurrentFigureRow = 0;
    static int CurrentFigureCol = 0;
    static bool[,] TetrisField = new bool[TetrisRows, TetrisCols];

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
                    // TODO: Move current figure left
                    CurrentFigureCol--; // TODO: Out of range
                }
                if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                {
                    // TODO: Move current figure right
                    CurrentFigureCol++; // TODO: Out of range
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
            // user input
            // change state

            // Redraw UI
            DrawBorder();
            DrawInfo();
            Frame++;
            Thread.Sleep(40);
        }


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
        Write("Score:", 1, 3 + TetrisCols);
        Write(Score.ToString(), 2, 3 + TetrisCols);
        Write("Frame:", 4, 3 + TetrisCols);
        Write(Frame.ToString(), 5, 3 + TetrisCols);

    }

    static void Write(string text, int row, int col, ConsoleColor color = ConsoleColor.Green)
    {
        Console.ForegroundColor = color;
        Console.SetCursorPosition(col, row);
        Console.WriteLine(text);
        Console.ResetColor();
    }

}