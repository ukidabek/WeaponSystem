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
                var objectList = (logicObjectsList[0].GetValue(weapon) as IList);
                object[] objectArray = new object[objectList.Count];
                for (int i = 0; i < objectList.Count; i++)
                    objectArray[i] = objectList[i];
                FillWeaponLogicList(weapon, logicObjectsList, objectArray);
            }
            else
                Debug.Log("Provided IWeapon don't have field type List<Object> marked with LogicObjects attribute!");
        }

        public static void FillWeaponLogicList(IWeapon weapon, FieldInfo[] fieldInfo, params object[] logicObjects)
        {
            if (logicObjects.Length > 0)
            {
                for (int i = 0; i < logicObjects.Length; i++)
                {
                    for (int j = 0; j < fieldInfo.Length; j++)
                    {
                        var listType = fieldInfo[j].FieldType;
                        var listObject = fieldInfo[j].GetValue(weapon);
                        var list = (listObject as IList);
                        Add(list, logicObjects[i], listType.GetGenericArguments()[0]);
                        fieldInfo[j].SetValue(weapon, list);
                    }
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

        public static void FillRequirements<T>(IWeapon weapon, object valueToSet = null) where T : InitializationAttribute
        {
            var logicObjectsList = GetAllFieldsWithAttribute(weapon.GetType(), typeof(LogicObjectsAttribute));
            var list = logicObjectsList[0].GetValue(weapon) as IList;

            for (int i = 0; i < list.Count; i++)
            {
                var fields = GetAllFieldsWithAttribute(list[i].GetType(), typeof(T));
                FillRequirements<T>(weapon, list[i], fields, valueToSet);
            }
        }

        public static void FillRequirements<T>(IWeapon weapon, object instance, FieldInfo[] fields, object valueToSet = null) where T : InitializationAttribute
        {
            for (int j = 0; j < fields.Length; j++)
            {
                var attributes = fields[j].GetCustomAttributes(false);
                for (int k = 0; k < attributes.Length; k++)
                {
                    if (attributes[k] is T)
                    {
                        var requirement = attributes[k] as T;
                        if (requirement.Type != null)
                        {
                            if(valueToSet == null)
                            {
                                valueToSet = GetComponet(weapon.GameObject, requirement.Type);
                                if (valueToSet == null)
                                    ComponentIsMissing(weapon.GameObject, fields[j].FieldType);
                            }

                            var @object = fields[j].GetValue(instance);
                            var objectFields = @object.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                            for (int l = 0; l < objectFields.Length; l++)
                            {
                                if (objectFields[l].FieldType == valueToSet.GetType())
                                {
                                    objectFields[l].SetValue(@object, valueToSet);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if(valueToSet == null)
                            {
                                valueToSet = GetComponet(weapon.GameObject, fields[j].FieldType);
                                if (valueToSet == null)
                                    ComponentIsMissing(weapon.GameObject, fields[j].FieldType);
                            }
                            if(fields[j].FieldType == valueToSet.GetType())
                                fields[j].SetValue(instance, valueToSet);
                        }
                    }
                }
            }
        }

        public static void FillListOfFields(object owener, FieldInfo[] fields, params object[] objectToSet)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                var attribute = fields[i].GetCustomAttributes(false).FirstOrDefault(a=> a.GetType() == typeof(InitializeWeaponComponentAttribute)) as InitializeWeaponComponentAttribute;

                FieldInfo field = null;
                object @object = null;
                Type type = null;

                if(attribute.Type == null)
                {
                    @object = owener;
                    type = fields[i].FieldType;
                    field = fields[i];
                }
                else
                {
                    @object = fields[i].GetValue(owener);
                    type = attribute.Type;
                    field = @object.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(f => f.FieldType == attribute.Type);
                }

                for (int j = 0; j < objectToSet.Length; j++)
                {
                    if (type == objectToSet[j].GetType())
                    {
                        field.SetValue(@object, objectToSet[j]);
                        break;
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