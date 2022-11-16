using Assets.Scripts.Domain.Objects;
using CSharpFunctionalExtensions;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class InGameButtonUtils
    {
        public static Result<ClickableObject> GetClickableObject(GameObject go, int id)
        {
            return Result
                .Try(go.GetComponent<ClickableObject>)
                .Ensure(co => co != null,
                    "context menu button id: " + id +
                    " have no clickable object as a parent. The parent is " + go.name);
        }
    }
}
