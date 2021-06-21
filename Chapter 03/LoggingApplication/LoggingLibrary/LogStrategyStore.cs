using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;

namespace LogLibrary
{
    public static class CloneHelpers
    {
        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }
    }

    public class ObjectFactory
    {
        //The Dictionary which maps XML configuration Keys (key) to TypeName(value)
        private Dictionary<string, string> entries = new Dictionary<string, string>();

        //The Dictionary which maps Entry keys to Objects already instantiated by the Container
        private Dictionary<string, object> objects = new Dictionary<string, object>();

        private Dictionary<string, string> LoadData(string str)
        {
            //We use LINQ lambda syntax to load the contents of the XML file
            return XDocument.Load(str).Descendants("entries").
                Descendants("entry").ToDictionary(p =>
                p.Attribute("key").Value,
                p => p.Attribute("value").Value);
        }

        public ObjectFactory(string str)
        {
            entries = LoadData(str);
        }

        public object Get(string key, string mode = "singleton")
        {
            //singleton will return the same object every time
            //prototype will create a clone of the object if already instantiated before
            //singleton and prototype are the permissible parameters
            if (mode != "singleton" && mode != "prototype")
            {
                return null;
            }

            object temp = null;

            if (objects.TryGetValue(key, out temp))
            {
                return (mode == "singleton") ? temp : temp.DeepClone<object>();
            }

            //If we could not retrieve an instance of previously created object, retrieve the typename from entries map
            string classname = null;
            entries.TryGetValue(key, out classname);

            if (classname == null)
            {
                return null;
            }

            string fullpackage = classname;

            //Use .Net reflection API to retrieve the CLR type of the class name
            Type t = Type.GetType(fullpackage);

            if (t == null)
            {
                return null;
            }

            //Instantiate the object using .Net reflection API
            objects[key] = Activator.CreateInstance(t);

            return objects[key];
        }
    }
}
