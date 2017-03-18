using System;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using Code.UI.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.ModifiersPanel
{
    public class SlideredNumberController : NumberController
    {
        public Text MaxText ,MinText;
        public Slider Slider;
        public float Max
        {
            private get { return _max; }
            set
            {
                _max = value;
                MaxText.text = _max.ToString();
                Slider.maxValue = _max;
                
            }
        }

        public void OnValueChanged()
        {
            if (Info.GetValue(Object) is int)
            {
                Info.SetValue(Object, (int)Slider.value);
                this.Field.text = ((int)Slider.value).ToString();
            }
            else
            {
                Info.SetValue(Object, Slider.value);
                this.Field.text = Slider.value.ToString();
            }
            
        }

        public float Min
        {
            private get { return _min; }
            set
            {
                _min = value;
                MinText.text = _min.ToString();
                Slider.minValue = _min;
            }
        }
        private float _max, _min;
        public override void Bind(object obj, FieldInfo field)
        {
            base.Bind(obj,field);
            Slider.value = float.Parse(Info.GetValue(Object).ToString());
        }
    }
}