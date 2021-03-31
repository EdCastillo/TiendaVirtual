using NUnit.Framework;
using System.Threading.Tasks;
using Views.Managers;
using Views.Models;

namespace PruebasUnitarias
{
    public class Tests
    {
        public static string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImEiLCJuYmYiOjE2MTQzNzM4NTgsImV4cCI6MTYxNDM3NTY1OCwiaWF0IjoxNjE0MzczODU4LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUxMjIxIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTIyMSJ9.M_zYj1_eesjX0NP37XOh9pEIwFH-teC-cxx0mOlM9AQ";

        [Test]
        public async Task Test1()
        {
            CarritoManager manager = new CarritoManager();
            Producto_Carrito carrito=await manager.Ingresar(new Producto_Carrito {US_ID=1,CAR_PRO_CANTIDAD=12,PRO_ID=3},token);
        }
        [Test]
        public async Task Test2() { 
            
        }
    }
}