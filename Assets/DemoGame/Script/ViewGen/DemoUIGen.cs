
using UnityEngine;
using UnityEngine.UI;
namespace DC
{
    public class DemoUIGen : MonoBehaviour
    {
		public RectTransform rootRectTransform;

		public Image headerImage;

		public Text nameText;


        void Awake()
        {
			rootRectTransform = transform.Find("tf_root").GetComponent<RectTransform>();

			headerImage = transform.Find("tf_root/cm_header").GetComponent<Image>();

			nameText = transform.Find("tf_root/cm_name").GetComponent<Text>();


        }
    }
}
