using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace WeaponSystem.Utility
{
    using Object = UnityEngine.Object;

    public class WeaponSystemUtility
    {
        public static FieldInfo[] GetAllFieldsWithAttribute(Type inType, Type attributeType)
        {
            return inType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(
                field => field.GetCustomAttributes(false).Any(attribute => attribute.GetType() == attributeType)).ToArray();
        }

        public static void FillWeaponLogicList(IWeapon weapon)
        {
            var logicObjectsList = GetAllFieldsWithAttribute(weapon.GetType(), typeof(LogicObjectsAttribute));
            if(logicObjectsList.Length > 0)
            {
                var objectList = logicObjectsList[0].GetValue(weapon) as IList;
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
            else
                Debug.Log("Provided IWeapon don't have field type List<Object> marked with LogicObjects attribute!");
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
                    Debug.LogFormat("Object type {0} added to list of type {1}", objet.GetType().Name, list.GetType().GetGenericArguments()[0].Name);
                    break;
                }
            }
            return list;
        }

        public static void GetPartTypes(object instance, Transform transform, Dictionary<Type, Object> dictionary, IList list)
        {
            List<FieldInfo> parts = new List<FieldInfo>();
            parts.AddRange(GetAllFieldsWithAttribute(instance.GetType(), typeof(WeaponPartAttribute)));

            for (int i = 0; i < parts.Count; i++)
            {
                Object @object = null;
                if (dictionary.TryGetValue(parts[i].FieldType, out @object))
                {
                    parts[i].SetValue(instance, @object);
                }
                else
                {
                    @object = AddPart(parts[i].FieldType, transform, list);
                    parts[i].SetValue(instance, @object);
                    dictionary.Add(parts[i].FieldType, @object);
                    GetPartTypes(@object, transform, dictionary, list);
                }
            }
        }

        public static Object AddPart(Type partType, Transform transform, IList list)
        {
            GameObject gameObject = new GameObject();

            gameObject.transform.SetParent(transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;

            gameObject.name = partType.Name;

            var component = gameObject.AddComponent(partType);

            list.Add(component);

            return component;
        }

        public static void FillRequirements(IWeapon weapon, GameObject gameObject)
        {
            var logicObjectsList = GetAllFieldsWithAttribute(weapon.GetType(), typeof(LogicObjectsAttribute));
            var list = logicObjectsList[0].GetValue(weapon) as IList;

            for (int i = 0; i < list.Count; i++)
            {
                var fields = GetAllFieldsWithAttribute(list[i].GetType(), typeof(WeaponRequireComponentAttribute));
                for (int j = 0; j < fields.Length; j++)
                {
                    var attributes = fields[j].GetCustomAttributes(false);
                    for (int k = 0; k < attributes.Length; k++)
                    {
                        if (attributes[k] is WeaponRequireComponentAttribute)
                        {
                            var requirement = attributes[k] as WeaponRequireComponentAttribute;
                            if (requirement.Type != null)
                            {
                                var component = GetComponet(gameObject, requirement.Type);
                                if (component == null)
                                    ComponentIsMissing(gameObject, fields[j].FieldType);
                                var @object = fields[j].GetValue(list[i]);
                                var objectFields = @object.GetType().GetFields();
                                for (int l = 0; l < objectFields.Length; l++)
                                {
                                    if (objectFields[l].FieldType == requirement.Type)
                                    {
                                        objectFields[l].SetValue(@object, component);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                var component = GetComponet(gameObject, fields[j].FieldType);
                                if (component == null)
                                    ComponentIsMissing(gameObject, fields[j].FieldType);
                                fields[j].SetValue(list[i], component);
                            }
                        }
                    }
                }
            }
        }

        private static void ComponentIsMissing(GameObject gameObject, Type type)
        {
            Debug.LogErrorFormat("Game object {0} require component type of {1} and that component is missing", gameObject.name, type.Name);
        }

        private static object GetComponet(GameObject gameObject, Type type)
        {
            var component = gameObject.GetComponent(type);
            if (component == null)
                component = gameObject.GetComponentInChildren(type);

            return component;
        }
    }
}