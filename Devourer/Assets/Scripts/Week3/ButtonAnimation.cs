using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private Sprite enter;
    [SerializeField] private Sprite exit;
    [SerializeField] private Sprite click;
    private Image buttonImage;
    private void Awake() {
        buttonImage = GetComponent<Image>();
    }
    public void SetEnter() {
        buttonImage.sprite = enter;
    }
    
    public void SetExit() {
        buttonImage.sprite = exit;
    }
    public void SetClick() {
        buttonImage.sprite = click;
    }
}
