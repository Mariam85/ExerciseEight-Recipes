using System;
using System.Collections.Generic;
using System.Text;

public class Recipe
{
    public int Id { get; set; }
    public List<string> Ingredients { get; set; } = new List<string>();
    public string Title { get; set; }
    public List<string> Instructions { get; set; } = new List<string>();
    public List<string> Categories { get; set; } = new List<string>();

    public Recipe(List<string> ingredients, string title, List<string> instructions, List<string> categories)
    {
        Id = 0535356;
        this.Ingredients = ingredients;
        this.Title = title;
        this.Instructions = instructions;
        this.Categories = categories;
    }
    public Recipe()
    {
        Id = 0535356;
        this.Ingredients = new();
        this.Title = "";
        this.Instructions = new();
        this.Categories = new();
    }
}