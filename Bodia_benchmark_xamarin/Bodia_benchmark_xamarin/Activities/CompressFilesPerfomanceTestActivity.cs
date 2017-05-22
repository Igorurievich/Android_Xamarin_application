using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Content.Res;
using System.IO;

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
            string content;
            AssetManager assets = Assets;
            using (StreamReader sr = new StreamReader(assets.Open("read_asset.txt")))
            {
                content = sr.ReadToEnd();
            }


            try
            {
                assets = Assets.List();

                if (assets.length == 0)
                {
                    CopyFile(path);
                }
                else
                {
                    String fullPath = getFilesDir() + File.separator + path;
                    File dir = new File(fullPath);
                    if (!dir.exists())
                        dir.mkdir();
                    for (int i = 0; i < assets.length; ++i)
                    {
                        СopyFileOrDir(path + "/" + assets[i]);
                    }
                }
            }
            catch (IOException ex)
            {
                Log.e("tag", "I/O Exception", ex);
            }
        }

        private void CopyFile(object path)
        {
            throw new NotImplementedException();
        }
    }
}