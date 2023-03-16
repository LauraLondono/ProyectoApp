// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Provider;
using Android.Media;
using Xamarin.Forms;
using Stream = System.IO.Stream;
using demo_santiago;
using demo_santiago.Droid;

[assembly: Dependency(typeof(MediaManager))]
namespace demo_santiago.Droid
{
    public class MediaManager : IMediaManager
    {
        public static TaskCompletionSource<MediaResult> mediaResult;
        public static int requestCodeSelectPick = 15102;
        public static int requestCodeTakePick = 0;
        //public static int requestCode = 15102;
        public static Java.IO.File _file;
        public static Java.IO.File _dir;
        public static Bitmap bitmap;
        public MediaManager()
        {
            CreateDirectoryForPictures();
        }
        public Task<MediaResult> selectPictureAsync(bool onlyPhoto = false, bool original = false, bool editing = false)
        {
            // Define the Intent for getting images
            Intent intent = new Intent(Intent.ActionPick, Android.Provider.MediaStore.Images.Media.ExternalContentUri);
            //intent.SetType("image/*");
            //            intent.SetAction(Intent.ActionGetContent);

            // Get the MainActivity instance
            MainActivity activity = Android.App.Application.Context as MainActivity;

            // Start the picture-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
               requestCodeSelectPick);

            if (mediaResult != null)
                mediaResult.TrySetCanceled();

            mediaResult = new TaskCompletionSource<MediaResult>();

            // Return Task object
            return mediaResult.Task;
        }

        public Task<MediaResult> takePictureAsync(bool onlyPhoto = false, bool original = false, bool editing = false)
        {
            // Define the Intent for getting images
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            _file = new Java.IO.File(_dir, String.Format("myPhoto_{0}.jpg", "_1"));
            Debug.WriteLine("Photo path: " + _file.AbsolutePath);
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));

            // Get the MainActivity instance
            MainActivity activity = Android.App.Application.Context as MainActivity;

            // Start the take-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(intent, requestCodeTakePick);

            if (mediaResult != null)
                mediaResult.TrySetCanceled();

            mediaResult = new TaskCompletionSource<MediaResult>();

            // Return Task object
            return mediaResult.Task;
        }

        public static Stream resizeImage(Stream imageStream, float maxWidth, float maxHeight)
        {
            var bitmap = BitmapFactory.DecodeStream(imageStream);
            Debug.WriteLine("Bitmap Width: " + bitmap.Width + " height: " + bitmap.Height);
            var maxResizeFactor = Math.Min(maxWidth / bitmap.Width, maxHeight / bitmap.Height);
            if (maxResizeFactor > 1) return imageStream;
            var width = (int)(maxResizeFactor * bitmap.Width);
            var height = (int)(maxResizeFactor * bitmap.Height);
            Bitmap resized = Bitmap.CreateScaledBitmap(bitmap, width, height, true);
            MemoryStream tempStream = new MemoryStream();
            resized.Compress(Bitmap.CompressFormat.Png, 100, tempStream);
            Stream newStream = new MemoryStream(tempStream.ToArray());
            tempStream.Dispose();
            imageStream.Dispose();
            bitmap.Dispose();
            resized.Dispose();
            return newStream;
        }

        public static Stream resizeImage(string path, float maxWidth, float maxHeight)
        {

            Bitmap bitmap = BitmapFactory.DecodeFile(path);
            bitmap = ExifRotateBitmap(path, bitmap);

            MemoryStream tempStream = new MemoryStream();
            Stream newStream = null;
            Debug.WriteLine("Bitmap Width: " + bitmap.Width + " height: " + bitmap.Height);
            var maxResizeFactor = Math.Min(maxWidth / bitmap.Width, maxHeight / bitmap.Height);
            if (maxResizeFactor > 1)
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, tempStream);
                newStream = new MemoryStream(tempStream.ToArray());
                tempStream.Dispose();
                bitmap.Dispose();
                return newStream;
            }
            var width = (int)(maxResizeFactor * bitmap.Width);
            var height = (int)(maxResizeFactor * bitmap.Height);
            Bitmap resized = Bitmap.CreateScaledBitmap(bitmap, width, height, true);
            resized.Compress(Bitmap.CompressFormat.Jpeg, 100, tempStream);
            newStream = new MemoryStream(tempStream.ToArray());
            tempStream.Dispose();
            bitmap.Dispose();
            resized.Dispose();
            return newStream;
        }

        private static Bitmap ExifRotateBitmap(string filePath, Bitmap bitmap)
        {
            if (bitmap == null)
                return null;

            var exif = new ExifInterface(filePath);
            var rotation = exif.GetAttributeInt(ExifInterface.TagOrientation, (int)Orientation.Normal);
            var rotationInDegrees = ExifToDegrees(rotation);
            if (rotationInDegrees == 0)
                return bitmap;
            using (var matrix = new Matrix())
            {
                matrix.PreRotate(rotationInDegrees);
                var bitmapRotated = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
                bitmap.Dispose();
                return bitmapRotated;
            }
        }

        private static int ExifToDegrees(int exifOrientation)
        {
            switch (exifOrientation)
            {
                case (int)Orientation.Rotate90:
                    return 90;
                case (int)Orientation.Rotate180:
                    return 180;
                case (int)Orientation.Rotate270:
                    return 270;
                default:
                    return 0;
            }
        }

        private void CreateDirectoryForPictures()
        {
            _dir = new Java.IO.File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "ValetNowPhotos");
            if (!_dir.Exists())
            {
                _dir.Mkdirs();
                Debug.WriteLine("Photos Dir created: " + _dir.AbsolutePath);
            }
            else
            {
                Debug.WriteLine("Photos Dir Already Exist!!: " + _dir.AbsolutePath);
            }
        }
    }
}
