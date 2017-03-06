using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.UI.Util;
using UnityEngine;
using UnityEngine.UI;
namespace Code.UI.ModifiersPanel
{
    public class ModifierController : ListView<FieldInfo, NumberController>
    {
        public MonoBehaviour Modifier;
        public Text NameLabel;
        public List<FieldInfo> Fields;
        public string PanelTitle
        {
            set
            {
                NameLabel.text = value;
            }
        }

        private void Start()
        {
            Fields = Modifier
                .GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(x => x.GetValue(Modifier) is float || x.GetValue(Modifier) is int)
                .ToList();
            var scale = Place(Fields, 3f);
            GetComponent<RectTransform>().sizeDelta = new Vector2(0, scale + 25f);
        }

        public void SetActiveness(bool value)
        {
            Modifier.enabled = value;
        }
        public override void Setup(FieldInfo source, NumberController element)
        {
            element.NameLabel.text = source.Name;
            element.Bind(Modifier, source);
        }
    }
}