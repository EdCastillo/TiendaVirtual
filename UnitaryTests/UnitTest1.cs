using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace UnitaryTests
{
    [TestClass]
    public class UnitTest1
    {
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImEiLCJuYmYiOjE2MjA5MzE2NTUsImV4cCI6MTYyMTE0NzY1NSwiaWF0IjoxNjIwOTMxNjU1LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUxMjIxIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTIyMSJ9.ldYHJjrFtnlZfF0zRw3ha_D-8n5OrAh2-uqqAk9YUJk";
        [TestMethod]
        public async Task TestMethod1()
        {
            Views.Managers.PCManager manager = new Views.Managers.PCManager();
            await manager.Insertar(new Views.Models.Producto_Compra { CM_PRECIO_PRODUCTO_UNIDAD = 5, COM_ID = 7, COM_PRO_CANTIDAD = 142, PRO_ID = 3 }, token);
        }
    }
}
