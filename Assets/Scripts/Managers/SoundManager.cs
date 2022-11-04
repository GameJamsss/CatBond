using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] Slider volumeSlider;

        private void Start()
        {
            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1f);
            }

            Load();
        }

        public void ChangeVolume()
        {
            AudioListener.volume = volumeSlider.value;
            Save();
        }

        private void Load()
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        }
    }
}
