using System.Collections.Generic;
using Android.App;
using Android.OS;

namespace EndlessScrollPort
{
	[Activity(Label = "EndlessScrollPort", MainLauncher = true, Icon = "@drawable/icon")]
	public class EndlessListActivity : ListActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Create your application here
			SetContentView(Resource.Layout.Main);

			List<int> items = new List<int>();

			for(int i = 0; i < 25; i++) { items.Add(i); }

			DemoAdapter adapter = new DemoAdapter(this, items);
			ListAdapter = adapter;
		}
	}
}

