namespace Mindbox.UnitTests;

/// <summary>
/// Contains unit tests for the <see cref="IShape"/> interface and its related extension methods.
/// </summary>
public class ShapeTests
{
    /// Of course, the data for this test should be more generalised,
    /// for example through the use of type reflection, but due to the small amount of data, this is acceptable.
    /// 
    /// <summary>
    /// Verifies that the TryAs method correctly identifies a valid shape and returns the expected shape.
    /// </summary>
    /// <typeparam name="TShape">The type of shape to try to convert to.</typeparam>
    /// <param name="shape">The shape to test.</param>
    /// <param name="tryAs">The TryAs method to test.</param>
    [Theory]
    [ClassData(typeof(ValidTryAsData))]
    public void TryAs_PassValidShape_ExpectedRectangle<TShape>(IShape shape, Func<IShape, (bool, TShape)> tryAs)
        where TShape : IShape
    {
        // Arrange

        // Act
        var (can, shape1) = tryAs(shape);

        // Assert
        Assert.True(can);
        Assert.Equal(shape.CalculateArea(), shape1.CalculateArea(), double.Epsilon);
    }

    /// Of course, the data for this test should be more generalised,
    /// for example through the use of type reflection, but due to the small amount of data, this is acceptable.
    /// 
    /// <summary>
    /// Verifies that the TryAs method correctly identifies an invalid shape and returns false with null shape.
    /// </summary>
    /// <typeparam name="TShape">The type of shape to try to convert to.</typeparam>
    /// <param name="shape">The shape to test.</param>
    /// <param name="tryAs">The TryAs method to test.</param>
    [Theory]
    [ClassData(typeof(InvalidValidTryAsData))]
    public void TryAs_PassInvalidShape_ExpectedRectangle<TShape>(IShape shape, Func<IShape, (bool, TShape)> tryAs)
        where TShape : IShape
    {
        // Arrange

        // Act
        var (can, shape1) = tryAs(shape);

        // Assert
        Assert.False(can);
        Assert.Null(shape1);
    }

    /// <summary>
    /// Contains test data for <see cref="TryAs_PassInvalidShape_ExpectedRectangle{TShape}"/> tests.
    /// </summary>
    public class InvalidValidTryAsData : TheoryData<IShape, Func<IShape, (bool, IShape?)>>
    {
        public InvalidValidTryAsData()
        {
            Add(new Circle(3), shape =>
            {
                var can = shape.TryAs<Triangle>(out var triangle);
                return new(can, triangle);
            });

            Add(new Triangle(3, 4, 5), shape =>
            {
                var can = shape.TryAs<Circle>(out var triangle);
                return new(can, triangle);
            });
        }
    }

    /// <summary>
    /// Contains test data for <see cref="TryAs_PassValidShape_ExpectedRectangle{TShape}"/> tests.
    /// </summary>
    public class ValidTryAsData : TheoryData<IShape, Func<IShape, (bool, IShape)>> 
    {
        public ValidTryAsData()
        {
            Add(new Circle(3), shape =>
            {
                var can = shape.TryAs<Circle>(out var circle);
                return new (can, circle!);
            });

            Add(new Triangle(3, 4 ,5), shape =>
            {
                var can = shape.TryAs<Triangle>(out var triangle);
                return new(can, triangle!);
            });
        }
    }
}
