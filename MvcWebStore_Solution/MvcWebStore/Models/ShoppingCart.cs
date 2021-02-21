using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWebStore.Models
{
    public class ShoppingCart
    {
        WebStoreEntities storeDB = new WebStoreEntities();
        private string ShoppingCartId { get; set; }

        public const string CartSesionKey = "CartId";

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSesionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSesionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSesionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSesionKey].ToString();
        }

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product product)
        {
            var cartItem = storeDB.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId && c.ProductID== product.ProductID);
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ProductID= product.ProductID,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                storeDB.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            storeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = storeDB.Carts.Single(cart => cart.CartId == ShoppingCartId && cart.RecordId == id);
            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }
                storeDB.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(cart => cart.CartId == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }

            storeDB.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            //LINQ (Language Integrated Query)
            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Product.UnitPrice).Sum();

            return total ?? decimal.Zero;

        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductID= item.ProductID,
                    OrderId = order.OrderId,
                    UnitPrice = item.Product.UnitPrice,
                    Quantity = item.Count
                };
                orderTotal += (item.Count * item.Product.UnitPrice);

                storeDB.OrderDetails.Add(orderDetail);
            }

            order.Total = orderTotal;
            storeDB.SaveChanges();
            EmptyCart();
            return order.OrderId;
        }

        public void MigrateCart(string username)
        {
            var shoppingCart = storeDB.Carts.Where(c => c.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = username;
            }
            storeDB.SaveChanges();
        }
    }
}