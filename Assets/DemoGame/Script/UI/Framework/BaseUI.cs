namespace DC.UI
{
    public class BaseUI : BaseMonoBehaviour
    {
        public virtual void Init(params object[] param)
        {

        }

        public virtual void UpdateUi(params object[] param)
        {

        }

        public virtual void OnShow()
        {

        }

        public virtual void OnHide()
        {

        }

        public virtual void Destroy()
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