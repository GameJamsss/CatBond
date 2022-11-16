using System;
using CSharpFunctionalExtensions;

namespace Assets.Scripts.Utils
{
    public static class MaybeRich
    {
        public static Maybe<T> NullSafe<T>(T value)
        {
            return value != null ? Maybe<T>.From(value) : Maybe<T>.None;
        }

        public static Maybe<T> Tap<T>(this Maybe<T> value, Action<T> action)
        {
            value.Match(action, () => { });
            return value;
        }
    }
}
