using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.UI.Util;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
namespace Code.UI.ModifiersPanel
{
    public class ModifierController : ListView<FieldInfo, NumberController>
    {
        public MonoBehaviour Modifier;
        public Text NameLabel;
        public List<FieldInfo> Fields;
        public SlideredNumberController SlideredNumber;
        public string PanelTitle
        {
            set
            {
                NameLabel.text = value;
            }
        }

        private void Start()
        {
            PanelTitle = Modifier.GetType().Name;
            Fields = Modifier
                .GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(x => x.GetValue(Modifier) is float || x.GetValue(Modifier) is int)
                .ToList();
            Place(Fields);
        }

        public void SetActiveness(bool value)
        {
            Modifier.enabled = value;
        }

        public override NumberController SelectElement(FieldInfo source)
        {
            return source.GetCustomAttributes(false).Any(x => x is RangeAttribute)
                ? ElementExamples[0] : ElementExamples[1];
        }

        public override NumberController Setup(FieldInfo source, NumberController element)
        {
            var controller = element as SlideredNumberController;
            if (controller != null)
            {
                var range = source.GetCustomAttributes(false)
                    .First(x => x is RangeAttribute) as RangeAttribute;
                if (range != null)
                {
                    controller.Max = range.max;
                    controller.Min = range.min;
                }
                controller.NameLabel.text = source.Name;
                controller.Bind(Modifier, source);
                return controller;
            }
            element.NameLabel.text = source.Name;
            element.Bind(Modifier, source);
            return element;
        }
    }
}