namespace BBTicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new TicTacToeGame().Run();
        }
    }

    public class TicTacToeGame
    {
        public void Run()
        {
            Board board = new Board();
            BoardRenderer renderer = new BoardRenderer();
            Player player1 = new Player(Cell.X);
            Player player2 = new Player(Cell.O);
            int turnNumber = 0;

            Player currentPLayer = player1;

            // after 9 moves if nobody has won, the game is a draw
            while (turnNumber < 9)
            {
                renderer.Draw(board);
                Console.WriteLine($"It is {currentPLayer.Symbol}'s turn");
                Square square = currentPLayer.PickSquare(board);
                board.FillCell(square.Row, square.Column, currentPLayer.Symbol);
                
                if (HasWon(board, Cell.X))
                {
                    renderer.Draw(board);
                    Console.WriteLine("X has Won!");
                    return;
                }          
                else if (HasWon(board, Cell.O))
                { 
                    renderer.Draw(board);
                    Console.WriteLine("O has Won!");
                    return;
                }

                // change to the other player
                currentPLayer = currentPLayer == player1 ? player2 : player1;
                turnNumber++;
            }

        }

        private bool HasWon(Board board, Cell value)
        {
            // Check rows.
            if (board.ContentsOf(0, 0) == value && board.ContentsOf(0, 1) == value && board.ContentsOf(0, 2) == value) return true;
            if (board.ContentsOf(1, 0) == value && board.ContentsOf(1, 1) == value && board.ContentsOf(1, 2) == value) return true;
            if (board.ContentsOf(2, 0) == value && board.ContentsOf(2, 1) == value && board.ContentsOf(2, 2) == value) return true;

            // Check columns.
            if (board.ContentsOf(0, 0) == value && board.ContentsOf(1, 0) == value && board.ContentsOf(2, 0) == value) return true;
            if (board.ContentsOf(0, 1) == value && board.ContentsOf(1, 1) == value && board.ContentsOf(2, 1) == value) return true;
            if (board.ContentsOf(0, 2) == value && board.ContentsOf(1, 2) == value && board.ContentsOf(2, 2) == value) return true;

            // Check diagonals.
            if (board.ContentsOf(0, 0) == value && board.ContentsOf(1, 1) == value && board.ContentsOf(2, 2) == value) return true;
            if (board.ContentsOf(2, 0) == value && board.ContentsOf(1, 1) == value && board.ContentsOf(0, 2) == value) return true;

            return false;
        }
    }

    public class Player
    {
        public Cell Symbol { get; }
        
        public Player(Cell symbol)
        {
            Symbol = symbol;
        }

        public Square PickSquare(Board board)
        {
            while(true)
            {
                Console.WriteLine("What square do you want to play in? ");
                ConsoleKey key = Console.ReadKey().Key;
                Console.WriteLine();

                Square choice = key switch
                {
                    ConsoleKey.NumPad7 => new Square(0, 0),
                    ConsoleKey.NumPad8 => new Square(0, 1),
                    ConsoleKey.NumPad9 => new Square(0, 2),
                    ConsoleKey.NumPad4 => new Square(1, 0),
                    ConsoleKey.NumPad5 => new Square(1, 1),
                    ConsoleKey.NumPad6 => new Square(1, 2),
                    ConsoleKey.NumPad1 => new Square(2, 0),
                    ConsoleKey.NumPad2 => new Square(2, 1),
                    ConsoleKey.NumPad3 => new Square(2, 2)
                };

                if (board.IsEmpty(choice.Row, choice.Column))
                    return choice;
                else
                    Console.WriteLine("That square is already taken.");

            }
        }
    }

    public class BoardRenderer
    {
        public void Draw(Board board)
        {
            char[,] symbols = new char[3, 3];
            symbols[0, 0] = GetCharacterFor(board.ContentsOf(0, 0));
            symbols[0, 1] = GetCharacterFor(board.ContentsOf(0, 1));
            symbols[0, 2] = GetCharacterFor(board.ContentsOf(0, 2));
            symbols[1, 0] = GetCharacterFor(board.ContentsOf(1, 0));
            symbols[1, 1] = GetCharacterFor(board.ContentsOf(1, 1));
            symbols[1, 2] = GetCharacterFor(board.ContentsOf(1, 2));
            symbols[2, 0] = GetCharacterFor(board.ContentsOf(2, 0));
            symbols[2, 1] = GetCharacterFor(board.ContentsOf(2, 1));
            symbols[2, 2] = GetCharacterFor(board.ContentsOf(2, 2));

            Console.WriteLine($" {symbols[0, 0]} | {symbols[0, 1]} | {symbols[0, 2]}");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {symbols[1, 0]} | {symbols[1, 1]} | {symbols[1, 2]}");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {symbols[2, 0]} | {symbols[2, 1]} | {symbols[2, 2]}");        
    }

        private char GetCharacterFor(Cell cell) => cell switch { Cell.X => 'X', Cell.O => 'O', Cell.Empty => ' ' };
    }


    public class Square
    {
        public int Row { get; }
        public int Column { get; }

        public Square(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }

    public class Board
    {
        private readonly Cell[,] _cells = new Cell[3, 3];

        public Cell ContentsOf(int row, int column) => _cells[row, column];
        public void FillCell(int row, int column, Cell value) => _cells[row, column] = value;
        public bool IsEmpty(int row, int column) => _cells[row, column] == Cell.Empty;
    }

}

public enum Cell { Empty, X, O }