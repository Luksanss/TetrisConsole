using System.Collections;

namespace Tetris
{
    public class GameState
    {
        public Block curBlock;

        public Block CurBlock
        {
            get
            {
                return curBlock;
            }
            private set
            {
                // set to a new block
                curBlock = value;
                // set to specific block starting possition
                curBlock.Reset();
            }
        }

        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurBlock = BlockQueue.GetARandomBlock();
        }

        private bool BlockFits()
        {
            // foreach block segment position check if it has a empty space in grid to fit in
            foreach (Position p in CurBlock.TilePositions())
            {
                if (!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    // if it doesnt have an empty space
                    return false;       
                }
            }
            return true;
        }

        public void RotateBlockCW()
        {
            CurBlock.RotateCW();


            if (!BlockFits())
            {
                // move block position back
                CurBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurBlock.RotateCCW();

            if (!BlockFits())
            {
                CurBlock.RotateCW();
            }
        }

        public void MoveBlockLeft()
        {
            CurBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            CurBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurBlock.Move(0, -1);
            }
        }

        public bool IsGameOver()
        {
            // top two rows must be empty
            return !(GameGrid.RowIsEmpty(0) && GameGrid.RowIsEmpty(1));
        }
        private void PlaceBlock()
        {
            foreach (Position p in CurBlock.TilePositions())
            {
                // assign gamegrid places to an id of current block
                GameGrid[p.Row, p.Column] = CurBlock.Id;
            }

            // clear rows that are full
            GameGrid.ClearRows();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurBlock = BlockQueue.GetARandomBlock();
            }
        }

        public void MoveBlockDown()
        {
            CurBlock.Move(1, 0);

            if (!BlockFits())
            {
                // if block does not fit, that means there is a block/end of queue under it, so it gets placed
                CurBlock.Move(-1, 0);
                PlaceBlock();
            }
        }
    }

}