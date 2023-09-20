using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Speech;
using VoiceRecognition.Services;

namespace VoiceRecognition;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public static MainActivity Instance;
    
    public const int VOICE_RECOGNITION_REQUEST_CODE = 1001;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Instance = this;
    }
    
    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
        base.OnActivityResult(requestCode, resultCode, data);
    
        if (requestCode == VOICE_RECOGNITION_REQUEST_CODE)
        {
            if (resultCode == Result.Ok)
            {
                var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                if (matches != null && matches.Count > 0)
                {
                    // Broadcast this result or use some method to process it
                    AndroidVoiceRecognitionService.OnReceivedVoiceResult(matches[0]);
                }
            }
        }
    }

}