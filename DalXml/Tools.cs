using System.Xml.Linq;
using System.Xml.Serialization;
namespace Dal;

internal static class Tools<T>
{
    /// <summary>
    /// Saving a generic list in an Excel file
    /// </summary>
    /// <param name="l"></param>
    /// <param name="path"></param>
    public static void SaveListToXml(List<T?> l, string path)
    {
        XmlSerializer xml = new XmlSerializer(l.GetType());
        string dir = "..\\xml\\";
        FileStream file = new FileStream(dir + path, FileMode.Create);
        xml.Serialize(file, l);
        file.Close();
    }
    /// <summary>
    /// Loading an Excel file into a generic list
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<T?> LoadListFromXml(string path)
    {
        List<T?> l;
        XmlSerializer xml = new XmlSerializer(typeof(List<T?>));
        string dir = "..\\xml\\";
        FileStream file = new FileStream(dir + path, FileMode.Open);
        l = (List<T?>)xml.Deserialize(file);
        file.Close();
        return l.ToList<T?>();
    }
    /// <summary>
    /// A running variable for an order number
    /// </summary>
    /// <returns></returns>
    public static int GetLastOrderID()
    {
        string dir = "..\\xml\\";
        string ConfigPath = @"Config.xml";
        XElement root = XElement.Load(dir + ConfigPath);
        int id = Convert.ToInt32(root.Element("IDorder")!.Value);
        id++;
        root.Element("IDorder")!.SetValue(id);
        root.Save(dir + ConfigPath);
        return id;
    }
    /// <summary>
    /// A running variable for an order item number
    /// </summary>
    /// <returns></returns>
    public static int GetLastOrderItemID()
    {
        string dir = "..\\xml\\";
        string ConfigPath = @"Config.xml";
        XElement root = XElement.Load(dir + ConfigPath);
        int id = Convert.ToInt32(root.Element("IDorderItem")!.Value);
        id++;
        root.Element("IDorderItem")!.SetValue(id);
        root.Save(dir + ConfigPath);
        return id;
    }
}
