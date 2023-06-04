namespace SODV1202_Final_Project
{
    internal class Program
    {
        public class ConnectFour
        {
            // properties for player objects
            public string Name;
            private static List<ConnectFour> PlayerList;
            private static string[,] GameBoard;
            public int Count;

            public ConnectFour() // default player constructor
            {
                Name = "Anonymous";
            }

            public static void AddPlayer(string name) // player creation function
            {
                // create new player instance and add them to the list
                ConnectFour player = new ConnectFour {Name = name };
                PlayerList.Add(player);
            }

            private static void InitializeGameBoard() // create the gameboard
            {
                GameBoard = new string[6,7];
                for(int i = 0; i < GameBoard.GetLength(0); i++)
                {
                    for(int j = 0; j < GameBoard.GetLength(1); j++)
                    {
                        GameBoard[i, j] = "-";
                    }
                }
            }



        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}