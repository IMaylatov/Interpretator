namespace Interpretator.Type
{
    public interface Exp<T>
    {
        T Evaluate(Context c);

        Exp<T> Replace(string name, Exp<T> exp);

        Exp<T> Copy();
    }
}
