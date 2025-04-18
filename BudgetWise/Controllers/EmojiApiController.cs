using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace BudgetWise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmojiApiController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmojiApiController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("flattened")]
        public async Task<List<EmojiItem>> GetFlattenedEmojis()
        {
            try
            {
                // Path to the emoji.json file in wwwroot/data
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "data", "emoji.json");
                
                // Check if file exists
                if (!System.IO.File.Exists(filePath))
                {
                    Console.WriteLine($"Error: emoji.json file not found at path: {filePath}");
                    return new List<EmojiItem>();
                }

                // Read and parse the JSON file
                using var fileStream = System.IO.File.OpenRead(filePath);
                var options = new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true 
                };
                var emojis = await JsonSerializer.DeserializeAsync<List<LocalEmoji>>(fileStream, options);

                if (emojis == null)
                {
                    Console.WriteLine("Error: Failed to deserialize emoji.json");
                    return new List<EmojiItem>();
                }

                Console.WriteLine($"Successfully loaded {emojis.Count} emojis from local file");

                // Convert to our application's emoji format
                var emojiItems = emojis
                    .Where(e => !string.IsNullOrEmpty(e.Character)) // Filter out emojis with no character
                    .Select(e => new EmojiItem
                    {
                        name = e.name ?? "Unnamed emoji",
                        category = GetCategoryFromEmoji(e),
                        group = e.Category ?? "Uncategorized",
                        // For flag emojis or any emoji, ensure we're storing just the actual character
                        htmlCode = e.Character.Contains(" ") ? e.Character.Split(' ')[0] : e.Character
                    })
                    .ToList();
                
                // Log a sample of emoji items for debugging
                if (emojiItems.Count > 0)
                {
                    Console.WriteLine("Sample emoji entries:");
                    foreach (var item in emojiItems.Take(5))
                    {
                        Console.WriteLine($"Name: {item.name}, Category: {item.category}, HtmlCode: {item.htmlCode}");
                    }
                    
                    // Log a few flag emojis specifically if they exist
                    var flags = emojiItems.Where(e => e.category == "Flags").Take(3).ToList();
                    if (flags.Any())
                    {
                        Console.WriteLine("Sample flag emojis:");
                        foreach (var flag in flags)
                        {
                            Console.WriteLine($"Flag: {flag.name}, HtmlCode: {flag.htmlCode}");
                        }
                    }
                }

                return emojiItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred loading emojis: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return new List<EmojiItem>();
            }
        }

        // Helper method to categorize emojis
        private string GetCategoryFromEmoji(LocalEmoji emoji)
        {
            string category = emoji.Category?.ToLower() ?? string.Empty;
            
            // Create a simplified category based on the emoji type
            if (category.Contains("face"))
                return "Smileys & Emotion";
            else if (category.Contains("animal") || category.Contains("plant"))
                return "Animals & Nature";
            else if (category.Contains("food") || category.Contains("drink"))
                return "Food & Drink";
            else if (category.Contains("travel") || category.Contains("place"))
                return "Travel & Places";
            else if (category.Contains("activity") || category.Contains("sport"))
                return "Activities";
            else if (category.Contains("object"))
                return "Objects";
            else if (category.Contains("symbol"))
                return "Symbols";
            else if (category.Contains("flag"))
                return "Flags";
            else
                return "Other";
        }

        // Structure that matches the emoji.json file format
        public class LocalEmoji
        {
            public string name { get; set; }
            // Using different property names to avoid C# keyword 'char'
            [System.Text.Json.Serialization.JsonPropertyName("char")]
            public string Character { get; set; }
            
            [System.Text.Json.Serialization.JsonPropertyName("category")]
            public string Category { get; set; }
            
            public string[] keywords { get; set; }
        }

        // Our application's emoji structure (kept the same for compatibility)
        public class EmojiItem
        {
            public string name { get; set; }
            public string category { get; set; }
            public string group { get; set; }
            public string htmlCode { get; set; }
        }
    }
}