namespace Mindbox.ShapeLibrary;

/// <summary>
/// Represents a circle shape.
/// </summary>
public class Circle : IShape
{
    private readonly int radius;

    /// <summary>
    /// Initializes a new instance of the <see cref="Circle"/> class with the specified radius.
    /// </summary>
    /// <param name="radius">The radius of the circle.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the radius is less than or equal to zero.</exception>
    public Circle(int radius)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radius, 0);

        this.radius = radius;
    }

    /// <summary>
    /// Calculates the area of the circle.
    /// </summary>
    /// <returns>The area of the circle.</returns>
    public double CalculateArea()
    {
        return Math.PI * radius * radius;
    }
}