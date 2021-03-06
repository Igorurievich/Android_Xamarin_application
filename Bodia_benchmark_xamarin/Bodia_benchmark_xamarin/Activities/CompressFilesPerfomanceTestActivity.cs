﻿using System;
using System.IO;
using System.IO.Compression;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Bodia_benchmark_xamarin.Activities
{
    [Android.App.Activity]
    public class CompressFilesPerfomanceTestActivity : Android.App.Activity
    {
        TextView firstResultText;
        TextView secondResultText;
        TextView thirdResultText;
        TextView averageTimeResultText;
        Button btnRunCompressTest;

        double compressTime;
        double unCompressTime;

        double averageCompressTime;
        double averageUnCompressTime;

        private int testNumber = 0;

        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ActivityCompressFilesPerfomanceTest);

            firstResultText = (TextView)FindViewById(Resource.Id.first_compressing_test_result);
            secondResultText = (TextView)FindViewById(Resource.Id.second_compressing_test_result);
            thirdResultText = (TextView)FindViewById(Resource.Id.third_compressing_test_result);
            averageTimeResultText = (TextView)FindViewById(Resource.Id.arithmetic_main_compressing_test);
            btnRunCompressTest = (Button)FindViewById(Resource.Id.run_compress_test_button);
            btnRunCompressTest.Click += BtnRunCompressTest_Click;

            CopyFilesForCompressFromAssets();

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = Resources.GetString(Resource.String.test_name_compress_files);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Directory.Delete(Globals.ApplicationFilesFolder, true);
            Finish();
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.compress_activity_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void BtnRunCompressTest_Click(object sender, EventArgs e)
        {
            if (File.Exists(Globals.PathToCompressedZipFile))
            {
                File.Delete(Globals.PathToCompressedZipFile);
            }
            if (testNumber < 3)
            {
                EraseWhile();
                testNumber++;
                FillResults(testNumber);
                averageCompressTime = averageCompressTime + compressTime;
                averageUnCompressTime = averageUnCompressTime + unCompressTime;
            }
            if (testNumber == 3)
            {
                averageTimeResultText.Text = (averageCompressTime / 3) + ", " + (averageUnCompressTime / 3);
                btnRunCompressTest.Text = Resources.GetString(Resource.String.start_new_tests_series_with_while_btn_label);
                testNumber++;
                averageCompressTime = 0;
                averageUnCompressTime = 0;
                return;
            }
            if (btnRunCompressTest.Text == Resources.GetString(Resource.String.start_new_tests_series_with_while_btn_label))
            {
                btnRunCompressTest.Text = Resources.GetString(Resource.String.label_button_run_while);
                ClearTestsResults();
                testNumber = 0;
            }
        }

        private double CompressFiles()
        {
            if (File.Exists(Globals.PathToCompressedZipFile))
            {
                File.Delete(Globals.PathToCompressedZipFile);
            }
            long tStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            ZipFile.CreateFromDirectory(Globals.PathToFilesForCompressFolder, Globals.PathToCompressedZipFile);
            long tEnd = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long tDelta = tEnd - tStart;
            return tDelta / 1000.0;
        }

        private double UnCompressFiles()
        {
            if (Directory.Exists(Globals.PathToUncompressFilesFolder))
            {
                Directory.Delete(Globals.PathToUncompressFilesFolder, true);
            }
            long tStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            ZipFile.ExtractToDirectory(Globals.PathToCompressedZipFile, Globals.PathToUncompressFilesFolder);
            long tEnd = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long tDelta = tEnd - tStart;
            return tDelta / 1000.0;
        }

        private void EraseWhile()
        {
            compressTime = CompressFiles();
            unCompressTime = UnCompressFiles();
        }

        private void FillResults(int numberOfTest)
        {
            switch (numberOfTest)
            {
                case 1:
                    firstResultText.Text = Convert.ToString(compressTime) +"/"+ Convert.ToString(unCompressTime);
                    break;
                case 2:
                    secondResultText.Text = Convert.ToString(compressTime) + "/" + Convert.ToString(unCompressTime);
                    break;
                case 3:
                    thirdResultText.Text = Convert.ToString(compressTime) + "/" + Convert.ToString(unCompressTime);
                    break;
            }
        }

        private void ClearTestsResults()
        {
            firstResultText.Text = String.Empty;
            secondResultText.Text = String.Empty;
            thirdResultText.Text = String.Empty;
            averageTimeResultText.Text = String.Empty;
        }

        private void CopyFilesForCompressFromAssets()
        {

            string[] filesForCompress = Assets.List(Globals.FilesForCompressFolderName);
            if (Directory.Exists(Globals.PathToFilesForCompressFolder))
            {
                Directory.Delete(Globals.PathToFilesForCompressFolder, true);
            }
            Directory.CreateDirectory(Globals.PathToFilesForCompressFolder);
            foreach (var file in filesForCompress)
            {
                byte[] buffer = ReadFully(Assets.Open(Path.Combine(Globals.FilesForCompressFolderName, file)));
                File.WriteAllBytes(Path.Combine(Globals.PathToFilesForCompressFolder, file), buffer);
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}