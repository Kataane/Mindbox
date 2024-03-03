namespace Mindbox.ShapeLibrary;

/// <summary>
/// Provides extension methods for working with shapes.
/// </summary>
public static class ShapeExtensions
{
    /// <summary>
    /// Tries to convert the specified shape to the specified type.
    /// </summary>
    /// <typeparam name="TShape">The type of shape to convert to.</typeparam>
    /// <param name="shape">The shape to convert.</param>
    /// <param name="result">When this method returns, contains the converted shape, if the conversion succeeded, or the default value of <typeparamref name="TShape"/> if the conversion failed.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; otherwise, <see langword="false"/>.</returns>
    public static bool TryAs<TShape>(this IShape shape, [MaybeNullWhen(false)] out TShape? result)
        where TShape : IShape
    {
        result = default;

        if (shape is not TShape convertedShape) return false;

        result = convertedShape;
        return true;
    }
}