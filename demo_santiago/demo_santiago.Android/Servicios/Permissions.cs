// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Threading.Tasks;
using Android;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using demo_santiago.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Permissions))]
namespace demo_santiago.Droid
{
    public class Permissions : IPermissions
    {
        #pragma warning disable CS0618 // Type or member is obsolete
        MainActivity MainActivity_ContextForms = Forms.Context as MainActivity;
        #pragma warning restore CS0618 // Type or member is obsolete
        public static TaskCompletionSource<bool> TaskCompletion_TaskObject;
        Permission permissionStatus;

        Task<bool> IPermissions.PermissionMethod(string permission)
        {
            TaskCompletion_TaskObject = new TaskCompletionSource<bool>();

            if ((int)Build.VERSION.SdkInt >= 23)
            {
                switch (permission)
                {
                    case "camera":
                        permissionStatus = ContextCompat.CheckSelfPermission(MainActivity_ContextForms, Manifest.Permission.Camera);
                        if (permissionStatus != Permission.Granted)
                        {
                            ActivityCompat.RequestPermissions(MainActivity_ContextForms, new string[] { Manifest.Permission.Camera }, 102);
                        }
                        else
                        {
                            TaskCompletion_TaskObject.SetResult(true);
                        }
                        break;
                    case "location":
                        permissionStatus = ContextCompat.CheckSelfPermission(MainActivity_ContextForms, Manifest.Permission.AccessFineLocation);
                        if (permissionStatus != Permission.Granted)
                        {
                            ActivityCompat.RequestPermissions(MainActivity_ContextForms, new string[] { Manifest.Permission.AccessFineLocation }, 101);
                        }
                        else
                        {
                            TaskCompletion_TaskObject.SetResult(true);
                        }
                        break;
                    case "storage":
                        permissionStatus = ContextCompat.CheckSelfPermission(MainActivity_ContextForms, Manifest.Permission.WriteExternalStorage);
                        if (permissionStatus != Permission.Granted)
                        {
                            ActivityCompat.RequestPermissions(MainActivity_ContextForms, new string[] { Manifest.Permission.WriteExternalStorage }, 103);
                        }
                        else
                        {
                            TaskCompletion_TaskObject.SetResult(true);
                        }
                    break;

                    case "ACCESS_FINE_LOCATION":
                        permissionStatus = ContextCompat.CheckSelfPermission(MainActivity_ContextForms, Manifest.Permission.AccessFineLocation);
                        if (permissionStatus != Permission.Granted)
                        {
                            ActivityCompat.RequestPermissions(MainActivity_ContextForms, new string[] { Manifest.Permission.AccessFineLocation }, 1);
                        }
                        else
                        {
                            TaskCompletion_TaskObject.SetResult(true);
                        }
                        break;

                    case "ACCESS_BACKGROUND_LOCATION":
                        permissionStatus = ContextCompat.CheckSelfPermission(MainActivity_ContextForms, Manifest.Permission.AccessBackgroundLocation);
                        if (permissionStatus != Permission.Granted)
                        {
                            ActivityCompat.RequestPermissions(MainActivity_ContextForms, new string[] { Manifest.Permission.AccessBackgroundLocation }, 1);
                        }
                        else
                        {
                            TaskCompletion_TaskObject.SetResult(true);
                        }
                        break;
                    case "bluetooth":
                        permissionStatus = ContextCompat.CheckSelfPermission(MainActivity_ContextForms, Manifest.Permission.Bluetooth);
                        if (permissionStatus != Permission.Granted)
                        {
                            ActivityCompat.RequestPermissions(MainActivity_ContextForms, new string[] { Manifest.Permission.Bluetooth }, 1);
                        }
                        else
                        {
                            TaskCompletion_TaskObject.SetResult(true);
                        }
                     break;

                    case "BLUETOOTH_ADMIN":
                        permissionStatus = ContextCompat.CheckSelfPermission(MainActivity_ContextForms, Manifest.Permission.BluetoothAdmin);
                        if (permissionStatus != Permission.Granted)
                        {
                            ActivityCompat.RequestPermissions(MainActivity_ContextForms, new string[] { Manifest.Permission.BluetoothAdmin }, 1);
                        }
                        else
                        {
                            TaskCompletion_TaskObject.SetResult(true);
                        }
                        break;

                    case "BLUETOOTH_SCAN":
                        permissionStatus = ContextCompat.CheckSelfPermission(MainActivity_ContextForms, Manifest.Permission.BluetoothScan);
                        if (permissionStatus != Permission.Granted)
                        {
                            ActivityCompat.RequestPermissions(MainActivity_ContextForms, new string[] { Manifest.Permission.BluetoothScan }, 1);
                        }
                        else
                        {
                            TaskCompletion_TaskObject.SetResult(true);
                        }
                        break;
                    case "BLUETOOTH_CONNECT":
                        permissionStatus = ContextCompat.CheckSelfPermission(MainActivity_ContextForms, Manifest.Permission.BluetoothConnect);
                        if (permissionStatus != Permission.Granted)
                        {
                            ActivityCompat.RequestPermissions(MainActivity_ContextForms, new string[] { Manifest.Permission.BluetoothConnect }, 1);
                        }
                        else
                        {
                            TaskCompletion_TaskObject.SetResult(true);
                        }
                        break;


                }
            }
            else
            {
                TaskCompletion_TaskObject.SetResult(true);
            }

            return TaskCompletion_TaskObject.Task;
        }
    }
}