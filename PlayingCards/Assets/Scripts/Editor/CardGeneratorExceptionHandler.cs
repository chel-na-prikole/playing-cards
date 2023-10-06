using System;
using System.Collections.Generic;
using UnityEngine;
using Views;
using Object = UnityEngine.Object;

namespace Editor
{
    public static class CardGeneratorExceptionHandler
    {
        public static void CheckFolderPath(CardGenerator cardGenerator)
        {
            if (cardGenerator.TargetFolder == null)
            {
                throw new Exception($"[{nameof(CardGenerator)}] {nameof(CardGenerator.TargetFolder)} is null!");
            }
        }
    
        public static void CheckObjectNull<T>(T obj) where T : Object
        {
            if (obj == null)
            {
                ThrowSomethingWrongException();
            }
        }

        public static void CheckCollectionsCountEquality<TFirst, TSecond>(IReadOnlyCollection<TFirst> first, IReadOnlyCollection<TSecond> second)
        {
            if (first.Count != second.Count)
            {
                ThrowSomethingWrongException();
            }
        }

        private static void ThrowSomethingWrongException() => throw new Exception("Something went wrong with prefabs. Please regenerate them");
    }
}