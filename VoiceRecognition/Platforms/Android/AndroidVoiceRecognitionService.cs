using Android.App;
using Android.Content;
using Android.Speech;
using AndroidX.Activity.Result.Contract;
using VoiceRecognition.Interfaces;
using VoiceRecognition.Services;

[assembly: Dependency(typeof(AndroidVoiceRecognitionService))]
namespace VoiceRecognition.Services;

public class AndroidVoiceRecognitionService : IVoiceRecognitionService
{
        private static TaskCompletionSource<string> tcs;
        public const int VOICE_RECOGNITION_REQUEST_CODE = 1001;

        public async Task<string> RecognizeSpeechAsync()
        {
            tcs = new TaskCompletionSource<string>();
            
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Speak now");

            MainActivity.Instance.StartActivityForResult(voiceIntent, VOICE_RECOGNITION_REQUEST_CODE);
            
            return await tcs.Task;
        }

    
        public static void OnReceivedVoiceResult(string result)
        {
            tcs?.SetResult(result);
        }
}
    