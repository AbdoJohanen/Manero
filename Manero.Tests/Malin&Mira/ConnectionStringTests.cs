using Microsoft.Extensions.Configuration;

namespace Manero.Tests;

public class ConnectionStringTests
{

    [Fact]
    public void ConnectionString_ProductDatabase_Should_Be_ConnectionString_To_LocalDB()
    {
        //Arrange
        //Adding static connection string to DB, and path to the actual connection string 
        var expectedConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sangs\\Desktop\\MalinDB.mdf;Integrated Security=True;Connect Timeout=30";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        //Act
        //Retrieving the actual connection string to product database
        var actualConnectionString = configuration.GetConnectionString("ProductDatabase");

        //Assert
        //Ensure that the static connection string is equal to the actual connection string
        Assert.Equal(expectedConnectionString, actualConnectionString);

    }
}
