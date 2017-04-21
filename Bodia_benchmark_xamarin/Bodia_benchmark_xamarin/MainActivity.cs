using Android.App;
using Android.Widget;
using Android.OS;

namespace Bodia_benchmark_xamarin
{
    [Activity(Label = "Bodia_benchmark_xamarin", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
    }
}

