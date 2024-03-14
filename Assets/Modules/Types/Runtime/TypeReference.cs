
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Scaffold.Types
{
    [Serializable]
    public class TypeReference
    {
        public TypeReference()
        {

        }

        public TypeReference(Type type)
        {
            Set(type);
        }

        private Type type;
        [SerializeField] private string serializedType;

        public Type Type
        {
            get
            {
                if (type == null)
                {
                    type = JsonConvert.DeserializeObject<Type>(serializedType, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });
                }
                return type;
            }
        }

        public void Set<T>()
        {
            Set(typeof(T));
        }

        public void Set(Type type)
        {
            this.type = type;
            serializedType = JsonConvert.SerializeObject(type, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
            });
        }
    }
}
