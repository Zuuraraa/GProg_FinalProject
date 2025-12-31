using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAction : MonoBehaviour
{

    enum Order { Previous, Next };

    public static bool isAttacking = false;
    public static bool freezeAiming = false;
    public static Item currentItem;

    [Header("References")]
    [SerializeField] Player player;
    [SerializeField] GameObject anchor;
    [SerializeField] SpriteRenderer[] sprites;


    [Header("Items")]
    [SerializeField] Item[] items;
    [SerializeField] int currentItemIdx;
    
    int itemsCount;
    

    Vector3 mousePos;

    private void Awake()
    {
        currentItem = items[currentItemIdx];
        itemsCount = items.Length;
    }

    void Update()
    {
        if (!freezeAiming)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            FaceMouse();
        }

        currentItem.CheckUse();
        if (currentItem.ValidToSwapOut())
        {
            CheckItemSwap();
        }
    }

    void CheckItemSwap()
    {
        if (Input.inputString != "")
        {
            int number;
            bool is_a_number = Int32.TryParse(Input.inputString, out number);
            if (is_a_number && number >= 1 && number <= itemsCount)
            {
                int idx = number - 1;
                if (items[idx].ValidToSwapInto())
                {
                    SwapItem(idx);
                    return;
                }
            }
        }

        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta > 0)
        {
            SwapItem(GetValidItemIndex(Order.Next));
        } 
        else if (scrollDelta < 0)
        {
            SwapItem(GetValidItemIndex(Order.Previous));
        }
    }

    void SwapItem(int idx)
    {
        currentItemIdx = idx;
        currentItem = items[currentItemIdx];
    }

    int GetValidItemIndex(Order order)
    {
        int itemOrder = order == Order.Next ? 1 : -1;
        for (int i = 1; i < 5; i++)
        {
            int testIndex = (currentItemIdx + itemOrder * i) % itemsCount;
            testIndex = testIndex < 0 ? itemsCount + testIndex : testIndex;
            Debug.Log(testIndex);
            if (items[testIndex].ValidToSwapInto())
            {
                return testIndex;
            }
        }

        return currentItemIdx;
    }


    void FaceMouse()
    {
        Vector3 rotation = mousePos - anchor.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        anchor.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        player.GetAnimator().SetInteger("Facing", rotation.x >= 0 ? 1 : -1);

        if (rotation.x != 0)
        {
            FlipSprite(rotation.x < 0);
        }
    }


    void FlipSprite(bool value)
    {
        foreach (SpriteRenderer s in sprites)
        {
            s.flipX = value;
        }
    }


    public bool CheckAttack()
    {
        Weapon weapon = currentItem as Weapon;
        if (weapon.info.canHoldFire)
        {
            return Input.GetMouseButton(0);
        }
        else
        {
            return Input.GetMouseButtonDown(0);
        }
    }

}