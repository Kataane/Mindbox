namespace Mindbox.UnitTests;

/// <summary>
/// Contains unit tests for the <see cref="Triangle"/> class.
/// </summary>
public class TriangleTests
{
    /// <summary>
    /// Represents the maximum value for which the cube root does not exceed <see cref="int.MaxValue"/>.
    /// It is used to calculate Pythagorean triples. See <see cref="GetRandomPythagoreanTriple"/>
    /// </summary>
    private static readonly int MaxValueThreeRoot = (int)Math.Pow(int.MaxValue, 1.0 / 3);

    private readonly ITestOutputHelper output;

    /// <summary>
    /// Initializes a new instance of the <see cref="TriangleTests"/> class with the specified test output helper.
    /// </summary>
    /// <param name="output">The test output helper.</param>
    public TriangleTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    #region Constructor

    /// <summary>
    /// Verifies that an <see cref="ArgumentOutOfRangeException"/> is thrown when attempting to create a triangle with negative or zero sides.
    /// </summary>
    [Fact]
    public void Constructor_NegativeOrZeroSides_Exception()
    {
        // Arrange
        var a = Random.Shared.Next(int.MinValue, 0);
        var b = Random.Shared.Next(int.MinValue, 0);
        var c = Random.Shared.Next(int.MinValue, 0);

        output.WriteLine("Pass {0} {1} {2} for triangle sides", a, b, c);

        // Act
        Triangle Act() => new(a, b, c);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Act);
    }

    /// <summary>
    /// Verifies that no exception is thrown when creating a triangle with two sides greater than the third.
    /// </summary>
    [Fact]
    public void Constructor_TwoSidesAreBiggerThanOne_NotThrowException()
    {
        // Arrange
        var sides = GetRandomValidTriangle();
        output.WriteLine("Pass {0} {1} {2} for triangle sides", sides[0], sides[1], sides[2]);

        // Act
        var exception = Record.Exception(() => new Triangle(sides[0], sides[1], sides[2]));

        // Assert
        Assert.Null(exception);
    }

    /// <summary>
    /// Verifies that no exception is thrown when creating a triangle with all sides equal.
    /// </summary>
    [Fact]
    public void Constructor_SidesAreSame_NotThrowException()
    {
        // Arrange
        var a = Random.Shared.Next(1, int.MaxValue / 3);
        output.WriteLine("Pass {0} for all triangle sides", a);

        // Act
        var exception = Record.Exception(() => new Triangle(a, a, a));

        // Assert
        Assert.Null(exception);
    }

    /// <summary>
    /// Verifies that an <see cref="ArgumentOutOfRangeException"/> is thrown when attempting to create a triangle with one side larger than the sum of the other two.
    /// </summary>
    [Fact]
    public void Constructor_OneSideIsBigger_ThrowException()
    {
        // Arrange
        var sides = GetRandomInvalidTriangle();
        output.WriteLine("Pass {0} for all triangle sides", sides);

        // Act
        Triangle Act() => new(sides[0], sides[1], sides[2]);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Act);
    }

    #endregion

    #region CalculateArea

    /// <summary>
    /// Verifies that the area of the triangle is calculated correctly for valid sides.
    /// </summary>
    [Fact]
    public void CalculateArea_ValidSides_ExpectedArea()
    {
        // Arrange
        var sides = GetRandomValidTriangle();
        output.WriteLine("Pass {0} {1} {2} for triangle sides", sides[0], sides[1], sides[2]);

        var triangle = new Triangle(sides[0], sides[1], sides[2]);

        var semiperimeter = (sides[0] + sides[1] + sides[2]) / 2;
        var expected = Math.Sqrt(semiperimeter * (semiperimeter - sides[0]) * (semiperimeter - sides[1]) * (semiperimeter - sides[2]));

        // Act
        var actual = triangle.CalculateArea();

        // Assert
        Assert.Equal(expected, actual, double.Epsilon);
    }

    #endregion

    #region IsRectangular

    /// <summary>
    /// Verifies that a triangle with sides forming a Pythagorean triple is considered rectangular.
    /// </summary>
    [Fact]
    public void IsRectangular_Rectangular_ExpectedTrue()
    {
        // Arrange
        var sides = GetRandomPythagoreanTriple();

        output.WriteLine("Pass {0} {1} {2} for rectangular sides", sides[0], sides[1], sides[2]);

        var triangle = new Triangle(sides[0], sides[1], sides[2]);

        // Act
        var actual = triangle.IsRectangular();

        // Assert
        Assert.True(actual);
    }

    /// <summary>
    /// Verifies that a triangle with sides not forming a Pythagorean triple is not considered rectangular.
    /// </summary>
    [Fact]
    public void IsRectangular_NonRectangular_ExpectedFalse()
    {
        // Arrange
        var sides = GetRandomNonPythagoreanTriple();

        output.WriteLine("Pass {0} {1} {2} for triangle sides", sides[0], sides[1], sides[2]);

        var triangle = new Triangle(sides[0], sides[1], sides[2]);

        // Act
        var actual = triangle.IsRectangular();

        // Assert
        Assert.False(actual);
    }

    #endregion

    /// <summary>
    /// Retrieves random sides forming a non-Pythagorean triple.
    /// </summary>
    /// <remarks>
    /// The method generates random integers for triangle sides within a certain range. 
    /// It ensures that the triangle inequality holds and that the sides do not form a Pythagorean triple.
    /// </remarks>
    /// <param name="cancelToken">The cancellation token.</param>
    /// <returns>An array representing the sides of the triangle.</returns>
    protected static int[] GetRandomNonPythagoreanTriple(CancellationToken cancelToken = default)
    {
        int[] result;

        while (true)
        {
            cancelToken.ThrowIfCancellationRequested();
            result = Enumerable.Range(0, 3).Select(_ => Random.Shared.Next(1, MaxValueThreeRoot)).ToArray();
            var max = result.Max();
            if (result[0] + result[1] + result[2] < max * 2) continue;

            result = result.OrderByDescending(static side => side).ToArray();
            if (result[0] * result[0] - result[1] * result[1] - result[2] * result[2] != 0) break;
        }

        return result;
    }

    /// <summary>
    /// Retrieves random sides forming a Pythagorean triple.
    /// </summary>
    /// <remarks>
    /// The method generates random integers for the parameters k, m, and n within a certain range.
    /// It ensures that the generated sides form a Pythagorean triple according to the formula: a² + b² = c².
    /// </remarks>
    /// <seealso cref="https://en.wikipedia.org/wiki/Pythagorean_triple"/>
    /// <param name="cancelToken">The cancellation token.</param>
    /// <returns>An array representing the sides of the Pythagorean triple.</returns>
    protected static int[] GetRandomPythagoreanTriple(CancellationToken cancelToken = default)
    {
        var triple = new int[3];

        while (true)
        {
            cancelToken.ThrowIfCancellationRequested();

            var k = Random.Shared.Next(1, MaxValueThreeRoot);
            var m = Random.Shared.Next(2, MaxValueThreeRoot);
            var n = Random.Shared.Next(1, m - 1);

            if (int.IsEvenInteger(n) || int.IsEvenInteger(m)) continue;

            try
            {
                // Actually not necessary for checked, but because <see cref="MaxValueThreeRoot"/> is not exactly calculated,
                // operations with k, m, n may exceed the allowed values, checked is needed to catch this.
                // Of course this reduces the number of observable values, but the range is wide enough to try to get all valid values.
                checked
                {
                    triple[0] = k * (m * m - n * n);
                    triple[1] = k * 2 * m * n;
                    triple[2] = k * (m * m + n * n);

                    triple = triple.OrderByDescending(static side => side).ToArray();
                    if (triple[0] * triple[0] - triple[1] * triple[1] - triple[2] * triple[2] == 0) break;
                }
            }
            catch
            {
                // ignored
            }
        }

        return triple;
    }

    /// <summary>
    /// Retrieves random sides for a valid triangle.
    /// </summary>
    /// <remarks>
    /// The method generates random integers for triangle sides within a certain range.
    /// It ensures that the triangle inequality holds, where the sum of any two sides must be greater than the third side.
    /// </remarks>
    /// <param name="cancelToken">The cancellation token.</param>
    /// <returns>An array representing the sides of the triangle.</returns>
    private static int[] GetRandomValidTriangle(CancellationToken cancelToken = default)
    {
        int[] result;

        while (true)
        {
            cancelToken.ThrowIfCancellationRequested();

            result = Enumerable.Range(0, 3).Select(_ => Random.Shared.Next(1, int.MaxValue)).ToArray();
            var max = result.Max();
            if (result[0] + result[1] + result[2] > max * 2) break;
        }

        return result;
    }

    /// <summary>
    /// Retrieves random sides for an invalid triangle.
    /// </summary>
    /// <remarks>
    /// The method generates random integers for triangle sides within a certain range.
    /// It ensures that the triangle inequality does not hold, where the sum of any two sides is not greater than the third side.
    /// </remarks>
    /// <param name="cancelToken">The cancellation token.</param>
    /// <returns>An array representing the sides of the triangle.</returns>
    private static int[] GetRandomInvalidTriangle(CancellationToken cancelToken = default)
    {
        int[] result;

        while (true)
        {
            cancelToken.ThrowIfCancellationRequested();

            result = Enumerable.Range(0, 3).Select(_ => Random.Shared.Next(1, int.MaxValue)).ToArray();
            var max = result.Max();
            if (result[0] + result[1] + result[2] < max * 2) break;
        }

        return result;
    }
}