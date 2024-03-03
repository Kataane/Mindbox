namespace Mindbox.ShapeLibrary;

/// <summary>
/// Represents a geometric shape that can calculate its area.
/// </summary>
public interface IShape
{
    /// <summary>
    /// Calculates the area of the shape.
    /// </summary>
    /// <returns>The area of the shape.</returns>
    double CalculateArea();
}