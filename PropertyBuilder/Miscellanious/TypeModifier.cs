using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static System.String;

namespace PropertyBuilder.Miscellanious
{
    public static class TypeModifier
    {
        public static void ApplyType(this ObservableCollection<string> typeList, string newType)
        {
            for (int i = 0; i < typeList.Count; i++)
            {
                if (IsNullOrWhiteSpace(typeList[i]))
                    continue;

                var currentType = new StringBuilder(typeList[i]);

                var typeStartsFrom = typeList[i].IndexOf("<", StringComparison.Ordinal) + 1;

                var typeEndsAt = typeList[i].IndexOf(">", StringComparison.Ordinal) - 1;

                var typeLength = typeStartsFrom == typeEndsAt ? 1 : typeEndsAt - typeStartsFrom;

                if (typeLength == 1)
                    currentType.Remove(typeStartsFrom, 1);
                else
                {
                    currentType.Remove(typeStartsFrom, typeLength + 1);
                }

                currentType.Insert(typeStartsFrom,
                    typeList[i].StartsWith("Dictionary") ? $"{newType}, {newType}" : newType);

                typeList[i] = currentType.ToString();
            }
        }
    }
}
