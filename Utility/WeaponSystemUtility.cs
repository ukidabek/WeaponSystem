using System;
using System.Linq;
using System.Reflection;
using System.Collections;

namespace WeaponSystem.Utility
{
    public class WeaponSystemUtility
    {
        public static FieldInfo[] GetAllFieldsWithAttribute(Type inType, Type attributeType)
        {
            return inType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(
                field => field.GetCustomAttributes(false).Any(attribute => attribute.GetType() == attributeType)).ToArray();
        }

        public static void FillWeaponLogicList(IWeapon weapon)
        {
            var objectList = GetAllFieldsWithAttribute(weapon.GetType(), typeof(LogicObjectsAttribute))[0].GetValue(weapon) as IList;
            var fieldInfo = GetAllFieldsWithAttribute(weapon.GetType(), typeof(LogicListAttribute));

            for (int i = 0; i < objectList.Count; i++)
            {
                for (int j = 0; j < fieldInfo.Length; j++)
                {
                    var listType = fieldInfo[j].FieldType;
                    var listObject = fieldInfo[j].GetValue(weapon);
                    var list = (listObject as IList);
                    Add(list, objectList[i], listType.GetGenericArguments()[0]);
                    fieldInfo[j].SetValue(weapon, list);
                }
            }
        }

        protected static object Add(IList list, object objet, Type type)
        {
            var objectType = objet.GetType();
            var interfaces = objectType.GetInterfaces();

            for (int i = 0; i < interfaces.Length; i++)
            {
                if (interfaces[i] == type)
                {
                    list.Add(objet);
                    break;
                }
            }
            return list;
        }
    }
}