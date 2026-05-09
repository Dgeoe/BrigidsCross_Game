using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsSliderController : MonoBehaviour
{
    [Header("TARGET SLIDERS")]
    public Slider brightnessSlider;
    public Slider audioSlider;

    [Header("BRIGHTNESS")]
    public CanvasGroup brightnessOverlay;

    [Range(0f, 1f)]
    public float maxDarkness = 0.75f; 

    [Header("AUDIO")]
    public AudioMixer masterMixer;

    public string volumeParameter = "MasterVolume";

    [Header("ICON IMAGES")]
    public Image brightnessIcon;
    public Image audioIcon;

    [Header("SPRITE ARRAYS")]
    public Sprite[] brightnessSprites;
    public Sprite[] audioSprites;

    [Header("HANDLE ROTATION")]
    public RectTransform brightnessHandle;
    public RectTransform audioHandle;
    public float spinAmount = 900f;

    private void Start()
    {
        // Load saved values
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        float savedAudio = PlayerPrefs.GetFloat("Audio", 1f);

        brightnessSlider.value = savedBrightness;
        audioSlider.value = savedAudio;

        // Add listeners
        brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
        audioSlider.onValueChanged.AddListener(UpdateAudio);

        UpdateBrightness(savedBrightness);
        UpdateAudio(savedAudio);
    }

    public void UpdateBrightness(float value)
    {
        float darkness = Mathf.Lerp(maxDarkness, 0f, value);

        if (brightnessOverlay != null)
            brightnessOverlay.alpha = darkness;

        UpdateSpriteFromArray(value, brightnessSprites, brightnessIcon);
        RotateHandle(brightnessHandle, value);

        PlayerPrefs.SetFloat("Brightness", value);
    }

    public void UpdateAudio(float value)
    {
        float volume;

        if (value <= 0.001f)
        {
            volume = -80f; // mute
        }
        else
        {
            volume = Mathf.Log10(value) * 20f;
        }

        masterMixer.SetFloat(volumeParameter, volume);

        UpdateSpriteFromArray(value, audioSprites, audioIcon);
        RotateHandle(audioHandle, value);

        PlayerPrefs.SetFloat("Audio", value);
    }
    private void UpdateSpriteFromArray(float value, Sprite[] sprites, Image targetImage)
    {
        if (sprites == null || sprites.Length == 0 || targetImage == null)
            return;

        int index = Mathf.RoundToInt(value * (sprites.Length - 1));
        index = Mathf.Clamp(index, 0, sprites.Length - 1);

        targetImage.sprite = sprites[index];
    }

    private void RotateHandle(RectTransform handle, float value)
    {
        if (handle == null)
            return;

        float rotation;

        if (value <= 0f || value >= 1f)
        {
            rotation = 0f;
        }
        else
        {
            // Sin curve so it returns to zero at both ends (ik ik its not ideal but like im tired man)
            rotation = Mathf.Sin(value * Mathf.PI) * spinAmount;
        }

        handle.localEulerAngles = new Vector3(0f, 0f, rotation);
    }
}
