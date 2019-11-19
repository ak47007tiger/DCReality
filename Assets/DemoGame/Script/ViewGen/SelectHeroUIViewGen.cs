
using UnityEngine;
using UnityEngine.UI;
namespace DC
{
    public class SelectHeroUIViewGen : MonoBehaviour
    {
		public ScrollRect heroSvScrollRect;
		public Image heroSvImage;

		public Button confirmButton;
		public Image confirmImage;

		public GameObject HeroItemGameObject;


        void Awake()
        {
			heroSvScrollRect = transform.Find("cm_heroSv").GetComponent<ScrollRect>();
			heroSvImage = transform.Find("cm_heroSv").GetComponent<Image>();

			confirmButton = transform.Find("cm_confirm").GetComponent<Button>();
			confirmImage = transform.Find("cm_confirm").GetComponent<Image>();

			HeroItemGameObject = transform.Find("it_HeroItem").gameObject;


        }
    }
}
