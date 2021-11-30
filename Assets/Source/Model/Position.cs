using System;

namespace ManAndPig.Model
{
    public struct Position
    {
        public int X;
        public int Y;

        public int Magnitude => Math.Abs(X + Y);
        public Position Normalized => new Position(Math.Clamp(X, -1, 1), Math.Clamp(Y, -1, 1));

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position operator +(Position a, Position b)
        {
            return new Position(a.X + b.X, a.Y + b.Y);
        }

        public static Position operator -(Position a, Position b)
        {
            return new Position(a.X - b.X, a.Y - b.Y);
        }
    }
}