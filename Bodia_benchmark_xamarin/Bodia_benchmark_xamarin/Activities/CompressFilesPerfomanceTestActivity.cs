using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.IO;
using Android.Content.Res;

namespace Bodia_benchmark_xamarin.Sources
{
    [Activity]
    public class CompressFilesPerfomanceTestActivity : Activity
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

            CopyFileOrDir("FilesForCompress");
        }

        private void CopyFileOrDir(string v)
        {
            using (var assets = Assets.Open("FilesForCompress/file0"))
            using (var dest = File.Create(Android.App.Application.Context.FilesDir.AbsolutePath, 65535, FileOptions.None))
                assets.CopyTo(dest);
        }
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }



        private void CopyFile(object path)
        {
            throw new NotImplementedException();
        }


    }
}