using Models.Dao;
using Models.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        public Order order = new Order();
        public ShoppingCartModel model = new ShoppingCartModel();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BillingAddress()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));
                if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult BillingAddress(AddressModel addressModel, bool shipToSameAddress)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                if (ModelState.IsValid)
                {
                    var address = new Address
                    {
                        FirstName = addressModel.FirstName,
                        LastName = addressModel.LastName,
                        Email = addressModel.Email,
                        Company = addressModel.Company,
                        City = addressModel.City,
                        Address1 = addressModel.Address1,
                        Address2 = addressModel.Address2,
                        ZipPostalCode = addressModel.ZipPostalCode,
                        PhoneNumber = addressModel.PhoneNumber,
                        FaxNumber = addressModel.FaxNumber,
                        CreatedOnUtc = DateTime.UtcNow
                    };

                    var customerDao = new CustomerDao();
                    var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                    var customer = customerDao.GetCustomerById(Convert.ToInt32(customerSession.UserId));

                    customer.Address = address;
                    customer.Addresses.Add(address);
                    customerDao.UpdateCustomer(customer);
                    customer = customerDao.GetCustomerById(customer.Id);
                    if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() > 0)
                    {
                        order = new Order();
                        order.OrderGuid = Guid.NewGuid();
                        order.CustomerId = customer.Id;
                        order.BillingAddressId = address.Id;
                        order.ShippingAddressId = shipToSameAddress ? address.Id : 0;
                        foreach (var item in customer.ShoppingCartItems)
                        {
                            if (item.ShoppingCartTypeId == 1)
                            {
                                var orderItem = new OrderItem();
                                orderItem.OrderItemGuid = Guid.NewGuid();
                                orderItem.ProductId = item.ProductId;
                                orderItem.Quantity = item.Quantity;
                                orderItem.UnitPriceInclTax = item.Product.Price;
                                orderItem.UnitPriceExclTax = item.Product.Price;
                                orderItem.PriceInclTax = item.Product.Price;
                                orderItem.PriceExclTax = item.Product.Price;
                                orderItem.DiscountAmountInclTax = 0;
                                orderItem.DiscountAmountExclTax = 0;
                                orderItem.OriginalProductCost = item.Product.ProductCost;
                                orderItem.ItemWeight = item.Product.Weight;
                                orderItem.RentalStartDateUtc = item.RentalStartDateUtc;
                                orderItem.RentalEndDateUtc = item.RentalEndDateUtc;
                                order.OrderItems.Add(orderItem);
                            }
                        }
                        customer.Orders.Add(order);
                        TempData["Customer"] = customer;
                        return RedirectToAction("ShippingMethod", "CheckOut");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Cart");
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public JsonResult SelectBillingAddress(int addressId, bool shipToSameAddress)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var customerDao = new CustomerDao();
                var addressDao = new AddressDao();
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = customerDao.GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var address = addressDao.GetAddressById(addressId);

                if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() > 0)
                {
                    Order order = new Order()
                    {
                        OrderGuid = Guid.NewGuid(),
                        CustomerId = customer.Id,
                        BillingAddressId = address.Id,
                        ShippingAddressId = shipToSameAddress ? address.Id : 0,
                    };

                    foreach (var item in customer.ShoppingCartItems)
                    {
                        if (item.ShoppingCartTypeId == 1)
                        {
                            OrderItem orderItem = new OrderItem()
                            {
                                OrderItemGuid = Guid.NewGuid(),
                                ProductId = item.ProductId,
                                Quantity = item.Quantity,
                                UnitPriceInclTax = item.Product.Price,
                                UnitPriceExclTax = item.Product.Price,
                                PriceInclTax = item.Product.Price,
                                PriceExclTax = item.Product.Price,
                                DiscountAmountInclTax = 0,
                                DiscountAmountExclTax = 0,
                                OriginalProductCost = item.Product.ProductCost,
                                ItemWeight = item.Product.Weight,
                                RentalStartDateUtc = item.RentalStartDateUtc,
                                RentalEndDateUtc = item.RentalEndDateUtc
                            };
                            order.OrderItems.Add(orderItem);
                        }
                    }
                    customer.Orders.Add(order);
                    TempData["Customer"] = customer;
                    return Json(new
                    {
                        Status = true
                    });
                    //return PartialView("ShippingMethod", customer);
                    //return RedirectToAction("ShippingMethod", "CheckOut");
                }
                else
                {
                    return Json(new
                    {
                        Status = false
                    });
                }
            }
            else
            {
                return Json(new
                {
                    Status = false
                });
            }
        }

        [HttpGet]
        public ActionResult ShippingMethod()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                Customer customer = (Customer)TempData["Customer"];
                if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
                return View(customer);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public ActionResult ShippingMethod(string shippingOption, string nextStep)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                Customer customer = (Customer)TempData["Customer"];
                if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
                foreach (var item in customer.Orders)
                {
                    if (item.Id == 0)
                    {
                        item.ShippingMethod = shippingOption;
                    }
                }
                if (nextStep == "Next")
                {
                    TempData["Customer"] = customer;
                    return RedirectToAction("PaymentMethod", "CheckOut");
                }
                return View(customer);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpGet]
        public ActionResult PaymentMethod()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                Customer customer = (Customer)TempData["Customer"];

                if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
                return View(customer);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public ActionResult PaymentMethod(string paymentMethod, string nextStep)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                Customer customer = (Customer)TempData["Customer"];
                if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
                foreach (var item in customer.Orders)
                {
                    if (item.Id == 0)
                    {
                        item.PaymentMethodSystemName = paymentMethod;
                    }
                }
                if (nextStep == "Next")
                {
                    TempData["Customer"] = customer;
                    return RedirectToAction("PaymentInfo", "CheckOut");
                }
                return View(customer);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpGet]
        public ActionResult PaymentInfo()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                Customer customer = (Customer)TempData["Customer"];
                if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
                TempData["Customer"] = customer;
                return View(customer);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public ActionResult PaymentInfo(string nextStep)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                Customer customer = (Customer)TempData["Customer"];
                if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
                if (nextStep == "Next")
                {
                    TempData["Customer"] = customer;
                    return RedirectToAction("Confirm", "CheckOut");
                }
                return View(customer);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpGet]
        public ActionResult Confirm()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                Customer customer = (Customer)TempData["Customer"];

                if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
                return View(customer);
            }
            else
            {
                return Redirect("/");
            }
        }
        public static string PublicIPAddress()
        {
            string uri = "http://checkip.dyndns.org/";
            string ip = String.Empty;

            using (var client = new HttpClient())
            {
                var result = client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;

                ip = result.Split(':')[1].Split('<')[0];
            }

            return ip;
        }
        String OrderNumer;
        int OrderId;
        //Confirm
        [HttpPost]
        public ActionResult Confirm(string nextStep)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                Customer customer = (Customer)TempData["Customer"];
                if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
                if (nextStep == "Confirm")
                {
                    Decimal Total = 0;
                    var customerDao = new CustomerDao();
                    var orderDao = new OrderDao();
                    var shoppingCartItemDao = new ShoppingCartItemDao();
                    foreach (var item in customer.Orders)
                    {
                        if (item.Id == 0)
                        {
                            foreach (var ite in item.OrderItems)
                            {
                                Total += Convert.ToDecimal(ite.Quantity) * ite.UnitPriceInclTax;
                            }
                            item.OrderStatusId = (int)Orders.Pending;
                            item.ShippingStatusId = (int)Shipping.Delivered;
                            item.PaymentStatusId = (int)Payment.Pending;
                            item.CustomerCurrencyCode = "VND";
                            item.CurrencyRate = 1;
                            item.CustomerTaxDisplayTypeId = 1;
                            item.OrderSubtotalInclTax = Total;
                            item.OrderSubtotalExclTax = Total;
                            item.OrderTotal = Total;
                            item.OrderSubTotalDiscountInclTax = 0;
                            item.OrderSubTotalDiscountExclTax = 0;
                            item.OrderShippingInclTax = 0;
                            item.OrderShippingExclTax = 0;
                            item.PaymentMethodAdditionalFeeInclTax = 0;
                            item.PaymentMethodAdditionalFeeExclTax = 0;
                            item.TaxRates = "0";
                            item.OrderTax = 0;
                            item.OrderDiscount = 0;
                            item.CustomerLanguageId = 1;
                            item.CustomerIp = PublicIPAddress();
                            item.ShippingRateComputationMethodSystemName = "";
                            OrderNumer = item.CustomOrderNumber = orderDao.GetMaxOrderNumber();
                            item.Deleted = false;
                            item.CreatedOnUtc = DateTime.UtcNow;
                            OrderId = orderDao.InserOrder(item);
                        }
                    }
                    var cus = customerDao.GetCustomerById(customer.Id);
                    if (OrderId > 0)
                    {
                        foreach (var item in cus.ShoppingCartItems)
                        {
                            if (item.ShoppingCartTypeId == (int)ShoppingCartType.ShoppingCart)
                            {
                                shoppingCartItemDao.DeleteShoppingCartItem(item.Id);
                            }
                        }
                        TempData["Customer"] = cus;
                        return RedirectToAction("Completed/" + Convert.ToInt32(OrderNumer) + "", "CheckOut");
                    }
                    return View(customer);
                }
                else
                {
                    return View(customer);
                }
            }
            else
            {
                return Redirect("/");
            }
        }
        [HttpGet]
        public ActionResult Completed(int Id)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var orderDao = new OrderDao();
                var order = orderDao.GetOrderByOrderNumber(Id);
                return View(order);
            }
            else
            {
                return Redirect("/");
            }

        }
    }
}