using System.Collections.Generic;
using UnityEngine;

namespace DC.GameLogic.UI
{
    public abstract class SimpleListView<D,V> where V : SimpleListItemView<D>
    {
        public abstract SimpleListViewAdapter<D> GetAdapter();

        public abstract V CreateViewItem();

        public abstract Transform GetListRootTf();

        List<V> mViewList = new List<V>();

        void Awake()
        {

        }

        void Start()
        {
            
        }

        public void Create()
        {
            mViewList.Clear();

            var dataList = GetAdapter().GetDataList();
            for (var i = 0; i < dataList.Count; i++)
            {
                var viewItem = CreateViewItem();
                var dataItem = dataList[i];
                viewItem.UpdateUi(dataItem);
                mViewList.Add(viewItem);
            }
        }

        public void DestroyView()
        {
            for (var i = 0; i < mViewList.Count; i++)
            {
                Object.Destroy(mViewList[i].gameObject);
            }
            mViewList.Clear();
        }

        public V GetViewItem(int index)
        {
            return mViewList[index];
        }
    }

    public abstract class SimpleListViewAdapter<T>
    {
        public abstract List<T> GetDataList();
    }

    public abstract class SimpleListItemView<D> : MonoBehaviour
    {
        public abstract void UpdateUi(D data);
    }
}