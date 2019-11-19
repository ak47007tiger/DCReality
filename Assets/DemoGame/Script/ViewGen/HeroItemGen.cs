
using UnityEngine;
using UnityEngine.UI;
namespace DC
{
    public class HeroItemGen : MonoBehaviour
    {
		public Button headerButton;
		public Image headerImage;

		public Text nameText;


        void Awake()
        {
			headerButton = transform.Find("cm_header").GetComponent<Button>();
			headerImage = transform.Find("cm_header").GetComponent<Image>();

			nameText = transform.Find("cm_name").GetComponent<Text>();


        }
    }
}
