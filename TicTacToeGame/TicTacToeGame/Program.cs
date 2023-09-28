using System;

namespace TicTacToe
{
    class Program
    {
        static char[,] board = new char[3, 3]; // the Tic-Tac-Toe board
        static char currentPlayer = 'X'; // the current player, either X or O

        static void Main(string[] args)
        {
            InitializeBoard(); // initialize the board with empty spaces
            DrawBoard(); // draw the board

            while (!IsGameOver())
            {
                Console.Write("Player {0}'s turn. Enter row and column (e.g. 1,2): ", currentPlayer);
                string[] input = Console.ReadLine().Split(',');
                int row = int.Parse(input[0]) - 1; // subtract 1 to convert to 0-based index
                int col = int.Parse(input[1]) - 1;

                if (IsValidMove(row, col))
                {
                    MakeMove(row, col);
                    DrawBoard();
                    if (IsGameOver())
                    {
                        Console.WriteLine("Game over. Player {0} wins!", currentPlayer);
                    }
                    else
                    {
                        SwitchPlayer(); // switch to the other player's turn
                        Console.WriteLine("Computer's turn...");
                        MakeComputerMove();
                        DrawBoard();
                        if (IsGameOver())
                        {
                            Console.WriteLine("Game over. Computer wins!");
                        }
                        else
                        {
                            SwitchPlayer(); // switch back to the player's turn
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move. Please enter a valid row and column.");
                }
            }
        }

        static void InitializeBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        static void DrawBoard()
        {
            Console.Clear(); // clear the console
            Console.WriteLine("  1 2 3");
            Console.WriteLine(" -------");
            for (int row = 0; row < 3; row++)
            {
                Console.Write(row + 1 + "|");
                for (int col = 0; col < 3; col++)
                {
                    Console.Write(board[row, col] + "|");
                }
                Console.WriteLine();
                Console.WriteLine(" -------");
            }
        }

        static bool IsGameOver()
        {
            // check rows
            for (int row = 0; row < 3; row++)
            {
                if (board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2] && board[row, 0] != ' ')
                {
                    return true;
                }
            }

            // check columns
            for (int col = 0; col < 3; col++)
            {
                if (board[0, col] == board[1, col] && board[1, col] == board[2, col] && board[0, col] != ' ')
                {
                    return true;
                }
            }

            // check diagonals
            if ((board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != ' ') ||
                (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != ' '))
            {
                return true;
            }

            // check for a tie game
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        return false; // there are still empty spaces, so the game is not over
                    }
                }
            }

            // if we get here, there are no empty spaces and nobody has won, so it's a tie game
            Console.WriteLine("Game over. It's a tie!");
            return true;
        }

        static bool IsValidMove(int row, int col)
        {
            if (row < 0 || row > 2 || col < 0 || col > 2)
            {
                return false; // out of bounds
            }
            else if (board[row, col] != ' ')
            {
                return false; // already occupied
            }
            else
            {
                return true;
            }
        }

        static void MakeMove(int row, int col)
        {
            board[row, col] = currentPlayer;
        }

        static void SwitchPlayer()
        {
            if (currentPlayer == 'X')
            {
                currentPlayer = 'O';
            }
            else
            {
                currentPlayer = 'X';
            }
        }

        static void MakeComputerMove()
        {
            Random rand = new Random();
            int row, col;
            do
            {
                row = rand.Next(3);
                col = rand.Next(3);
            } while (!IsValidMove(row, col));

            MakeMove(row, col);
        }
    }
}
