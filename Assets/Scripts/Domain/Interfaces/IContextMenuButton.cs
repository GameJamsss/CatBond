using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Domain.Items
{
    public abstract class IContextMenuButton : MonoBehaviour
    {
        public abstract int GetId();
        public abstract void SpawnButton(GameObject parent, float x, float y);
    }
}
