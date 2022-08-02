using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;

namespace Unboxed.Player
{
    public class DotPlayer : MonoBehaviour, IShowable
    {
        //private Player _player;

        //private void Update()
        //{
        //    //ResetPosition();
        //}

        //public void BindPlayer(Player player)
        //{
        //    _player = player;
        //}

        //public void ResetPosition()
        //{
        //    //var mousePos = UnboxedUtility.GetMousePosition(_player.InteractPosition);
        //    //transform.position = new Vector3(mousePos.x, mousePos.y, -1);
        //}

        public void UpdatePosition(Vector3 position)
        {
            transform.position = new Vector3(position.x, position.y, -1);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

