namespace Tetris
{
    public abstract class Block
    {
        // abstract class to derive all blocks from
        // [][] Tiles contains all 4 positions block can have
        protected virtual Position[][] Tiles { get; set; }
        protected virtual Position StartOffset { get; set; }
        public abstract int Id { get; }

        // to choose from Tiles
        private int rotationState;
        private Position offset;

        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
            rotationState = 0;
        }

        // return grid positions occupied by a block (taking into consideration current rotation + offset)

        public IEnumerable<Position> TilePositions()
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        // rotate block 90 degrees clockwise
        public void RotateCW() 
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        // same but counterclockwise
        public void RotateCCW()
        {
            if (rotationState == 0)
                rotationState = Tiles.Length - 1;
            else
                rotationState--;
        }

        // move block
        public void Move(int r, int c)
        {
            offset.Row += r;
            offset.Column += c;
        }

        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}