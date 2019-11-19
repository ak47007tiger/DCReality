namespace DC.UI
{
    public class BaseUI : BaseMonoBehaviour
    {
        public virtual void Init(params object[] param)
        {

        }

        public virtual void Close()
        {
            UiManager.CloseUi(UIManager.GetUiName(GetType()));
        }

        public UIManager UiManager
        {
            get { return UIManager.Instance; }
        }
    }
}