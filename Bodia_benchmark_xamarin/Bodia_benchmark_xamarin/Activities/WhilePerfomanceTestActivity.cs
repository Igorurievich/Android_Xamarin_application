using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Bodia_benchmark_xamarin.Activities
{
    [Activity]
    public class WhilePerfomanceTestActivity : Activity
    {
        EditText editTextWhileCount;
        TextView firstResultText;
        TextView secondResultText;
        TextView thirdResultText;
        TextView averageTimeResultText;
        Button btnRunWhileTest;

        private int testNumber = 0;

        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ActivityWhilePerfomanceTest);

            editTextWhileCount = (EditText)FindViewById(Resource.Id.input_while_count);
            firstResultText = (TextView)FindViewById(Resource.Id.first_while_test_result);
            secondResultText = (TextView)FindViewById(Resource.Id.second_while_test_result);
            thirdResultText = (TextView)FindViewById(Resource.Id.third_while_test_result);
            averageTimeResultText = (TextView)FindViewById(Resource.Id.arithmetic_main);

            btnRunWhileTest = (Button)FindViewById(Resource.Id.run_while_test_btn);
            btnRunWhileTest.Click += BtnRunWhileTest_Click;
        }

        private void BtnRunWhileTest_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(editTextWhileCount.Text))
            {
                Toast.MakeText(this, Resource.String.message_inser_valid_count, ToastLength.Long).Show();
                return;
            }

            if (testNumber < 3)
            {
                editTextWhileCount.Enabled = false;
                double elapsedTime = EraseWhile(Convert.ToInt64(editTextWhileCount.Text));
                testNumber++;
                FillResults(testNumber, elapsedTime);
            }
            if (testNumber == 3)
            {
                double average = (Convert.ToDouble(firstResultText.Text) +
                         Convert.ToDouble(secondResultText.Text) +
                         Convert.ToDouble(thirdResultText.Text)) / 3;
                averageTimeResultText.Text = average.ToString();
                btnRunWhileTest.Text = Resources.GetString(Resource.String.start_new_tests_series_with_while_btn_label);
                testNumber++;
                return;
            }

            if (btnRunWhileTest.Text == Resources.GetString(Resource.String.start_new_tests_series_with_while_btn_label))
            {
                btnRunWhileTest.Text = Resources.GetString(Resource.String.label_button_run_while);
                ClearTestsResults();
                testNumber = 0;
                editTextWhileCount.Enabled = true;
            }
        }

        private double EraseWhile(long count)
        {
            long tStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (long i = 0; i < count; i++)
            {

            }
            long tEnd = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long tDelta = tEnd - tStart;
            return tDelta / 1000.0;
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