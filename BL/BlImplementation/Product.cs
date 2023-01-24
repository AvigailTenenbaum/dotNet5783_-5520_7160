using BO;
using System.Runtime.CompilerServices;

namespace BlImplementation;
/// <summary>
/// method for product list request
/// </summary>
internal class Product : BlApi.IProduct
{
    private DalApi.IDal? dal = DalApi.Factory.Get();
    public IEnumerable<BO.ProductForList?> GetListOfProducts(Func<BO.ProductForList?, bool>? func = null)
    {
        //var v = dal.Product.GetAllObject();
        IEnumerable<BO.ProductForList?> productList = dal?.Product.GetAllObject().Select(
            product => new BO.ProductForList
            {
                ID = product?.ID ?? throw new BO.NullData(),
                Name = product?.Name ?? throw new BO.NullData(),
                Category = (BO.Category?)product?.Category ?? throw new BO.NullData(),
                Price = product?.Price ?? throw new BO.NullData()
            }
        ) ?? throw new BO.NullData();
        if (func != null)
        {
            return productList.Where(product => func(product));
        }
        return productList;

    }
    /// <summary>
    /// A method that receives a product ID number and returns product details if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidId"></exception>
    public BO.Product GetProductDetails(int id)
    {
        if (id > 0)
        {

            try
            {
                DO.Product? product = dal?.Product.GetObject(id);
                BO.Product newProduct = new BO.Product
                {
                    ID = product?.ID ?? throw new NullData(),
                    Name = product?.Name ?? throw new NullData(),
                    Category = (BO.Category?)product?.Category,
                    Price = product?.Price ?? throw new NullData(),
                    InStock = product?.InStock ?? 0
                };
                return newProduct;
            }
            catch (DO.NotExist e) { throw new BO.NotExist(e); }

        }
        else
            throw new BO.InCorrectData();
    }
    /// <summary>
    /// A method that receives an ID number and shopping basket of a product and returns product item details if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public BO.ProductItem GetProductDetails(int id, BO.Cart cart)
    {
        if (id > 0)
        {
            DO.Product? product;

            try
            {
                product = dal!.Product.GetObject(id);
            }
            catch (DO.NotExist ex)//product doesnt exist
            {
                throw new BO.NotExist(ex);
            }
            /// return the productItem 
            return new BO.ProductItem()///builiding a productItem
            {
                ID = product?.ID ?? throw new BO.NullData(),
                Name = product?.Name ?? throw new BO.NullData(),
                InStock = product?.InStock > 0 ? true : false,
                Category = (BO.Category)(product?.Category ?? throw new BO.NullData()),
                Price = product?.Price ?? throw new BO.NullData(),
                AmountInCart = cart.Items is null ? 0 : cart.Items.Where(x => x?.ProductID == id).Sum(x => x!.Amount)

            };


        }
        else
            throw new BO.InCorrectData();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A method that receives a product and adds it to the list if the data is correct
    /// </summary>
    /// <param name="product"></param>
    public void AddProduct(BO.Product product)
    {
        if (product.ID < 100000 || product.ID > 999999 || product.Name == null || product.Price <= 0||product.InStock<0)
        {
            throw new BO.InCorrectData();
        }
        else
        {
            DO.Product p = new DO.Product() { Category = (DO.Category?)product.Category, ID = product.ID, Name = product.Name, Price = product.Price, InStock = product.InStock };
            try
            {
                dal?.Product.AddObject(p);
            }
            catch (DO.AllReadyExist e) { throw new BO.AllReadyExist(e); }
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]

    /// A method that receives a product and deletes it from the list if it is in it and is not found in any order
    /// </summary>
    /// <param name="id"></param>
    public void DeleteProduct(int id)
    {

        IEnumerable<DO.OrderItem?> orderI;
        try { orderI = dal?.OrderItem.GetAllObject() ?? new List<DO.OrderItem?>(); }//gets the al orderItems
        catch (DO.NotExist ex)
        {
            throw new NotExist(ex);
        }
        DO.OrderItem? exist = orderI.FirstOrDefault(oi => oi?.ProductID == id); //check if the product to delete is exist
        if (exist != null)// if the product is exist
        {
            throw new BO.NotPossibleToFillRequest();
        }
        try { dal?.Product.DeleteObject(id); } catch (DO.NotExist ex) { throw new BO.NullData(); }//delete the product

    }

    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A method that receives a product and updates the product to the received product if the data is correct
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProduct(BO.Product product)
    {
        if (product.ID <= 0 || product.Name == null || product.Price <= 0 || product.InStock < 0)
        {
            throw new BO.InCorrectData();
        }
        else
        {
            DO.Product p = new DO.Product() { Category = (DO.Category?)product.Category, ID = product.ID, Name = product.Name, Price = product.Price, InStock = product.InStock };
            try
            {
                dal?.Product.UpDateObject(p);
            }


            catch (DO.NotExist e) { throw e; }
        }
    }
    public IEnumerable<BO.ProductItem?> GetListOfProductsItem()
    {
        return from DO.Product? product1 in dal!.Product.GetAllObject()
               select new BO.ProductItem
               {
                   ID = product1?.ID ?? throw new NullData(),
                   Name = product1?.Name ?? throw new NullData(),
                   Category = (BO.Category)(product1?.Category ?? throw new NullData()),
                   Price = product1?.Price ?? throw new NullData(),
                   InStock = product1?.InStock > 0 ? true : false,
                   AmountInCart = 0
               };
    }

    public IEnumerable<ProductForList?> GetListOfProductsByCondition(IEnumerable<ProductForList?> productForLists, Func<ProductForList?, bool>? func)
    => productForLists.Where(func!);
}

