using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Logic
{
    public class ReflectionLogic
    {
        public List<T> GetAllSubClassesOf<T> (Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            List<T> returnList = new List<T>();
            foreach(Type type in types)
            {
                if (type.IsSubclassOf(typeof(T)))
                {
                    if(!type.IsAbstract)
                    {
                        T instance = (T)Activator.CreateInstance(type);
                        returnList.Add(instance);
                    }
                }
            }
            return returnList;
        }
    }
}
