using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ItemCollectableLives : ItemCollectableBase
    {
        public Collider2D collider;
        protected override void OnCollect()
        {
            base.OnCollect();
           // ItemManager.Instance.AddLife();
            collider.enabled = false;
    }
    }


