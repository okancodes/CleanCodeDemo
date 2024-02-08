﻿
namespace CleanCodeDemo
{
    class program { 
    static void Main(string[] args)
    {
        IProductService productService = new ProductManager(new IsBankServiceAdapter());
        productService.Sell(new Product { Id=1, Name = "Tablet", Price=7500},
            new Customer {Id = 1, Name = "Okan", IsStudent=true, IsOfficer=false});
    }
    }
    class Program : IEntity
    {

    }
    class Customer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStudent { get; set; }
        public bool IsOfficer {  get; set; }

    }

    interface IEntity
    {
    }

    class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
    class ProductManager : IProductService
    {
       private IBankService _bankService;

        public ProductManager(IBankService bankService)
        {
            _bankService = bankService;
        }

        public void Sell(Product product, Customer customer)
        {
            
            decimal price = product.Price;
            decimal StudentDiscount = 0.90m;
            decimal officerDiscount = 0.80m;
            if (customer.IsStudent)
            {
                price = price * StudentDiscount;
            }
           if (customer.IsOfficer)
            {
                price = price * officerDiscount;

            }

            
          price = _bankService.ConvertRate(new CurrencyRate { Currency=1, Price=price });
            Console.WriteLine(price);
            Console.ReadLine();

        }
    }

    internal interface IProductService
    {
        void Sell(Product product, Customer customer);      
        
    }

    class FakeBankService:IBankService
    {
        public  decimal ConvertRate(CurrencyRate currencyRate)
        {
            return currencyRate.Price / (decimal)30.67;
        }
    }

    internal interface IBankService
    {
        decimal ConvertRate(CurrencyRate currencyRate);
    }

    class CurrencyRate
    {
        public decimal Price { get; set; }
        public int Currency { get; set; }

    }
    class CentralBankAdapter : IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            CenteralBankService centeralBankService = new CenteralBankService();
            return centeralBankService.ConvertCurrency(currencyRate);
        }
    }
    //Merkez bankasının kodu gibi 
    class CenteralBankService 
    {
        public decimal ConvertCurrency(CurrencyRate currencyRate)
        {
            return currencyRate.Price / (decimal)39.27;
        }
    }
    class IsBankService
    {
        public decimal ConvertCurrency(CurrencyRate currencyRate)
        {
            return currencyRate.Price / (decimal)30.25;
        }
    }
    class IsBankServiceAdapter : IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            IsBankService IsBankService = new IsBankService();
            return IsBankService.ConvertCurrency(currencyRate);
        }
    }
}