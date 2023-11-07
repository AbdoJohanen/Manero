using Microsoft.Extensions.Configuration;

namespace Manero.Tests;

public class ConnectionStringTests
{

    [Fact]
    public void ConnectionString_ProductDatabase_Should_Be_ConnectionString_To_LocalDB()
    {
        //Arrange
        var expectedConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sangs\\Desktop\\MalinDB.mdf;Integrated Security=True;Connect Timeout=30";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        //Act
        var actualConnectionString = configuration.GetConnectionString("ProductDatabase");

        //Assert
        Assert.Equal(expectedConnectionString, actualConnectionString);

    }
}
