
using CarData;
using System.Collections;

internal class Program
{
    private static async void Test(CarQueries queries)
    {
        var res = await queries.GetAllCars();
        
    }
    private static async void Main(string[] args)
    {
        CarQueries queries = new CarQueries();
        Test(queries);
    }
}