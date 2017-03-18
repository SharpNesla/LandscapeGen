using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.ModifiersPanel
{
    public class NumberController : MonoBehaviour, IFieldController
    {
        protected FieldInfo Info;
        public Text NameLabel;
        protected object Object;

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
            try
            {
                if (Info.GetValue(Object) is int)
                {
                    Info.SetValue(Object, int.Parse(valuestr));
                }
                else
                {
                    Info.SetValue(Object, float.Parse(valuestr));
                }
            }
            catch (Exception)
            {
                // ignored
            }

        }

        public virtual void Bind(object obj, FieldInfo field)
        {
            Object = obj;
            Info = field;
            Field.text = Info.GetValue(Object).ToString();
        }
    }
}