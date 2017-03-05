using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.ModifiersPanel
{
    public class ModifierController : MonoBehaviour
    {
        public MonoBehaviour Modifier;
        private Text _nameLabel;
        private void Start()
        {

        }

        public string Name
        {
            set { _nameLabel.text = value; }
        }
    }
}