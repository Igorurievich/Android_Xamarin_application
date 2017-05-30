using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;

namespace Bodia_benchmark_xamarin.Sources
{
    [Activity(Label = "Bodia Xamarin benchmark", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Spinner _spinner;
        private Button _runChosenTestButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ActivityMain);

            _spinner = FindViewById<Spinner>(Resource.Id.ListOfTestsSpinner);
            _runChosenTestButton = FindViewById<Button>(Resource.Id.run_test_button);
            _runChosenTestButton.Click += _runChosenTestButton_Click;

            InitListOfTests();
        }

        private void _runChosenTestButton_Click(object sender, System.EventArgs e)
        {
            string selectedTest = _spinner.SelectedItem.ToString();
            if (selectedTest == Resources.GetString(Resource.String.test_name_ui_loading))
            {
                StartActivity(typeof(DynamicListViewPerfomanceTestActivity));
            }
            if (selectedTest == Resources.GetString(Resource.String.test_name_start_big_while))
            {
                StartActivity(typeof(WhilePerfomanceTestActivity));
            }
            if (selectedTest == Resources.GetString(Resource.String.test_name_database_operations))
            {
                StartActivity(typeof(DbOperationsPerfomanceTestActivity));
            }
            if (selectedTest == Resources.GetString(Resource.String.test_name_compress_files))
            {
                StartActivity(typeof(CompressFilesPerfomanceTestActivity));
            }
            if (selectedTest == Resources.GetString(Resource.String.test_name_fps_measuring))
            {
                StartActivity(typeof(FpsCameraPeerfomanceTestActivity));
            }
        }

        private void InitListOfTests()
        {
            List<string> testsNamesList = new List<string>()
            {
                Resources.GetString(Resource.String.test_name_ui_loading),
                Resources.GetString(Resource.String.test_name_database_operations),
                Resources.GetString(Resource.String.test_name_start_big_while),
                Resources.GetString(Resource.String.test_name_compress_files),
                Resources.GetString(Resource.String.test_name_fps_measuring)
            };

            ArrayAdapter adapter = new ArrayAdapter(this,
                Android.Resource.Layout.SimpleListItem1, testsNamesList);

            _spinner.Adapter = adapter;
        }


    }
}