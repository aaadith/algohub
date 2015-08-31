using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTracking
{
    /* Given the initial position of knight on a chessboard, find the selectio of moves that would cover all positions on the board
	*/
    public class Position
    {
        public int x { get; set; }

        public int y { get; set; }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Knight
    {

        IEnumerable<Position> GetNext(Position current)
        {
            List<Position> candidates = new List<Position> 
            {
                new Position(current.x+1, current.y+2), 
                new Position(current.x-1, current.y+2) , 
                new Position(current.x+1, current.y-2) , 
                new Position(current.x-1, current.y-2),
                new Position(current.x+2, current.y+1), 
                new Position(current.x-2, current.y+1) , 
                new Position(current.x+2, current.y-1) , 
                new Position(current.x-2, current.y-1)
            };

            IEnumerable<Position> result = candidates.Where(p => p.x >= 0 && p.x < 8 && p.y >= 0 && p.y < 8);
            return result;
        }

        public Position[] Tour(Position current)
        {
            Position[] result = null;
            Stack<Position> moves = new Stack<Position>();
            if (Tour(current, moves))
            {
                result = moves.Reverse().ToArray<Position>();
            }
            return result;
        }

        public bool Tour(Position current, Stack<Position> moves)
        {
            if (moves.Count == 64)
                return true;
            foreach (Position position in GetNext(current))
            {
                if (moves.Contains(position))
                    continue;

                moves.Push(position);
                if (Tour(position, moves))
                    return true;
                else
                    moves.Pop();
            }
            return false;
        }
    }

}
