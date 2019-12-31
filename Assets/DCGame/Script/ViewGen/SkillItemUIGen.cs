
using UnityEngine;
using UnityEngine.UI;
namespace DC
{
    public class SkillItemUIGen : MonoBehaviour
    {
		public Button iconButton;
		public Image iconImage;

		public Image cdImage;

		public Image silenceImage;


        void Awake()
        {
			iconButton = transform.Find("cm_icon").GetComponent<Button>();
			iconImage = transform.Find("cm_icon").GetComponent<Image>();

			cdImage = transform.Find("cm_cd").GetComponent<Image>();

			silenceImage = transform.Find("cm_silence").GetComponent<Image>();


        }
    }
}
