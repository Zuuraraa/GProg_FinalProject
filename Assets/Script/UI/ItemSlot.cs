using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image graphic;
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI hotkeyLabel;


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

    public void SetSlotActive(bool active)
    {
        background.color = active ? Color.yellow : Color.white;
    }
}
