using VoiceRecognition.Interfaces;

namespace VoiceRecognition;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }
    
    void OnEditorTextChanged(object sender, TextChangedEventArgs e)
    {
        double initialHeight = 50; 
        double heightPerLine = 20; 
        var lineCount = ResizableEditor.Text.Split('\n').Length;
        ResizableEditor.HeightRequest = initialHeight + (lineCount * heightPerLine);
    }

    private async void ONVoiceClicked(object sender, EventArgs e)
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.Microphone>();
            if (status != PermissionStatus.Granted)
                return; 
        }
        
        var voiceService = DependencyService.Get<IVoiceRecognitionService>();
        var transcribedText = await voiceService.RecognizeSpeechAsync();
        ResizableEditor.Text = transcribedText;
    }
}