namespace Tetris
{
    public class BlockQueue
    {
        // picking blocks
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };

        private readonly Random pickABlock = new Random();

        public Block NextBlock { get; private set; }

        public BlockQueue()
        {
            NextBlock = RandomBlock();
        }

        private Block RandomBlock()
        {
            return blocks[pickABlock.Next(0, blocks.Length)];
        }

        public Block GetARandomBlock()
        {
            // get a new block, can be the same it was before
            NextBlock = RandomBlock();

            return NextBlock ;
        }
    }
}