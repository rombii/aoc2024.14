namespace aoc2024._14;

internal class Robot(int x, int y, int moveX, int moveY)
{
    public int X { get; private set; } = x;
    public int Y { get; private set; } = y;
    private int MoveX { get; } = moveX;
    private int MoveY { get; } = moveY;

    public void Update(int limitX, int limitY)
    {
        X = WrapIndex(X + MoveX, limitX);
        Y = WrapIndex(Y + MoveY, limitY);
    }

    private static int WrapIndex(int index, int size)
    {
        return (index % size + size) % size;
    }
}