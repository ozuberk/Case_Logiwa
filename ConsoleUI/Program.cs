using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            ProductTest();
        }
        private static void ProductTest()
        {
            Product deneme = new Product();
            deneme.ProductTitle = "";
            deneme.StockQuantity = 1;
            deneme.Description = "Deneme";
            deneme.Category.CategoryId = 1;

            ProductManager productManager = new ProductManager(new EFProductDal());
            productManager.Add(deneme);


            //foreach (var item in productManager.GetAll())
            //{
            //    Console.WriteLine($"Ürün Başlığı:{item.ProductTitle},  Açıklaması:{item.Description}");
            //}
        }

    }
}