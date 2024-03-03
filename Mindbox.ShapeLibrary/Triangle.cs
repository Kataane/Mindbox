namespace Mindbox.ShapeLibrary;

/// <summary>
/// Represents a triangle shape.
/// </summary>
public class Triangle : IShape
{
    private readonly int a;
    private readonly int b;
    private readonly int c;

    /// <summary>
    /// Initializes a new instance of the <see cref="Triangle"/> class with the specified side lengths.
    /// </summary>
    /// <param name="a">The length of side a.</param>
    /// <param name="b">The length of side b.</param>
    /// <param name="c">The length of side c.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when any side length is less than or equal to zero,
    /// or when the sum of any two side lengths is less than the third side length (violating the triangle inequality).
    /// </exception>
    public Triangle(int a, int b, int c)
    {
        CheckTriangle(a, b, c);

        var desc = new[] { a, b, c }.OrderByDescending(static side => side).ToArray();

        this.a = desc[0];
        this.b = desc[1];
        this.c = desc[2];
    }

    /// <summary>
    /// Calculates the area of the triangle using Heron's formula.
    /// </summary>
    /// <returns>The area of the triangle.</returns>
    public double CalculateArea()
    {
        var semiperimeter = (a + b + c) / 2;
        return Math.Sqrt(semiperimeter * (semiperimeter - a) * (semiperimeter - b) * (semiperimeter - c));
    }

    /// <summary>
    /// Checks if the triangle is a right-angled triangle.
    /// </summary>
    /// <returns><see langword="true"/> if the triangle is a right-angled triangle; otherwise, <see langword="false"/>.</returns>
    public bool IsRectangular()
    {
        return a * a - b * b - c * c == 0;
    }

    /// <summary>
    /// Checks if the provided side lengths form a valid triangle.
    /// </summary>
    /// <param name="a">The length of side a.</param>
    /// <param name="b">The length of side b.</param>
    /// <param name="c">The length of side c.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when any side length is less than or equal to zero,
    /// or when the sum of any two side lengths is less than the third side length (violating the triangle inequality).
    /// </exception>
    public static void CheckTriangle(int a, int b, int c)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(a, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(b, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(c, 0);

        var sum = a + b + c;
        var max = Math.Max(a, Math.Max(b, c));
        ArgumentOutOfRangeException.ThrowIfLessThan(sum, max * 2);
    }
}