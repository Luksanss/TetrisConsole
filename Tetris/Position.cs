namespace Tetris
{
    public class Position
    {
        // every block will have its own position - blocks will be moved and rotated using their position coordinates
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int r, int c)
        {
            Row = r;
            Column = c;
        }
    }
}