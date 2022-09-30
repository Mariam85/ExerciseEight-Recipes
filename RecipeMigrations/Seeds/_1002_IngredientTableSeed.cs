using FluentMigrator;
using RecipeMigrations.Migrations;

namespace RecipeMigrations.Seeds
{
    public record RecipeIngredient
    {
        public int RecipeId { get; set; }
        public string Component { get; set; }
    }

    [Migration(1002)]
    public class _1002_IngredientTableSeed : Migration
    {
        public static List<RecipeIngredient> recipeIngredients = new()
        {
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "1 pound spaghetti, fettuccine, or pasta of choice"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "Salt, for the pasta water"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "1 tablespoon olive oil"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "6 ounces cremini mushrooms, sliced"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "4 tablespoons unsalted butter"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "2 cloves garlic, minced"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "1/3 cup freshly grated Parmesan cheese"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "2 tablespoons white miso paste"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "Optional: Fresh sage, parsley, or thyme, for garnish"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "2 pounds extra-large shrimp (26-30 count), peeled, cleaned, and deveined"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "2 teaspoons blackened seasoning"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/2 teaspoon salt, if needed"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "8 corn or flour tortillas"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "2 cups thinly sliced red cabbage"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/4 cup mayonnaise"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/2 tablespoon granulated sugar"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/4 teaspoon black pepper"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/2 tablespoon white vinegar"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1 tablespoon vegetable oil, avocado oil, or grapeseed oil"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/2 cup crumbled Mexican queso fresco"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/2 cup chopped cilantro"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "Lime wedges"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "1/4 cup soy sauce"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "1/2 Asian pear, peeled and grated"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "2 tablespoons mirin, rice wine, or dry white wine"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "2 tablespoons sugar"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "3 large cloves garlic, minced"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "2 tablespoons toasted sesame oil"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "1/4 teaspoon freshly ground black pepper"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "1 1/2 pounds ribeye, sliced 1/8 inch thick"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "1 tablespoon canola oil"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "1/2 cup chopped green onions"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "2 tablespoons toasted sesame seeds"
            },
            new RecipeIngredient
            {
                RecipeId = 3,
                Component = "Cooked rice, to serve"
            },
            new RecipeIngredient
            {
                RecipeId = 4,
                Component = "1 tablespoon extra virgin olive oil or unsalted butter"
            },
            new RecipeIngredient
            {
                RecipeId = 4,
                Component = "1/2 pound pancetta or thick cut bacon, diced"
            },
            new RecipeIngredient
            {
                RecipeId = 4,
                Component = "1 to 2 garlic cloves, minced, about 1 teaspoon (optional)"
            },
            new RecipeIngredient
            {
                RecipeId = 4,
                Component = "3 to 4 whole eggs"
            },
            new RecipeIngredient
            {
                RecipeId = 4,
                Component = "1 cup grated Parmesan or pecorino cheese"
            },
            new RecipeIngredient
            {
                RecipeId = 4,
                Component = "1 pound spaghetti (or bucatini or fettuccine)"
            },
            new RecipeIngredient
            {
                RecipeId = 4,
                Component = "Kosher salt and freshly ground black pepper to taste"
            },
            new RecipeIngredient
            {
                RecipeId = 5,
                Component = "4 1/2 cups pork chop brine"
            },
            new RecipeIngredient
            {
                RecipeId = 5,
                Component = "4 bone-in pork rib chops, each about 2 inches thick"
            },
            new RecipeIngredient
            {
                RecipeId = 5,
                Component = "1 1/2 teaspoons Morton's kosher salt"
            },
            new RecipeIngredient
            {
                RecipeId = 5,
                Component = "1 teaspoon ground cumin"
            },
            new RecipeIngredient
            {
                RecipeId = 5,
                Component = "1/2 teaspoon freshly ground black pepper"
            },
            new RecipeIngredient
            {
                RecipeId = 5,
                Component = "1/4 teaspoon sweet paprika"
            },
            new RecipeIngredient
            {
                RecipeId = 5,
                Component = "Pinch of cayenne pepper (optional)"
            },
            new RecipeIngredient
            {
                RecipeId = 5,
                Component = "1 tablespoon vegetable oil, plus more for grill"
            },
            new RecipeIngredient
            {
                RecipeId = 6,
                Component = "2 tbsp vegetable oil (or canola or peanut oil)"
            },
            new RecipeIngredient
            {
                RecipeId = 6,
                Component = "2 large garlic cloves , very finely chopped"
            },
            new RecipeIngredient
            {
                RecipeId = 6,
                Component = "2 onion , diced"
            },
            new RecipeIngredient
            {
                RecipeId = 6,
                Component = "150 g small prawns/shrimp (cooked or raw) , or chicken breast thinly sliced (Note 1)"
            },
            new RecipeIngredient
            {
                RecipeId = 6,
                Component = "3 green onion (shallots) , cut into small pieces"
            },
            new RecipeIngredient
            {
                RecipeId = 6,
                Component = "2 eggs , lightly beaten"
            },
            new RecipeIngredient
            {
                RecipeId = 6,
                Component = "3 cups cooked jasmine rice , cold (preferably refrigerated overnight) (Note 2)"
            },
            new RecipeIngredient
            {
                RecipeId = 6,
                Component = "1 1/2 tbsp soy sauce"
            },
            new RecipeIngredient
            {
                RecipeId = 6,
                Component = "1 1/2 tbsp oyster sauce"
            },
            new RecipeIngredient
            {
                RecipeId = 7,
                Component = "70ml rapeseed oil"
            },
            new RecipeIngredient
            {
                RecipeId = 7,
                Component = "1 tsp cider vinegar"
            },
            new RecipeIngredient
            {
                RecipeId = 7,
                Component = "200ml plant milk (we used oat)"
            },
            new RecipeIngredient
            {
                RecipeId = 7,
                Component = "225g self-raising flour"
            },
            new RecipeIngredient
            {
                RecipeId = 7,
                Component = "1 tsp baking powder"
            },
            new RecipeIngredient
            {
                RecipeId = 7,
                Component = "150g golden caster sugar"
            },
            new RecipeIngredient
            {
                RecipeId = 7,
                Component = "2 tsp vanilla paste"
            },
            new RecipeIngredient
            {
                RecipeId = 7,
                Component = "200g vegan plant butter block, softened"
            },
            new RecipeIngredient
            {
                RecipeId = 7,
                Component = "400g icing sugar"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "6 tablespoons extra virgin olive oil"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "2 tablespoons fresh lemon juice"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "1/2 teaspoon chopped garlic"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "1 teaspoon red wine vinegar"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "1/2 teaspoon dried oregano or 1 teaspoon chopped fresh oregano"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "1/2 teaspoon dried dill or 1 teaspoon chopped fresh dill"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "Salt and freshly ground black pepper"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "3 large plum tomatoes, seeded and coarsely chopped"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "3/4 cucumber, peeled, seeded, and coarsely chopped"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "1/2 red onion, chopped"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "1 bell pepper, seeded and coarsely chopped"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "1/2 cup pitted black olives (preferably brine-cured), coarsely chopped"
            },
            new RecipeIngredient
            {
                RecipeId = 8,
                Component = "Heaping 1/2 cup crumbled feta cheese"
            }
        };
        public override void Up()
        {
            foreach (var ing in recipeIngredients)
            {
                Insert.IntoTable("ingredient")
                    .Row(new
                    {
                        recipe_id = ing.RecipeId,
                        component = ing.Component
                    });
            }
        }
        public override void Down()
        {
            //empty, not using
        }
    }
}