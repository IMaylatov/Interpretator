namespace Interpretator
{
    public interface IInterpretator<T>
    {
        T Run(string expression);
    }
}
