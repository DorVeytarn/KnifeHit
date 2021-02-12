using UnityEngine;
using System.Collections.Generic;
using System;
using Object = UnityEngine.Object;

namespace Utils.Singletone
{
    /*
     * Класс, позволяющий не пробрасывать в сцене ссылки на кучу компонентов типа
     */
    public class SceneComponentProvider : Singleton<SceneComponentProvider>
    {
        private Dictionary<Type, Object> objectCache;
        private bool isDestroying = false;

        public override void OnDestroy()
        {
            if (objectCache != null) 
                objectCache.Clear();

            isDestroying = true;
            base.OnDestroy();
        }

        /// <summary>
        /// Следует всегда вызывать этот метод перед загрузкой новой сцены
        /// Дело в том, что может быть такой кейс: пусть есть A, B, глобальные компоненты,
        /// зарегистрированные здесь, причем B в OnDestroy обращается к A
        /// Сцена выгружается, удаляется A, потом B в своем дестрое просит A и мы
        /// создаем новое A, которое уже не удаляется(!)
        /// При этом наш OnDestroy может быть вызван сильно позже, потому приходится 
        /// обходиться таким костылём
        /// </summary>
        public static void NotifyOnSceneUnloading()
        {
            Instance.isDestroying = true;
        }

        public static void RegisterComponent(Type t, Object o)
        {
            // сцена закрывается, не выдаём ничего
            if (Instance.isDestroying) 
                return;

            if (Instance.objectCache == null)
                Instance.objectCache = new Dictionary<Type, Object>();

            Instance.objectCache[t] = o;
        }

        public new static Object GetComponent(Type t)
        {
            // сцена закрывается, не выдаём ничего
            if (Instance.isDestroying) 
                return null;

            if (Instance.objectCache == null) 
                Instance.objectCache = new Dictionary<Type, Object>();

            // проверяем непосредственно тип в кэше
            if (Instance.objectCache.ContainsKey(t))
            {
                if (Instance.objectCache[t].Equals(null))
                    Instance.objectCache.Clear();
                else
                    return Instance.objectCache[t];
            }

            // проевряем, вдруг в кэше есть объект более конкретного типа
            foreach (var ct in Instance.objectCache.Keys)
            {
                if (t.IsAssignableFrom(ct))
                {
                    if (Instance.objectCache[ct].Equals(null))
                    {
                        Instance.objectCache.Clear();
                        break;
                    }
                    else
                        return Instance.objectCache[ct];
                }
            }
            var objects = FindObjectsOfType(t);

            if (objects.Length > 1)
            {
                throw new Exception("SceneComponentProvider unable to provide " + t.ToString() 
                                    + ": we have " + objects.Length.ToString() + " objects");
            }

            if (objects.Length == 0) 
                return null;

            Instance.objectCache[t] = objects[0];
            return objects[0];
        }
    }
}