using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MarketPlaceItem : MonoBehaviour
    {
        public TMP_Text itemamount;
        public Image itemImage;


        public void Set(string text, Sprite pic)
        {
            itemamount.text = text;
            itemImage.sprite = pic;
        }

    }
}
