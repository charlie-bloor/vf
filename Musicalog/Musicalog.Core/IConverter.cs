namespace Musicalog.Core
{
    public interface IConverter<in TIn, out TOut>
    {
        TOut Convert(TIn value);
    }

    public interface IConverter<in TIn, in T1, out TOut>
    {
        TOut Convert(TIn value, T1 arg1);
    }

    public interface IConverter<in TIn, in T1, in T2, out TOut>
    {
        TOut Convert(TIn value, T1 arg1, T2 arg2);
    }

    public interface IConverter<in TIn, in T1, in T2, in T3, out TOut>
    {
        TOut Convert(TIn value, T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IConverter<in TIn, in T1, in T2, in T3, in T4, out TOut>
    {
        TOut Convert(TIn value, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }
}