using System;
using Android.Database;
using Android.Views;
using Android.Widget;

namespace EndlessScrollPort
{
	// TODO: refactoring
	public class MyDataSetObserver : DataSetObserver
	{
		private readonly Action _onChanged;
		private readonly Action _onIvalidated;

		public MyDataSetObserver(Action onChanged, Action onIvalidated)
		{
			_onChanged = onChanged;
			_onIvalidated = onIvalidated;
		}

		public override void OnChanged()
		{
			_onChanged();
		}

		public override void OnInvalidated()
		{
			_onIvalidated();
		}
	}


	public class AdapterWrapper : BaseAdapter
	{
		private readonly IListAdapter _wrappedAdapter = null;

		public AdapterWrapper(IListAdapter wrapped)
		{
			this._wrappedAdapter = wrapped;

			MyDataSetObserver dataSetObserver = new MyDataSetObserver(NotifyDataSetChanged, NotifyDataSetInvalidated);
			wrapped.RegisterDataSetObserver(dataSetObserver);
		}

		public override Java.Lang.Object GetItem(int position)
		{
			Java.Lang.Object item = _wrappedAdapter.GetItem(position);
			return item;
		}

		public override int Count
		{
			get
			{
				return _wrappedAdapter.Count;
			}
		}

		public override int ViewTypeCount
		{
			get
			{
				return _wrappedAdapter.ViewTypeCount;
			}
		}

		public override bool AreAllItemsEnabled()
		{
			bool areAllItemsEnabled = _wrappedAdapter.AreAllItemsEnabled();
			return areAllItemsEnabled;
		}

		public override int GetItemViewType(int position)
		{
			int itemViewType = _wrappedAdapter.GetItemViewType(position);
			return itemViewType;
		}

		public override bool IsEnabled(int position)
		{
			bool isEnabled = _wrappedAdapter.IsEnabled(position);
			return isEnabled;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View result = _wrappedAdapter.GetView(position, convertView, parent);
			return result;
		}

		public override long GetItemId(int position)
		{
			long itemId = _wrappedAdapter.GetItemId(position);
			return itemId;
		}

		protected IListAdapter WrappedAdapter
		{
			get { return _wrappedAdapter; }
		}
	}
}