using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Domain.Objects;
using CSharpFunctionalExtensions;
using UnityEngine;

namespace Assets.Scripts.Domain.Items
{
    public interface IContextMenuButton
    {
        public int GetId();
        public void SpawnButton(GameObject parent, float x, float y);
    }
}
