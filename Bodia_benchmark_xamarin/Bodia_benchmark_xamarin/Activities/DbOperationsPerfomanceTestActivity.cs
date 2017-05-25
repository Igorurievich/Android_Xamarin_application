using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Bodia_benchmark_xamarin.Sources.DBHelpers;
using Bodia_benchmark_xamarin.Sources.DbModels;
using System;
using System.Collections.Generic;

namespace Bodia_benchmark_xamarin.Sources
{
    [Activity]
    public class DbOperationsPerfomanceTestActivity : Activity
    {
        DbRespository dbRepository;
        EditText inputedRecordsCount;
        TextView firstResultText;
        TextView secondResultText;
        TextView thirdResultText;
        TextView averageTimeResultText;
        Button btnRunDBTest;

        private int testNumber = 0;

        double inseringTime;
        double updatingTime;
        double deletingTime;

        double averageInseringTime;
        double averageUpdatingTime;
        double averageDeletingTime;

        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ActivityDbOperationsPerfomanceTest);

            dbRepository = new DbRespository();

            dbRepository.CreateDb();
            dbRepository.CreateTable();

            inputedRecordsCount = (EditText)FindViewById(Resource.Id.input_records_count);
            firstResultText = (TextView)FindViewById(Resource.Id.first_db_test_result);
            secondResultText = (TextView)FindViewById(Resource.Id.second_db_test_result);
            thirdResultText = (TextView)FindViewById(Resource.Id.third_db_test_result);
            averageTimeResultText = (TextView)FindViewById(Resource.Id.arithmetic_main_db_test);
            btnRunDBTest = (Button)FindViewById(Resource.Id.run_db_test_button);
            btnRunDBTest.Click += BtnRunDBTest_Click;
        }

        private void BtnRunDBTest_Click(object sender, System.EventArgs e)
        {
            if (inputedRecordsCount.Text == null || string.IsNullOrEmpty(inputedRecordsCount.Text))
            {
                Toast.MakeText(this, Resource.String.message_inser_valid_count, ToastLength.Long).Show();
                return;
            }
            if (testNumber < 3)
            {
                inputedRecordsCount.Enabled = false;
                EraseDatabaseTest(Convert.ToInt64(inputedRecordsCount.Text));
                testNumber++;
                FillResults(testNumber);
                averageInseringTime = averageInseringTime + inseringTime;
                averageUpdatingTime = averageUpdatingTime + updatingTime;
                averageDeletingTime = averageDeletingTime + deletingTime;
            }
            if (testNumber == 3)
            {
                averageTimeResultText.Text = (averageInseringTime / 3) + ", " + (averageUpdatingTime / 3) + ", " + (averageDeletingTime / 3);
                btnRunDBTest.Text = Resources.GetString(Resource.String.start_new_tests_series_with_while_btn_label);
                testNumber++;
                averageInseringTime = 0;
                averageUpdatingTime = 0;
                averageDeletingTime = 0;
                return;
            }

            if (btnRunDBTest.Text == Resources.GetString(Resource.String.start_new_tests_series_with_while_btn_label))
            {
                btnRunDBTest.Text = Resources.GetString(Resource.String.label_button_run_while);
                ClearTestsResults();
                testNumber = 0;
                inputedRecordsCount.Enabled = true;
            }

            
        }
        private void ClearTestsResults()
        {
            firstResultText.Text = String.Empty;
            secondResultText.Text = String.Empty;
            thirdResultText.Text = String.Empty;
            averageTimeResultText.Text = String.Empty;
        }

        public double InsertUsers(long recordsCount)
        {
            long tStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < recordsCount; i++)
            {
                String name = Guid.NewGuid().ToString();
                String surname = Guid.NewGuid().ToString();
                dbRepository.InsertRecord(new UserData(name, surname, DateTime.Now));
            }
            long tEnd = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long tDelta = tEnd - tStart;
            return tDelta / 1000.0;
        }

        public double DeletingUsers()
        {
            List<UserData> userArrayList = dbRepository.GetAllRecords();
            long tStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            foreach (var user in userArrayList)
            {
                dbRepository.RemoveUserById(user.ID);
            }
            long tEnd = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long tDelta = tEnd - tStart;
            return tDelta / 1000.0;
        }

        public void EraseDatabaseTest(long recordsCount)
        {
            inseringTime = InsertUsers(recordsCount);
            updatingTime = UpdateUsers();
            deletingTime = DeletingUsers();
        }

        public double UpdateUsers()
        {
            List<UserData> userArrayList = dbRepository.GetAllRecords();
            long tStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            foreach (var user in userArrayList)
            {
                String name = Guid.NewGuid().ToString();
                String surname = Guid.NewGuid().ToString();

                user.BirthDate = DateTime.Now;
                user.Surname = surname;
                user.Name = name;

                dbRepository.UpdateRecord(user);
            }
            long tEnd = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long tDelta = tEnd - tStart;
            return tDelta / 1000.0;
        }

        private void FillResults(int numberOfTest)
        {
            switch (numberOfTest)
            {
                case 1:
                    firstResultText.Text= inseringTime + ", " + updatingTime + ", " + deletingTime;
                    break;
                case 2:
                    secondResultText.Text = inseringTime + ", " + updatingTime + ", " + deletingTime;
                    break;
                case 3:
                    thirdResultText.Text = inseringTime + ", " + updatingTime + ", " + deletingTime;
                    break;
            }
        }
    }
}