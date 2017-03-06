using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.ModifiersPanel
{
    public class NumberController : MonoBehaviour, IFieldController
    {
        private FieldInfo _info;
        public Text NameLabel;
        private object _object;

        public InputField Field;

        public string PanelTitle
        {
            set
            {
                NameLabel.text = value;
            }
        }

        public void SetValue(string valuestr)
        {
            if (_info.GetValue(_object) is int)
            {
                _info.SetValue(_object, int.Parse(valuestr));
            }
            else
            {
                _info.SetValue(_object, float.Parse(valuestr));
            }
        }

        public void Bind(object obj, FieldInfo field)
        {
            _object = obj;
            _info = field;
            Field.text = _info.GetValue(_object).ToString();
        }
    }
}