namespace DC
{
    public delegate T GetInstance<T>();

    public class CacheItem<T>
    {
        private T mItem;

        private GetInstance<T> mGetter;

        public CacheItem(GetInstance<T> getter)
        {
            SetGetter(getter);
        }

        public void SetGetter(GetInstance<T> getter)
        {
            mGetter = getter;
        }

        public T Value
        {
            get
            {
                if (mGetter == null)
                {
                    return default(T);
                }

                if (mItem == null)
                {
                    mItem = mGetter();
                }
                return mItem;
            }
        }
    }
}