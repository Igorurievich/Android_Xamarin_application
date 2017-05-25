using Android.App;
using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace Bodia_benchmark_xamarin.Sources
{
    [Activity]
    public class DynamicListViewPerfomanceTestActivity : Activity
    {
        EditText editTextWhileCount;
        TextView firstResultText;
        TextView secondResultText;
        TextView thirdResultText;
        TextView averageTimeResultText;
        Button btnRunListViewTest;
        ListView testListView;
        private int testNumber = 0;

        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ActivityDynamicListViewPerfomanceTest);

            List<string> data = new List<string>();
            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, data);
            
            testListView = (ListView)FindViewById(Resource.Id.test_List_View);
            editTextWhileCount = (EditText)FindViewById(Resource.Id.input_listview_items_count);
            firstResultText = (TextView)FindViewById(Resource.Id.first_listview_test_result);
            secondResultText = (TextView)FindViewById(Resource.Id.second_listview_test_result);
            thirdResultText = (TextView)FindViewById(Resource.Id.third_listview_test_result);
            averageTimeResultText = (TextView)FindViewById(Resource.Id.arithmetic_main_listview_test);

            btnRunListViewTest = (Button)FindViewById(Resource.Id.addItem);
            btnRunListViewTest.Click += BtnRunListViewTest_Click;

            testListView.Adapter = adapter;
        }

        private void BtnRunListViewTest_Click(object sender, System.EventArgs e)
        {
            if (editTextWhileCount.Text == null || string.IsNullOrEmpty(editTextWhileCount.Text))
            {
                Toast.MakeText(this, Resource.String.message_inser_valid_count, ToastLength.Long).Show();
                return;
            }
            if (testNumber < 3)
            {
                editTextWhileCount.Enabled = false;
                double elapsedTime = EraseDynamiclyItemsInserting(Convert.ToInt64(editTextWhileCount.Text));
                testNumber++;
                FillResults(testNumber, elapsedTime);
            }
            if (testNumber == 3)
            {
                double average = (Convert.ToDouble(firstResultText.Text) +
                         Convert.ToDouble(secondResultText.Text) +
                         Convert.ToDouble(thirdResultText.Text)) / 3;
                averageTimeResultText.Text = average.ToString();
                btnRunListViewTest.Text = Resources.GetText(Resource.String.start_new_tests_series_with_while_btn_label);
                testNumber++;
                ClearListView();
                return;
            }
            if (btnRunListViewTest.Text == Resources.GetString(Resource.String.start_new_tests_series_with_while_btn_label))
            {
                btnRunListViewTest.Text = Resources.GetString(Resource.String.label_button_run_while);
                ClearTestsResults();
                testNumber = 0;
                editTextWhileCount.Enabled = true;
            }
        }
        private double EraseDynamiclyItemsInserting(long count)
        {
            long tStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            ArrayAdapter adapter = (ArrayAdapter)testListView.Adapter;
            for (long i = 0; i < count; i++)
            {
                String device = "Item: " + i;
                adapter.Add(device);
                adapter.NotifyDataSetChanged();
            }
            long tEnd = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long tDelta = tEnd - tStart;
            return tDelta / 1000.0;
        }
        private void ClearListView()
        {
            ArrayAdapter adapter = (ArrayAdapter)testListView.Adapter;
            adapter.Clear();
            adapter.NotifyDataSetChanged();
        }

        private void ClearTestsResults()
        {
            firstResultText.Text = String.Empty;
            secondResultText.Text = String.Empty;
            thirdResultText.Text = String.Empty;
            averageTimeResultText.Text = String.Empty;
        }

        private void FillResults(int numberOfTest, double time)
        {
            switch (numberOfTest)
            {
                case 1:
                    firstResultText.Text = time.ToString();
                    break;
                case 2:
                    secondResultText.Text = time.ToString();
                    break;
                case 3:
                    thirdResultText.Text = time.ToString();
                    break;
            }
        }
    }
}