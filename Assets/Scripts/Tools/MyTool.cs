using System;
using System.ComponentModel;
using System.Reflection;

/// <summary>
/// Tool class for native classes.
/// <summary>
public class MyTool
{
    public static string GetEnumDescription(Enum enumValue)
    {
        string value = enumValue.ToString();
        FieldInfo field = enumValue.GetType().GetField(value);
        object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (objs == null || objs.Length == 0)
            return value;
        DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
        return descriptionAttribute.Description;
    }
}
