using Android.App;
using Android.OS;
using Android.Views;

namespace Bodia_benchmark_xamarin.Sources
{
    [Activity]
    public class WhilePerfomanceTestActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ActivityWhilePerfomanceTest);
        }
    }
}