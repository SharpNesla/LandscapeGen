using System.Reflection;

namespace Code.UI.ModifiersPanel
{
    public interface IFieldController
    {
        void Bind(object obj, FieldInfo field);
    }
}