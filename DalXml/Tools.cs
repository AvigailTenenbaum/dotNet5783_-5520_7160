using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
namespace Dal;

internal static class Tools<T>
{
    /// <summary>
    /// Saving a generic list in an Excel file
    /// </summary>
    /// <param name="l"></param>
    /// <param name="path"></param>
    public static void SaveListToXml( List<T?>l, string path)
    {
       XmlSerializer xml= new XmlSerializer(l.GetType());
        string dir = "..\\xml\\";
        FileStream file = new FileStream(dir + path,FileMode.Create);
        xml.Serialize(file, l);
    }
    /// <summary>
    /// Loading an Excel file into a generic list
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<T?> LoadListFromXml( string path ) 
    {
        List<T?> l;
        XmlSerializer xml= new XmlSerializer(typeof(List<T?>));
        string dir = "..\\xml\\";
        FileStream file = new FileStream(dir + path, FileMode.Open);
        l =(List<T?>)xml.Deserialize(file);
        return l.ToList<T?>();
    }
}
