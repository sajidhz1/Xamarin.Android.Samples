using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace EndlessScrollPort
{
	public class DemoAdapter : EndlessAdapterBase
	{
		private readonly RotateAnimation _rotateAnimation = null;
		private View _pendingItemView = null;

		public DemoAdapter(Context ctxt, List<int> list)
			: base(new ArrayAdapter<int>(ctxt,
																	 Resource.Layout.row,
																	 global::Android.Resource.Id.Text1,
																	 list))
		{
			_rotateAnimation = new RotateAnimation(0f, 360f, Dimension.RelativeToSelf,
																	 0.5f, Dimension.RelativeToSelf,
																	 0.5f);

			_rotateAnimation.Duration = 600;
			_rotateAnimation.RepeatMode = RepeatMode.Restart;
			_rotateAnimation.RepeatCount = Animation.Infinite;
		}

		protected override View GetPendingItemView(ViewGroup parent)
		{
			View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row, null);

			_pendingItemView = row.FindViewById(global::Android.Resource.Id.Text1);
			_pendingItemView.Visibility = ViewStates.Gone;
			_pendingItemView = row.FindViewById(Resource.Id.throbber);
			_pendingItemView.Visibility = ViewStates.Visible;
			StartProgressAnimation();

			return (row);
		}

		protected override async Task<bool> LoadData()
		{
			bool result = false;

			if(WrappedAdapter.Count < 75)
			{
				await Task.Delay(2000);
				result = true;
			}

			return result;
		}

		protected override void AppendCachedData()
		{
			if(WrappedAdapter.Count < 75)
			{
				ArrayAdapter<int> a = (ArrayAdapter<int>)WrappedAdapter;

				for(int i = 0; i < 25; i++)
					a.Add(a.Count);
			}
		}

		private void StartProgressAnimation()
		{
			if(_pendingItemView != null)
			{
				_pendingItemView.StartAnimation(_rotateAnimation);
			}
		}
	}
}