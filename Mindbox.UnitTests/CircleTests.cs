namespace Mindbox.UnitTests;

/// <summary>
/// Contains unit tests for the <see cref="Circle"/> class.
/// </summary>
public class CircleTests
{
    private readonly ITestOutputHelper output;

    /// <summary>
    /// Represents the maximum radius value for a circle that does not exceed <see cref="int.MaxValue"/> when calculating its area.
    /// </summary>
    private const int MaxValueByPi = (int)(int.MaxValue / Math.PI);

    /// <summary>
    /// Initializes a new instance of the <see cref="CircleTests"/> class with the specified test output helper.
    /// </summary>
    /// <param name="output">The test output helper.</param>
    public CircleTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    /// <summary>
    /// Verifies that an <see cref="ArgumentOutOfRangeException"/> is thrown when attempting to create a circle with a negative or zero radius.
    /// </summary>
    [Fact]
    public void Constructor_NegativeOrZeroRadius_Exception()
    {
        // Arrange
        var radius = Random.Shared.Next(int.MinValue, 0);
        output.WriteLine("Pass {0} for circle radius", radius);

        // Act
        Circle Act() => new(radius);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Act);
    }

    /// <summary>
    /// Verifies that the area of the circle is calculated correctly for a valid radius.
    /// </summary>
    [Fact]
    public void CalculateArea_ValidRadius_ExceptedArea()
    {
        // Arrange
        var radius = Random.Shared.Next(1, MaxValueByPi);
        output.WriteLine("Pass {0} for circle radius", radius);

        var circle = new Circle(radius);
        var expectedArea = Math.PI * radius * radius;

        // Act
        var actualArea = circle.CalculateArea();

        // Assert
        Assert.Equal(expectedArea, actualArea, double.Epsilon);
    }
}