namespace BlApi;
/// <summary>
/// An interface for performing operations on a shopping basket
/// </summary>
public interface ICart
{
    /// <summary>
    /// A method for adding a product to the shopping cart, receives a shopping cart and a product ID and returns a checked shopping cart if everything is correct
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Cart AddProductToCart(BO.Cart cart, int id);
    /// <summary>
    /// A method for updating the quantity of a product in the basket, receives a shopping basket, identifies a new product and quantity and returns an updated basket if everything is correct
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public BO.Cart UpdateProductAmount(BO.Cart cart, int id, int amount);
    /// <summary>
    /// Method for placing the order, receives a shopping basket and adds the order if everything is in order
    /// </summary>
    /// <param name="cart"></param>
    public void OrderConfirmation(BO.Cart cart);
}


