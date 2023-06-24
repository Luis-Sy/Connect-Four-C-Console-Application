namespace SODV1202_Final_Project
{
    internal class Program
    {
        // create separate classes for game logic, game board, player
        public static class ConnectFour
        {
            // properties for player objects
            private static List<Player> _PlayerList;
            private static Connect4GameBoard _GameBoard;

            public static void NewGame(List<Player> players) // game initialization
            {
                _GameBoard = new Connect4GameBoard();
                _PlayerList.Clear();
                _PlayerList = players;
            }
        }
        public class Connect4GameBoard // gameboard class
        {
            public string[,] Board;

            public Connect4GameBoard() // initialize gameboard
            {
                Board = new string[6, 7];
                for (int i = 0; i < Board.GetLength(0); i++)
                {
                    for (int j = 0; j < Board.GetLength(1); j++)
                    {
                        Board[i, j] = "#";
                    }
                }
            }

            public bool CheckForWin(Player player, Token token)
            {
                return false;
            }
        }
        public abstract class Player // base player class
        {
            private Token _token;
            public abstract bool PlaceToken(int col, int row);
        }

        // player subclasses
        public class HumanPlayer : Player
        {
            private string _Name;
            public HumanPlayer(string name)
            {
                _Name = name;
            }
            public override bool PlaceToken(int col, int row)
            {
                //todo
                return false;
            }
        }

        public class AIPlayer : Player
        {
            private string _Name;
            public AIPlayer(string name)
            {
                _Name = name;
            }
            public override bool PlaceToken(int col, int row)
            {
                return false;
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
                -player class
                    -human and ai derived classes
                -token class
                    -used by players on the gameboard
                -gameboard logic
                    -placing pieces
                    -check for invalid placements
                    -check for victory conditions
                
            Bonus:
            -configurable game options
                -more than 2 players
                -custom board size
                -custom player tokens
             */
        }
    }
}