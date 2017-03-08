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
        public override NumberController Setup(FieldInfo source)
        {
            var attrubutes = source.GetCustomAttributes(false);
            if (attrubutes.Any(x => x is RangeAttribute))
            {
                var range = attrubutes.First(x => x is RangeAttribute) as RangeAttribute;
                if (range != null)
                {
                    SlideredNumber.Max = range.max;
                    SlideredNumber.Min = range.min;
                }
                SlideredNumber.NameLabel.text = source.Name;
                SlideredNumber.Bind(Modifier, source);
                return SlideredNumber;
            }
            ElementExample.NameLabel.text = source.Name;
            ElementExample.Bind(Modifier, source);
            return ElementExample;
        }
    }
}