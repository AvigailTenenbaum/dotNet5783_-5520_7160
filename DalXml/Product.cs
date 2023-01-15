﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
namespace Dal;
internal class  Product:Iproduct
    {
    static XElement? productRoot;
    string dir = "..\\xml\\";
    string productPath = @"Product.xml";
    public Product()
    {
        
        if (!File.Exists(dir+productPath))
            CreateFiles();
    }
    private void CreateFiles()
    {
        productRoot = new XElement("products");
        productRoot.Save(dir + productPath);
    }
    private void LoadData()
    {
        try { productRoot = XElement.Load(dir + productPath); }
        catch { throw new Exception("file upload problem"); }
    }
    public int AddObject(DO.Product o1)
    {
        XElement id = new XElement("ID", o1.ID);
        XElement name = new XElement("Name", o1.Name);
        XElement Price = new XElement("Price", o1.Price);
        XElement inStock = new XElement("InStock", o1.InStock);
        XElement Category = new XElement("Category", o1.Category);
        productRoot?.Add(new XElement("Product", id, name, Price, inStock, Category));
        productRoot?.Save(dir + productPath);
        return o1.ID;
    }
    /// <summary>
    /// A function that receives an ID number of an object and returns the object if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public DO.Product? GetObject(int id)
    {
        LoadData();
        DO.Product product;
        try 
        {
            product = (from p in productRoot.Elements()
                       where Convert.ToInt32(p.Element("ID")!.Value) == id
                       select new DO.Product()
                       {
                           ID = Convert.ToInt32(p.Element("ID")!.Value),
                           Name = p.Element("Name")!.Value,
                           Price = Convert.ToDouble(p.Element("Price")!.Value),
                           Category = (DO.Category)Enum.Parse(typeof(DO.Category), (string)p.Element("Category")!),
                           InStock = Convert.ToInt32(p.Element("InStock")!.Value)
                       }).FirstOrDefault();
        }
        catch
        {
            throw new DO.NotExist();
        }
        return product;
    }
    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<DO.Product?> GetAllObject(Func<DO.Product?, bool>? func = null)
    {
        LoadData();
        IEnumerable<DO.Product?> products = new List<DO.Product?>();
        if (func == null)
        {
            try
            {
                products = (from p in productRoot?.Elements()
                            select new DO.Product()
                            {
                                ID = Convert.ToInt32(p.Element("ID")!.Value),
                                Name = p.Element("Name")!.Value,
                                Price = Convert.ToDouble(p.Element("Price")!.Value),
                                Category = (DO.Category)Enum.Parse(typeof(DO.Category), (string)p.Element("Category")!),
                                InStock = Convert.ToInt32(p.Element("InStock")!.Value)
                            }).ToList().Cast<DO.Product?>();
            }
            catch { products = null; }
        }
        else
        {
            products = (from p in productRoot?.Elements()
                        let pro = new DO.Product()
                        {
                            ID = Convert.ToInt32(p.Element("ID")!.Value),
                            Name = p.Element("Name")!.Value,
                            Price = Convert.ToDouble(p.Element("Price")!.Value),
                            Category = (DO.Category)Enum.Parse(typeof(DO.Category), (string)p.Element("Category")!),
                            InStock = Convert.ToInt32(p.Element("InStock")!.Value)
                        }
                        where func(pro)
                        select pro).Cast<DO.Product?>();
        }
        return products;
    }
    /// <summary>
    /// A function that receives an ID number of an object and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteObject(int id)
    {
        XElement? productElement;
        try
        {
            productElement = (from p in productRoot?.Elements()
                              where Convert.ToInt32(p.Element("ID")!.Value) == id
                              select p).FirstOrDefault();
            productElement!.Remove();
            productRoot?.Save(dir + productPath);
        }
        catch
        {
            throw new DO.NotExist();
        }
    }
    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(DO.Product pro)
    {
        XElement? productElement;
        try
        {
            productElement = (from p in productRoot?.Elements()
                              where Convert.ToInt32(p.Element("ID")!.Value) ==pro.ID
                              select p).FirstOrDefault();
            productElement!.Element("Name")!.Value = pro.Name!;
            productElement!.Element("Price")!.Value =pro.Price.ToString();
            productElement!.Element("InStock")!.Value = pro.InStock.ToString()!;
            productElement!.Element("Category")!.Value = pro.Category.ToString()!;

            productRoot?.Save( dir + productPath);
        }
        catch
        {
            throw new DO.NotExist();

        }

    }
    /// <summary>
    /// Accepts a condition and returns the first object that meets this condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="CanNotFound"></exception>
    public DO.Product? GetObjectByFilter(Func<DO.Product?, bool>? func)
    {
        LoadData();
        DO.Product? product;
        product = (from p in productRoot?.Elements()
                   let pro = new DO.Product()
                   {
                       ID = Convert.ToInt32(p.Element("ID")!.Value),
                       Name = p.Element("Name")!.Value,
                       Price = Convert.ToDouble(p.Element("Price")!.Value),
                       Category = (DO.Category)Enum.Parse(typeof(DO.Category), (string)p.Element("Category")!),
                       InStock = Convert.ToInt32(p.Element("InStock")!.Value)
                   }
                   where func!(pro)
                   select pro).FirstOrDefault();

        return product;
    }
}
