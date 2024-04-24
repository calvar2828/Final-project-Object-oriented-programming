using System;

class Board
{
    private char[,] board;
    public int Rows { get; private set; }
    public int Cols { get; private set; }

    public Board(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        board = new char[rows, cols];
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                board[i, j] = ' ';
            }
        }
    }

    public void Print()
    {
        Console.WriteLine();
        Console.WriteLine("    1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18  19  20  21");
        Console.WriteLine("  +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+");
        for (int i = 0; i < Rows; i++)
        {
            Console.Write($"  |");
            for (int j = 0; j < Cols; j++)
            {
                Console.Write($" {board[i, j]} |");
            }
            Console.WriteLine();
            Console.WriteLine("  +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+");
        }
    }

    public bool PlacePiece(int row, int col, char piece)
    {
        if (row < 0 || row >= Rows || col < 0 || col >= Cols || board[row, col] != ' ')
            return false;

        board[row, col] = piece;
        return true;
    }

    public char GetPiece(int row, int col)
    {
        if (row < 0 || row >= Rows || col < 0 || col >= Cols)
        {
            throw new IndexOutOfRangeException("row or column index is out of range");
        }

        return board[row, col];
    }

    public bool IsFull()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                if (board[i, j] == ' ')
                    return false;
            }
        }
        return true;
    }

    public bool CheckWin(char piece)
    {
        return CheckHorizontal(piece) || CheckVertical(piece) || CheckDiagonal(piece);
    }

    private bool CheckHorizontal(char piece)
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j <= Cols - 4; j++)
            {
                if (board[i, j] == piece &&

                    board[i, j + 1] == piece &&

                    board[i, j + 2] == piece &&
                    board[i, j + 3] == piece)
                    return true;
            }
        }
        return false;
    }

    private bool CheckVertical(char piece)
    {
        for (int j = 0; j < Cols; j++)
        {
            for (int i = 0; i <= Rows - 4; i++)
            {
                if (board[i, j] == piece &&
                    board[i + 1, j] == piece &&
                    board[i + 2, j] == piece &&

                    board[i + 3, j] == piece)


                    return true;



            }
        }

        return false;
    }

    private bool CheckDiagonal(char piece)
    {
        for (int i = 0; i <= Rows - 4; i++)
        {

            for (int j = 0; j <= Cols - 4; j++)
            {
                if (board[i, j] == piece &&
                    board[i + 1, j + 1] == piece &&
                    board[i + 2, j + 2] == piece &&
                    board[i + 3, j + 3] == piece)
                    return true;


                if (board[i, j + 3] == piece &&
                    board[i + 1, j + 2] == piece &&

                    board[i + 2, j + 1] == piece &&
                    board[i + 3, j] == piece)



                    return true;
            }
        }
        return false;
    }
}

class Player
{
    public char Piece { get; private set; }

    public Player(char piece)
    {
        Piece = piece;
    }
}

class ConnectFour
{
    private Board board;
    private Player[] players;
    private Player currentPlayer;

    public Player CurrentPlayer => currentPlayer;

    public ConnectFour(int rows, int cols)
    {
        board = new Board(rows, cols);
        players = new Player[2] { new Player('X'), new Player('O') };
        currentPlayer = players[0]; // Player X starts
    }

    public void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == players[0]) ? players[1] : players[0];
    }

    public bool PlayMove(int col)
    {
        int row = FindEmptyRow(col);
        if (row == -1)
        {
            return false;
        }
        board.PlacePiece(row, col, currentPlayer.Piece);
        return true;
    }

    private int FindEmptyRow(int col)
    {
        for (int row = board.Rows - 1; row >= 0; row--)
        {
            if (board.GetPiece(row, col) == ' ')
            {
                return row;
            }
        }
        return -1;
    }

    public bool CheckWin()
    {
        return board.CheckWin(currentPlayer.Piece);
    }

    public bool IsBoardFull()
    {
        return board.IsFull();
    }

    public void PrintBoard()
    {
        board.Print();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("This is the carlos alvarez's final project! press any key to continue.");
        Console.ReadKey();

        int rows, cols;
        char choice;

        do
        {
            Console.WriteLine("Choose the size of the board:");
            Console.WriteLine("1. 6x7");
            Console.WriteLine("2. 6x14");
            Console.WriteLine("3. 6x21");
            Console.Write("Enter your choice (1-3): ");
            choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (choice)
            {
                case '1':
                    rows = 6;
                    cols = 7;
                    break;
                case '2':
                    rows = 6;
                    cols = 14;
                    break;
                case '3':
                    rows = 6;
                    cols = 21;
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please enter 1, 2, or 3");
                    continue;
            }

            ConnectFour game = new ConnectFour(rows, cols);
            bool gameOver = false;

            while (!gameOver)
            {
                Console.Clear();
                game.PrintBoard();
                Console.WriteLine($"Player {game.CurrentPlayer.Piece}'s turn");

                int col;
                bool validInput;
                do
                {
                    Console.Write($"enter column number (1-{cols}): ");
                    validInput = int.TryParse(Console.ReadLine(), out col);
                    if (!validInput || col < 1 || col > cols)
                        Console.WriteLine($"invalid input! Please enter a number between 1 and {cols}");
                } while (!validInput || col < 1 || col > cols);

                if (game.PlayMove(col - 1))
                {
                    if (game.CheckWin())
                    {
                        Console.Clear();
                        game.PrintBoard();
                        Console.WriteLine($"Player {game.CurrentPlayer.Piece} wins!!!");
                        gameOver = true;
                    }
                    else if (game.IsBoardFull())
                    {
                        Console.Clear();
                        game.PrintBoard();
                        Console.WriteLine("It's a draw!!!");
                        gameOver = true;
                    }
                    else
                    {
                        game.SwitchPlayer();
                    }
                }
                else
                {
                    Console.WriteLine("Column is full! Please choose another column");
                }
            }

            Console.Write("Do you want to play again? (y/n): ");
            choice = Console.ReadKey().KeyChar;
            Console.WriteLine();
        } while (choice == 'y' || choice == 'Y');
    }
}
