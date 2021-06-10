namespace Musicalog.Core
{
    /// <summary>
    /// Updates properties of one object from those of another
    /// </summary>
    /// <typeparam name="TSource">The source object containing new property values</typeparam>
    /// <typeparam name="TTarget">The target object whose properties are to be updated</typeparam>
    public interface IUpdater<in TSource, in TTarget>
    {
        /// <summary>
        /// Updates properties of the target from those of the source
        /// </summary>
        /// <param name="source">The source object containing new property values</param>
        /// <param name="target">The target object whose properties are to be updated</param>
        void Update(TSource source, TTarget target);
    }
}