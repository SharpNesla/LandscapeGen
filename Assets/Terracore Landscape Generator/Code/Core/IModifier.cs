using Assets.SimpleGenerator;

namespace Code.Core
{
    public interface IModifier<T>
    {
        void Callback(T current);
        void Start();
    }
}