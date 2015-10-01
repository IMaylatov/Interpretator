namespace Interpretator
{
    public interface IInterpretatorType<T>
    {
        T Run(string expression);
    }
}
