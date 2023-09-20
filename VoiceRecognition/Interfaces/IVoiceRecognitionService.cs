namespace VoiceRecognition.Interfaces;

public interface IVoiceRecognitionService
{
    Task<string> RecognizeSpeechAsync();
}