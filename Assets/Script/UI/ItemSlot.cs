using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    [SerializeField] Image graphic;
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI counterLabel;
    [SerializeField] TextMeshProUGUI hotkeyLabel;

    public bool hasCounter = false;


    private void Awake()
    {
        counterLabel.enabled = hasCounter;
    }

    public void SetHotkeyLabel(int keyLabel)
    {
        hotkeyLabel.text = keyLabel.ToString();
    }

    public void SetImage(Sprite sprite)
    {
        graphic.sprite = sprite;
        if (sprite.rect.size.x > sprite.rect.size.y)
        {
            graphic.rectTransform.localScale = new Vector3(1, sprite.rect.size.y / sprite.rect.size.x, 1); 
        }
        else if(sprite.rect.size.x < sprite.rect.size.y)
        {
            graphic.rectTransform.localScale = new Vector3(sprite.rect.size.x / sprite.rect.size.y, 1, 1);
        }
        else
        {
            graphic.rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetSlotSelected(bool active)
    {
        background.color = active ? Color.yellow : Color.white;
    }

    public void SetSlotUnlocked(bool active)
    {
        graphic.color = active ? Color.white: new Color(.2f, .2f, .2f, 1);
    }

    public void SetCounterLabel(int count)
    {
        counterLabel.text = count.ToString();
    }
}
