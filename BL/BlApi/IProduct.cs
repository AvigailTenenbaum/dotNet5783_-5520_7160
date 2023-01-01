using BO;
namespace BlApi;
/// <summary>
/// Interface for operations on a product
/// </summary>
public interface IProduct
{
    /// <summary>
    /// Method for product list request
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList?> GetListOfProducts(Func<BO.ProductForList?, bool>? func = null);

    public IEnumerable<BO.ProductForList?> GetListOfProductsByCondition(IEnumerable<ProductForList?> productForLists, Func<ProductForList?, bool>? func = null);
    /// <summary>
    /// A method that receives a product ID number and returns product details if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Product GetProductDetails(int id);
    /// <summary>
    /// A method that receives an ID number and shopping basket of a product and returns product item details if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public BO.ProductItem GetProductDetails(int id, BO.Cart cart);
    /// <summary>
    /// A method that receives a product and adds it to the list if the data is correct
    /// </summary>
    /// <param name="product"></param>
    public void AddProduct(BO.Product product);
    /// <summary>
    /// A method that receives a product and deletes it from the list if it is in it and is not found in any order
    /// </summary>
    /// <param name="id"></param>
    public void DeleteProduct(int id);
    /// <summary>
    /// A method that receives a product and updates the product to the received product if the data is correct
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProduct(BO.Product product);
    public IEnumerable<BO.ProductItem?> GetListOfProductsItem();

}

