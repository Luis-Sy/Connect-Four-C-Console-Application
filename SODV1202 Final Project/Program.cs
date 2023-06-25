namespace SODV1202_Final_Project
{
    internal class Program
    {
        
        public static class ConnectFour
        {
            // properties for player objects
            private static List<Player> _PlayerList;
            public static Connect4GameBoard GameBoard;
            
            public static void NewGame(List<Player> players) // game initialization
            {
                GameBoard = new Connect4GameBoard();
                // if a previous game has already been played, clear the player list
                _PlayerList = new List<Player>();
                _PlayerList = players;

                // set default tokens
                players[0].Token = new Token("O");
                players[1].Token = new Token("X");

                GameBoard.DisplayBoard();
                Play();
            }

            public static void Play() // main game loop
            {
                int currentplayer;
                bool win = false;
                do
                {
                    for(int i = 0; i < _PlayerList.Count; i++)
                    {
                        currentplayer = i;
                        string input = string.Empty;
                        Console.WriteLine($"\n{_PlayerList[i].ToString()}'s turn\nPlace a piece");
                        // place a piece, repeat for valid input
                        while (true)
                        {
                            input = Console.ReadLine();
                            // filter out invalid inputs
                            if(input != string.Empty &&  int.TryParse(input, out int x) != false && int.Parse(input) > 0 && int.Parse(input) < GameBoard.Board.GetLength(1) + 1)
                            {
                                // if piece can be placed, place token and move to next player's turn
                                if(_PlayerList[i].PlaceToken(int.Parse(input)) == true)
                                {
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
                        GameBoard.DisplayBoard();
                    }
                }while( win == false);
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
                    Console.Write($" {i + 1} ");
                }
            }

            public bool CheckForWin(Player player, Token token) // victory condition check
            {
                return false;
            }

            public bool CheckValidPlacement(int c)
            {
                // check to see if token placement is valid
                if (Board[0,c-1] == "#")
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
                        if (ConnectFour.GameBoard.Board[i, col-1] == "#")
                        {
                            ConnectFour.GameBoard.Board[i, col-1] = Token.ToString();
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
            public static string[] Names = File.ReadAllLines("..\\..\\..\\AiNames.txt")); // list of names
            public AIPlayer()
            {
                // pick a random name from the list in the AiNames.txt file
                Name = Names[r.Next(0, Names.Length)];
            }
            public override bool PlaceToken(int col)
            {
                if(ConnectFour.GameBoard.CheckValidPlacement(col) == true)
                {
                    for(int i = 6; i > 0; i--)
                    {
                        if (ConnectFour.GameBoard.Board[i, col] == "#")
                        {
                            ConnectFour.GameBoard.Board[i, col] = Token.ToString();
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
            /*
            ToDo:
            -main function game logic
                -setup
                -check for win conditions
                -check for tie condition
            -classes
                -player class *
                    -human(done) and ai derived classes *
                -gameboard logic *
                    -check for victory conditions
                
            Bonus:
            -configurable game options
                -more than 2 players
                -custom board size
                -custom player tokens
            -retain win statistics for each player
            -new round vs newgame function?
             */

            // begin a new game
            // for this sample it will only be for 2 players
            List<Player> players = new List<Player>();
            for(int i = 0; i < 2;  i++)
            {
                Console.Write($"Player {i + 1} name: ");
                string name = Console.ReadLine();
                if(name == string.Empty)
                {
                    name = $"Anon {i + 1}";
                }
                Player player = new HumanPlayer(name);
                players.Add(player);
            }
            ConnectFour.NewGame(players);
            ConnectFour.GameBoard.DisplayBoard();
        }
    }
}