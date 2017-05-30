using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Java.IO;
using System;

namespace Bodia_benchmark_xamarin.Sources
{
    [Activity]
    public class FpsCameraPeerfomanceTestActivity : Activity
    {
        TextView fpsRate;
        Button btnRunFpsTest;

        readonly int REQUEST_VIDEO_CAPTURE = 1;

        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ActivityFpsCameraPerfomanceTest);


            fpsRate = (TextView)FindViewById(Resource.Id.fps_value);

            btnRunFpsTest = (Button)FindViewById(Resource.Id.start_fps_test);
            btnRunFpsTest.Click += BtnRunFpsTest_Click;
        }

        private void BtnRunFpsTest_Click(object sender, EventArgs e)
        {
            DispatchTakeVideoIntent();
        }

        private void DispatchTakeVideoIntent()
        {
            Intent takeVideoIntent = new Intent(MediaStore.ActionVideoCapture);
            if (takeVideoIntent.ResolveActivity(PackageManager) != null)
            {
                StartActivityForResult(takeVideoIntent, REQUEST_VIDEO_CAPTURE);
                
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == REQUEST_VIDEO_CAPTURE && resultCode == Result.Ok)
            {
                Android.Net.Uri videoUri = data.Data;
                DoSmth(videoUri);
            }
        }

        private void DoSmth(Android.Net.Uri uriVideo)
        {
            MediaExtractor extractor = new MediaExtractor();
            int frameRate = 24;
            try
            {
                extractor.SetDataSource(uriVideo.Path);
                int numTracks = extractor.TrackCount;
                for (int i = 0; i < numTracks; ++i)
                {
                    MediaFormat format = extractor.GetTrackFormat(i);
                    String mime = format.GetString(MediaFormat.KeyMime);
                    if (mime.StartsWith("video/"))
                    {
                        if (format.ContainsKey(MediaFormat.KeyFrameRate))
                        {
                            frameRate = format.GetInteger(MediaFormat.KeyFrameRate);
                            fpsRate.Text = frameRate.ToString();
                        }
                    }
                }
            }
            catch (IOException e)
            {
                e.PrintStackTrace();
            }
            finally
            {
                extractor.Release();
            }
        }
    }
}