using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using System.IO.Compression;

namespace Bodia_benchmark_xamarin.Sources
{
    [Android.App.Activity]
    public class CompressFilesPerfomanceTestActivity : Android.App.Activity
    {
        TextView firstResultText;
        TextView secondResultText;
        TextView thirdResultText;
        TextView averageTimeResultText;
        Button btnRunCompressTest;

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
        }

        private void BtnRunCompressTest_Click(object sender, EventArgs e)
        {
            if (File.Exists(Globals.PathToCompressedZipFile))
            {
                File.Delete(Globals.PathToCompressedZipFile);
            }
            if (testNumber < 3)
            {
                double elapsedTime = EraseWhile();
                testNumber++;
                FillResults(testNumber, elapsedTime);
            }
            if (testNumber == 3)
            {
                double average = (Convert.ToDouble(firstResultText.Text) +
                        Convert.ToDouble(secondResultText.Text) +
                        Convert.ToDouble(thirdResultText.Text)) / 3;
                averageTimeResultText.Text = average.ToString();
                btnRunCompressTest.Text = Resources.GetString(Resource.String.start_new_tests_series_with_while_btn_label);
                testNumber++;
                return;
            }
            if (btnRunCompressTest.Text == Resources.GetString(Resource.String.start_new_tests_series_with_while_btn_label))
            {
                btnRunCompressTest.Text = Resources.GetString(Resource.String.label_button_run_while);
                ClearTestsResults();
                testNumber = 0;
            }
        }

        private double EraseWhile()
        {
            long tStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            ZipFile.CreateFromDirectory(Globals.PathToFilesForCompressFolder, Globals.PathToCompressedZipFile);
            try
            {
                ZipFile.ExtractToDirectory(Globals.PathToCompressedZipFile, Globals.PathToUncompressFilesFolder);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            long tEnd = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long tDelta = tEnd - tStart;
            File.Delete(Globals.PathToCompressedZipFile);
            return tDelta / 1000.0;
        }

        private void FillResults(int numberOfTest, double time)
        {
            switch (numberOfTest)
            {
                case 1:
                    firstResultText.Text = Convert.ToString(time);
                    break;
                case 2:
                    secondResultText.Text = Convert.ToString(time);
                    break;
                case 3:
                    thirdResultText.Text = Convert.ToString(time);
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