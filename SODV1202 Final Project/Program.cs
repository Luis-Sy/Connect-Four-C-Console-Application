namespace SODV1202_Final_Project
{
    internal class Program
    {

        public static class ConnectFour
        {
            
            private static List<Player> _PlayerList { get; set; }
            public static Connect4GameBoard GameBoard;

            public static void NewGame(List<Player> players) // game initialization for two players
            {
                GameBoard = new Connect4GameBoard();
                // if a previous game has already been played, clear the player list
                _PlayerList = new List<Player>();
                _PlayerList = players;

                // set default tokens
                players[0].Token = new Token("O");
                players[1].Token = new Token("X");
            }

            public static void Play() // main game loop
            {

                GameBoard.DisplayBoard();
                int currentplayer = 0;
                bool win = false;
                bool tie = false;
                do
                {
                    for (int i = 0; i < _PlayerList.Count; i++)
                    {

                        currentplayer = i;
                        // initialize input variable and write who's turn it is
                        string input;
                        Console.WriteLine($"\n{_PlayerList[i]}'s ({_PlayerList[i].Token}) turn\nPlace a piece");
                        // place a piece, repeat for valid input
                        while (true)
                        {
                            if (_PlayerList[i] is HumanPlayer) // if the current player is human
                            {
                                input = Console.ReadLine();
                                // filter out invalid inputs
                                if (input != string.Empty && int.TryParse(input, out _) != false && int.Parse(input) > 0 && int.Parse(input) < GameBoard.Board.GetLength(1) + 1)
                                {
                                    // if piece can be placed, place token and move to next player's turn
                                    if (_PlayerList[i].PlaceToken(int.Parse(input)) == true)
                                    {
                                        GameBoard.DisplayBoard();
                                        break;
                                    }
                                    else // input isn't a number in valid range or 
                                    {
                                        Console.WriteLine("Invalid placement");
                                    }
                                }
                                else // input isn't a number
                                {
                                    Console.WriteLine("Please enter a valid column number");
                                }
                            }
                            else if (_PlayerList[i] is AIPlayer) // ai player input
                            {
                                Random r = new Random();
                                input = r.Next(1, 8).ToString();
                                Console.WriteLine(input);
                                if (_PlayerList[i].PlaceToken(int.Parse(input)) == true)
                                {
                                    GameBoard.DisplayBoard();
                                    //pause the game so the player can take a moment to see what the ai did
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid placement");
                                }

                            }

                        }

                        // check for a winning player
                        win = GameBoard.CheckForWin(_PlayerList[i]);
                        if (win) break;
                        // check for a tie
                        tie = GameBoard.CheckForTie();
                        if (tie) { win = true; break; }

                    }

                } while (win == false);

                // if the game is a tie, display a message stating as such
                if (tie)
                {
                    Console.WriteLine("The game is a tie!");
                }
                else if (win)// if a player has won, display a message congratulating them
                {
                    Console.WriteLine($"Congratulations {_PlayerList[currentplayer]} ({_PlayerList[currentplayer].Token}), you have won!");
                }
            }
        }
        public class Connect4GameBoard // gameboard class
        {
            public string[,] Board;

            public Connect4GameBoard() // initialize gameboard
            {
                // default board initialization
                Board = new string[6, 7];
                for (int i = 0; i < Board.GetLength(0); i++)
                {
                    for (int j = 0; j < Board.GetLength(1); j++)
                    {
                        Board[i, j] = "#";
                    }
                }
            }
            public void DisplayBoard() // display every line in the board along with their corresponding column numbers
            {
                string display = string.Empty;
                for (int i = 0; i < Board.GetLength(0); i++)
                {
                    for (int j = 0; j < Board.GetLength(1); j++)
                    {
                        display += $" {Board[i, j]} ";
                    }
                    Console.WriteLine(display);
                    display = string.Empty;
                }
                // write column numbers under board
                for (int i = 0; i < 7; i++)
                {
                    display += $" {i + 1} ";
                }
                Console.WriteLine(display);
            }

            // check for a tie
            public bool CheckForTie()
            {
                // check only to see if the top row is completely filled
                for (int i = 0; i < ConnectFour.GameBoard.Board.GetLength(1); i++)
                {
                    if (ConnectFour.GameBoard.Board[0, i] == "#")
                    {
                        return false;
                    }
                }
                return true;
            }

            // run a check on the entire board to see if a player has won
            public bool CheckForWin(Player player) // victory condition check
            {
                for (int row = 0; row < ConnectFour.GameBoard.Board.GetLength(0); row++)
                {
                    for (int col = 0; col < ConnectFour.GameBoard.Board.GetLength(1); col++)
                    {
                        if (_checkWinDiagonalDown(row, col, player.Token)) { return true; }
                        if (_checkWinDiagonalUp(row, col, player.Token)) { return true; }
                        if (_checkWinHorizontal(row, col, player.Token)) { return true; }
                        if (_checkWinVertical(row, col, player.Token)) { return true; }
                    }
                }
                return false;
            }

            // check for a winning line in 4 directions: horizontally, vertically, diagonally upwards, and diagonally downwards
            private bool _checkWinHorizontal(int r, int c, Token token)
            {
                // check to see if current check is out of bounds
                if (c + 3 >= ConnectFour.GameBoard.Board.GetLength(1))
                {
                    return false;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (ConnectFour.GameBoard.Board[r, c + i] != token.ToString())
                    {
                        return false;
                    }
                }
                return true;
            }
            private bool _checkWinVertical(int r, int c, Token token)
            {
                // check to see if current check is out of bounds
                if (r + 3 >= ConnectFour.GameBoard.Board.GetLength(0))
                {
                    return false;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (ConnectFour.GameBoard.Board[r + i, c] != token.ToString())
                    {
                        return false;
                    }
                }
                return true;
            }
            private bool _checkWinDiagonalDown(int r, int c, Token token)
            {
                // check to see if current check is out of bounds
                if (c + 3 >= ConnectFour.GameBoard.Board.GetLength(1))
                {
                    return false;
                }
                if (r + 3 >= ConnectFour.GameBoard.Board.GetLength(0))
                {
                    return false;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (ConnectFour.GameBoard.Board[r + i, c + i] != token.ToString())
                    {
                        return false;
                    }
                }
                return true;
            }
            private bool _checkWinDiagonalUp(int r, int c, Token token)
            {
                // check to see if current check is out of bounds
                if (c + 3 >= ConnectFour.GameBoard.Board.GetLength(1))
                {
                    return false;
                }
                if (r - 3 < 0)
                {
                    return false;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (ConnectFour.GameBoard.Board[r - i, c + i] != token.ToString())
                    {
                        return false;
                    }
                }
                return true;
            }

            public bool CheckValidPlacement(int c)
            {
                // check to see if token placement is valid
                if (Board[0, c - 1] == "#")
                {
                    return true;
                }
                return false;
            }
        }
        public abstract class Player // base player class
        {
            public Token Token; // object to represent the piece a player uses
            public abstract bool PlaceToken(int col);
        }

        // player subclasses
        public class HumanPlayer : Player
        {
            public string Name;
            public HumanPlayer(string name)
            {
                Name = name;
            }
            public override bool PlaceToken(int col)
            {
                if (ConnectFour.GameBoard.CheckValidPlacement(col) == true)
                {
                    for (int i = 5; i >= 0; i--)
                    {
                        if (ConnectFour.GameBoard.Board[i, col - 1] == "#")
                        {
                            ConnectFour.GameBoard.Board[i, col - 1] = Token.ToString();
                            return true;
                        }
                    }
                }
                return false;
            }
            public override string ToString()
            {
                return Name;
            }
        }

        public class AIPlayer : Player // non-human player, not implemented
        {
            public string Name;
            public Random r = new Random();
            public static string[] Names = File.ReadAllLines("..\\..\\..\\AiNames.txt"); // list of names
            public AIPlayer()
            {
                // pick a random name from the list in the AiNames.txt file
                Name = Names[r.Next(0, Names.Length)];
            }
            public override bool PlaceToken(int col)
            {
                if (ConnectFour.GameBoard.CheckValidPlacement(col) == true)
                {
                    for (int i = 5; i >= 0; i--)
                    {
                        if (ConnectFour.GameBoard.Board[i, col - 1] == "#")
                        {
                            ConnectFour.GameBoard.Board[i, col - 1] = Token.ToString();
                            return true;
                        }
                    }
                }
                return false;
            }
            public override string ToString()
            {
                return Name;
            }
        }

        public class Token // token used by players
        {
            private string _piece;
            public Token(string piece)
            {
                // ensure entered character is only a single value
                _piece = piece.Substring(0);
            }
            public override string ToString()
            {
                return _piece;
            }
        }

        static void Main(string[] args)
        {

            // begin a new game
            // for this sample it will only be for 2 players

            List<Player> players = new List<Player>();

            Console.WriteLine("Enter player names (leave blank for a default name, type \'ai\' to create an AI player");

            for (int i = 0; i < 2; i++)
            {
                Console.Write($"Player {i + 1} name: ");
                string name = Console.ReadLine();
                if (name == string.Empty) // if no name is provided, give a default name
                {
                    name = $"Anon {i + 1}";
                    Player player = new HumanPlayer(name);
                    players.Add(player);
                }
                else if (name.ToLower() == "ai") // if any configuration of 'ai' is entered, create an AI player instead
                {
                    Player player = new AIPlayer();
                    players.Add(player);
                }
                else // if a name is provided, create a human player with the entered name
                {
                    Player player = new HumanPlayer(name);
                    players.Add(player);
                }

            }

            // start a new game with the players we just made and begin a round of Connect Four
            ConnectFour.NewGame(players);
            ConnectFour.Play();

        }
    }
}