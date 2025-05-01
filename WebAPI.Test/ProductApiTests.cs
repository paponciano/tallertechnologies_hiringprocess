using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;
using Xunit;

namespace WebAPI;

public class ProductApiTests : IClassFixture<Program>
{
    private readonly HttpClient _client;

    public ProductApiTests(HttpClient httpClient)
    {
        _client = httpClient;
    }

    [Fact]
    public async Task GetAllProducts()
    {
        //Act
        var response = await _client.GetAsync("api/products");

        //Assert
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.True(products.Count >= 0);
    }
}
