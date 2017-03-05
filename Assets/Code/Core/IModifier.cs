using Assets.SimpleGenerator;

namespace Code.Core
{
    public interface IModifier<T> where T : Cell
    {
        void Callback(T current);
        void Start();
    }
}