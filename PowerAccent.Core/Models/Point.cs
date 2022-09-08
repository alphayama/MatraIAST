﻿namespace PowerIAST.Core;

public struct Point
{
    public Point()
    {
        X = 0;
        Y = 0;
    }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point(System.Drawing.Point point)
    {
        X = point.X;
        Y = point.Y;
    }

    public double X { get; init; }
    public double Y { get; init; }

    public static implicit operator Point(System.Drawing.Point point) => new Point(point.X, point.Y);

    public static Point operator /(Point point, double divider)
    {
        if (divider == 0)
        {
            throw new DivideByZeroException();
        }
        return new Point(point.X / divider, point.Y / divider);
    }

    public static Point operator /(Point point, Point divider)
    {
        if (divider.X == 0 || divider.Y == 0)
        {
            throw new DivideByZeroException();
        }
        return new Point(point.X / divider.X, point.Y / divider.Y);
    }
}
