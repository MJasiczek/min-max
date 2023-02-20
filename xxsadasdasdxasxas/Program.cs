using System;
using System.Collections.Generic;
using xxsadasdasdxasxas;
//X/O  MJas
namespace xxsadasdasdxasxas
{
    public class Program
    {

      
        static void Main(string[] args)
        {

            Content c = new Content();
            c.Go();
            Console.ReadKey();
        }

       
}
   public class Content
{
    public char[,] board = { { '?', '?', '?' }, { '?', '?', '?' }, { '?', '?', '?' } };
    public static char sysO = 'O';
    public static char playerX = 'X';
    public static char empty = '?';
    public Player xPlayer;
    public Player xSys;

        public Content()
        {
            this.xPlayer = new Player(true, this);
            this.xSys = new Player(false, this);
        }

    public Cell sysMove;

        public bool Over()
        {
            return Win(sysO) || Win(playerX) || GetCells().Count == 0;
        }
       

        public bool Win(char player)
        {
            
            

            //check col rows
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2] && board[i, 0] == player) || (board[0, i] == board[1, i] && board[0, i] == board[2, i] && board[0, i] == player))
                {
                    return true;
                }
            }
            // check przeka
            if ((board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2] && board[0, 0] == player) || (board[0, 2] == board[1, 1] && board[0, 2] == board[2, 0] && board[0, 2] == player))
            {
                return true;
            }
            return false;
        }

        public List<Cell> GetCells()
        {
            List<Cell> cells = new List<Cell>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == empty)
                    {
                        cells.Add(new Cell(i, j));
                    }
                }
            }
            return cells;
        }

        public bool PlayerMove(Cell cell, char player)
        {
            if (board[cell.x, cell.y] != empty)  // komorka zajeta
                return false;

            board[cell.x, cell.y] = player;
            return true;
        }


        public void show()
        {
            
            Console.WriteLine();

            Console.WriteLine("    a   b   c");
            for (int i = 0; i < 3; i++)
            {
               
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("  ---");
                }
                Console.WriteLine();
                Console.Write("");
                Console.Write("{0} | ", i + 1);


                for (int j = 0; j < 3; j++)
                {
                    string value = "?";

                    if (board[i, j] == sysO)
                        value = "O";
                    else if (board[i, j] == playerX)
                        value = "X";

                    Console.Write("{0} | ", value);
                }
                Console.WriteLine();
            }
            
            Console.WriteLine();
        }

        public void Go()
        {
            show();

            while (!Over())
            {
                xPlayer.GetPlayerMove();
                show();

                if (Over())
                {
                    break;
                }
                xSys.GetPlayerMove();
                show();

                if (Over())
                {
                    break;
                }
            }

            if (Win(sysO))
            {
                Console.WriteLine("Lose");
            }
            else if (Win(playerX))
            {
                Console.WriteLine("Win");
            }
            else
            {
                Console.WriteLine("Draw");
            }
        }
    }
}
       
    public struct Cell
    {
        public int x;
        public int y;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class Player
    {
        
        private char sign; // x/o 
        private bool player;

        Content p;
        SystemMiniMax sys;
        public Player( bool player, Content p)
        {
            this.player = player;
            this.sign = player ? 'X' : 'O';
            this.p = p;
            this.sys = new SystemMiniMax(this.p);
        }

        public char iSign()
        {
            return sign;
        }
        public bool GetPlayer()
        {
            return player;
        }
        public void GetPlayerMove() //get the currentPlayer Move
        {
            if (this.GetPlayer())
            {
                PlayerMove();
            }
            else
            {
                SysMove();
            }
        }

        private void PlayerMove()
        {
            bool ismovepossible = true;

            Cell cell = new Cell();
            string move;

            do
            {
                if (!ismovepossible)
                {
                    Console.WriteLine("Already filled");
                }
                do
                {
                    Console.Write(this + ":");
                    move = Console.ReadLine().ToUpper();

                    switch (move)
                    {
                        case "A1":
                            {
                                cell.x = 0; cell.y = 0;
                                break;
                            }
                        case "A2":
                            {
                                cell.x = 1; cell.y = 0;
                                break;
                            }
                        case "A3":
                            {
                                cell.x = 2; cell.y = 0;
                                break;
                            }
                        case "B1":
                            {
                                cell.x = 0; cell.y = 1;
                                break;
                            }
                        case "B2":
                            {
                                cell.x = 1; cell.y = 1;
                                break;
                            }
                        case "B3":
                            {
                                cell.x = 2; cell.y = 1;
                                break;
                            }
                        case "C1":
                            {
                                cell.x = 0; cell.y = 2;
                                break;
                            }
                        case "C2":
                            {
                                cell.x = 1; cell.y = 2;
                                break;
                            }
                        case "C3":
                            {
                                cell.x = 2; cell.y = 2;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Błędnie wpisana nazwa pola...");
                                break;
                            }
                    }
                }
                while (move != "A1" && move != "A2" && move != "A3" &&
                   move != "B1" && move != "B2" && move != "B3" &&
                   move != "C1" && move != "C2" && move != "C3"); // do czasu az podamy własciwe pole


                ismovepossible = p.PlayerMove(cell, Content.playerX); //(sprawdza czy pole zajęte, jesli nie to wstawia X i zwraca true)
            }
            while (!ismovepossible); // do czasu aż wypełnimy PUSTE POLE 
        }
        private void SysMove()
        {
            sys.MinMax(0, this.iSign());
            p.PlayerMove(p.sysMove, Content.sysO);
        }

      
    }
    class SystemMiniMax
    {
        private Content p;

        public SystemMiniMax(Content p)
        {
            this.p = p;
        }

        public int MinMax(int depth, char turn)
        {
            
            if (p.Win(Content.playerX))
            {
                return -1;
            }
            if (p.Win(Content.sysO))
            {
                return 1;
            }

            List<Cell> possibleCells = p.GetCells();
            if (possibleCells.Count == 0)
            {
                return 0;
            }

            int min = int.MaxValue;
            int max = int.MinValue;

            for (int i = 0; i < possibleCells.Count; i++)
            {
                Cell cell = possibleCells[i];

                if (turn == Content.sysO)
                {
                    p.PlayerMove(cell, Content.sysO);
                    int currentScore = MinMax(depth + 1, Content.playerX);
                    max = Math.Max(currentScore, max);

                    if (currentScore >= 0)
                    {
                        if (depth == 0)
                        {
                            p.sysMove = cell;
                        }
                    }
                    if (currentScore == 1)
                    {
                        p.board[cell.x, cell.y] = Content.empty;
                        break;
                    }
                    if (i == possibleCells.Count - 1 && max < 0)
                    {
                        if (depth == 0)
                        {
                            p.sysMove = cell;
                        }
                    }
                }
                else if (turn == Content.playerX)
                {
                    p.PlayerMove(cell, Content.playerX);
                    int currentScore = MinMax(depth + 1, Content.sysO);
                    min = Math.Min(currentScore, min);
                    if (min == -1)
                    {
                        p.board[cell.x, cell.y] = Content.empty;
                        break;
                    }
                }
                p.board[cell.x, cell.y] = Content.empty;
            }
            return turn == Content.sysO ? max : min;
        }
    }


