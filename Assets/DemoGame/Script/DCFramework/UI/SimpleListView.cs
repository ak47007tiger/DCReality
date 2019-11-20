using System.Collections.Generic;
using UnityEngine;

namespace DC.GameLogic.UI
{
    public interface IListViewFuncs<D,V>
    {
        List<D> GetDataFunc();

        V CreateViewItemFunc(int index);

        void UpdateItemUiFunc(D itemData, V itemUi);
    }

    public class SimpleListView<D,V>
    {
        public delegate List<D> GetDataFunc();

        public delegate V CreateViewItemFunc(int index);

        public delegate void UpdateItemUiFunc(D data, V itemUi);

        List<V> mViewList = new List<V>();

        public GetDataFunc mGetData;

        public CreateViewItemFunc mCreateViewItem;

        public UpdateItemUiFunc mUpdateItemUi;

        public void Init(IListViewFuncs<D, V> listFuncs)
        {
            mGetData = listFuncs.GetDataFunc;
            mCreateViewItem = listFuncs.CreateViewItemFunc;
            mUpdateItemUi = listFuncs.UpdateItemUiFunc;
        }

        public void Init(GetDataFunc GetData, CreateViewItemFunc CreateViewItem, 
            UpdateItemUiFunc UpdateItemUi)
        {
            mGetData = GetData;
            mCreateViewItem = CreateViewItem;
            mUpdateItemUi = UpdateItemUi;
        }

        public void Create()
        {
            mViewList.Clear();

            var dataList = mGetData();
            for (var i = 0; i < dataList.Count; i++)
            {
                var viewItem = mCreateViewItem(i);
                var dataItem = dataList[i];
                mUpdateItemUi(dataItem, viewItem);
                mViewList.Add(viewItem);
            }
        }

        public void UpdateUi()
        {
            var dataList = mGetData();
            for (var i = 0; i < dataList.Count; i++)
            {
                var viewItem = mViewList[i];
                var dataItem = dataList[i];
                mUpdateItemUi(dataItem, viewItem);
            }
        }

        public void UpdateUi(int index)
        {
            var dataList = mGetData();
            var viewItem = mViewList[index];
            var dataItem = dataList[index];
            mUpdateItemUi(dataItem, viewItem);
        }

        public void DestroyView()
        {
            /*for (var i = 0; i < mViewList.Count; i++)
            {
                Object.Destroy(mViewList[i].gameObject);
            }*/
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