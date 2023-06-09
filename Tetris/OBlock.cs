﻿namespace Tetris
{
    public class OBlock : Block 
    {
        private readonly Position[][] tiles = new Position[][]
        {
            // OBlock = square, has the same position in all rotations
            new Position[] { new(0, 0), new(0, 1), new (1, 0), new (1, 1)},
        };
        public override int Id => 2;
        protected override Position StartOffset => new(0, 4);
        protected override Position[][] Tiles => tiles;
    }
}