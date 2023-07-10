using Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class BarView : MonoBehaviour
    {
        [field:SerializeField] private TextMeshProUGUI Name { get; set; }
        [field:SerializeField] private Image Image { get; set; }

        private Motive Motive { get; set; }
        
        public void Init(Motive motive)
        {
            Motive = motive;
            Name.SetText(Motive.Need);
            Image.fillAmount = 0;
        }

        private void FixedUpdate()
        {
            Image.fillAmount = Motive.Urgency;
        }
    }
}