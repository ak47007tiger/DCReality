
using UnityEngine;
using UnityEngine.UI;
namespace DC
{
    public class SelectHeroUIGen : MonoBehaviour
    {
		public ScrollRect heroSvScrollRect;
		public Image heroSvImage;

		public RectTransform heroGridRectTransform;

		public Button confirmButton;
		public Image confirmImage;

		public GameObject HeroItemGameObject;


        void Awake()
        {
			heroSvScrollRect = transform.Find("cm_heroSv").GetComponent<ScrollRect>();
			heroSvImage = transform.Find("cm_heroSv").GetComponent<Image>();

			heroGridRectTransform = transform.Find("cm_heroSv/Viewport/tf_heroGrid").GetComponent<RectTransform>();

			confirmButton = transform.Find("cm_confirm").GetComponent<Button>();
			confirmImage = transform.Find("cm_confirm").GetComponent<Image>();

			HeroItemGameObject = transform.Find("it_HeroItem").gameObject;


        }
    }
}
