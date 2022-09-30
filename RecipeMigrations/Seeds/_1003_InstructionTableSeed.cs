using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMigrations.Seeds
{
    public record RecipeInstruction
    {
        public int RecipeId { get; set; }
        public string Step { get; set; }
    }

    [Migration(1003)]
    public class _1003_InstructionTableSeed : Migration
    {
        public static List<RecipeInstruction> recipeInstructions = new()
        {
            new RecipeInstruction
            {
                RecipeId= 1,
                Step="Fill a large pot with water and season with a generous amount of salt. Bring it to a boil over high heat.Add the pasta and cook according to the package instructions. Reserve a cup of pasta water, then drain the pasta in a colander. Set the colander over the pasta pot so it stays warm."
            },
            new RecipeInstruction 
            {
                RecipeId = 1,
                Step="Meanwhile, heat the olive oil in a large skillet over medium heat. Add the mushrooms and sauté until golden, 5 to 8 minutes. Add the mushrooms to a bowl or directly to the empty pasta pot."
            },
            new RecipeInstruction
            {
                RecipeId = 1,
                Step="Carefully rinse and dry off the skillet that you used to cook the mushrooms, then put it back on the stove over medium heat. Add the butter to the skillet and let it melt. Add the garlic and sauté until fragrant, about 2 minutes.Add the Parmesan, miso, and 1/2 cup reserved pasta water and stir until the cheese is melted and all ingredients are evenly combined, 2 to 4 minutes."
            },
            new RecipeInstruction
            {
                RecipeId = 1,
                Step="Add the sauce to the pot with the pasta and mushrooms and toss until all of the pasta is coated in sauce. Add more pasta water, if needed, to thin out the sauce. Sprinkle with fresh herbs, if using.Serve fresh. Store leftovers in a covered container in the fridge for up to 3 days."
            },
            new RecipeInstruction
            {
                RecipeId = 2,
                Step = "Preheat the oven to 300°F.Wrap the tortillas in foil and place them on a baking sheet or pan."
            },
            new RecipeInstruction
            {
                RecipeId = 2,
                Step ="In a large bowl combine the shrimp, blackening seasoning, and salt."
            },
            new RecipeInstruction
            {
                RecipeId = 2,
                Step ="Bake the foil-wrapped tortillas until the they are warmed through, about 10 minutes. (If your tortillas are done before you finish the slaw and shrimp, turn off the oven, and leave the tortillas inside to stay warm until you are ready to assemble.)"
            },
            new RecipeInstruction
            {
                RecipeId = 2,
                Step = "In a large bowl, combine the cabbage, mayo, sugar, black pepper, and white vinegar."
            },
            new RecipeInstruction
            {
                RecipeId = 2,
                Step = "In a large nonstick skillet over medium-high heat, add in the oil. Once it’s hot, add the shrimp.Cook, until the shrimp have slightly browned on the outside and they have cooked through turning pink throughout, flipping halfway through, 2 to 3 minutes per side."
            },
            new RecipeInstruction
            {
                RecipeId = 2,
                Step="Place the tortillas in a serving platter or plates. Divide the shrimp and slaw equally between the tortillas.Garnish with cheese and cilantro. Serve with lime wedges."
            },
            new RecipeInstruction
            {
                RecipeId = 3,
                Step="In a medium bowl, add the soy sauce, grated pear, mirin, sugar, garlic, sesame oil, and black pepper. Stir until the sugar dissolves."
            },
            new RecipeInstruction
            {
                RecipeId = 3,
                Step="Add the beef and toss until it is evenly coated with the marinade. Cover it with plastic wrap and refrigerate for at least 30 minutes or overnight. At this point, you can put the marinated beef in a zip top bag and freeze it for later."
            },
            new RecipeInstruction
            {
                RecipeId = 3,
                Step="Heat the canola oil in a large cast iron grill pan over medium-high heat. Make sure the pan is hot before you add the bulgogi. Add the bulgogi in a single layer. You may need to cook it in batches. The meat should not overlap so that it chars nicely without steaming and releasing liquid. Cook until lightly charred and cooked through, 2 to 3 minutes on each side."
            },
            new RecipeInstruction
            {
                RecipeId = 3,
                Step="Transfer the bulgogi onto a serving platter or serve it straight from the pan. Sprinkle the green onions and sesame seeds on top. Serve it while hot with cooked rice on the side.Leftovers can be tightly covered and refrigerated for up to 5 days."
            },
            new RecipeInstruction
            {
                RecipeId = 4,
                Step="Put a large pot of salted water on to boil (1 tablespoon salt for every 2 quarts of water.)"
            },
            new RecipeInstruction
            {
                RecipeId = 4,
                Step="While the water is coming to a boil, heat the olive oil or butter in a large sauté pan over medium heat. Add the bacon or pancetta and cook slowly until crispy.Add the garlic (if using) and cook another minute, then turn off the heat and put the pancetta and garlic into a large bowl."
            },
            new RecipeInstruction
            {
                RecipeId = 4,
                Step="In a small bowl, beat the eggs and mix in about half of the cheese."
            },
            new RecipeInstruction
            {
                RecipeId = 4,
                Step="Once the water has reached a rolling boil, add the dry pasta, and cook, uncovered, at a rolling boil."
            },
            new RecipeInstruction
            {
                RecipeId = 4,
                Step="When the pasta is al dente (still a little firm, not mushy), use tongs to move it to the bowl with the bacon and garlic. Let it be dripping wet. Reserve some of the pasta water.Move the pasta from the pot to the bowl quickly, as you want the pasta to be hot. It's the heat of the pasta that will heat the eggs sufficiently to create a creamy sauce.Toss everything to combine, allowing the pasta to cool just enough so that it doesn't make the eggs curdle when you mix them in. (That's the tricky part.)"
            },
            new RecipeInstruction
            {
                RecipeId = 4,
                Step="Add the beaten eggs with cheese and toss quickly to combine once more. Add salt to taste. Add some pasta water back to the pasta to keep it from drying out.Serve at once with the rest of the parmesan and freshly ground black pepper. If you want, sprinkle with a little fresh chopped parsley."
            },
            new RecipeInstruction
            {
                RecipeId= 5,
                Step="In a large pot or bowl, add the cooled brine and the pork chops, making sure all of them are fully submerged in the brine. You can place a heavy plate on top to help them stay submerged. Cover and refrigerate for 1 hour.Remove the pork chops from the brine. Rinse them under cold running water, then pat them dry with paper towels. Lay the pork chops on a platter so that they come to room temperature while the grill heats."
            },
            new RecipeInstruction
            {
                RecipeId= 5,
                Step="Use a grill brush to clean the grill grates. Saturate a wad of paper towel generously with vegetable oil and rub it along the grates. If using a gas grill, heat one side to 400°F or high and keep the burners off on the other side. If using a charcoal grill, spread the hot coals on one half of the grill and leave the other half empty."
            },
            new RecipeInstruction
            {
                RecipeId = 5,
                Step="In a small bowl, combine the salt, cumin, black pepper, paprika, and cayenne pepper, if using. Use your hands to rub both sides of the pork chops with the oil. Sprinkle and massage the spice rub all over them."
            },
            new RecipeInstruction
            {
                RecipeId = 5,
                Step="When the grill reaches 400°F, place the pork chops on the hot side of the grill and press down firmly with a grill spatula. Sear for 4 minutes without moving them. This allows the meat to caramelize properly. Flip them over and sear for 4 minutes without moving them. Move the seared pork chops to the cool side of the grill, then close the lid. Cook for 15 to 20 minutes, or until a meat thermometer inserted in the center of the pork chop registers 145°F."
            },
            new RecipeInstruction
            {
                RecipeId = 5,
                Step="Transfer the grilled pork chops onto a platter and cover it loosely with foil. Rest them for 5 minutes before serving, which will allow the juices to settle down."
            },
            new RecipeInstruction
            {
                RecipeId = 6,
                Step="Heat oil in a large wok or a large skillet over medium high heat. (Add 1 tbsp extra oil if using a skillet)"
            },
            new RecipeInstruction
            {
                RecipeId = 6,
                Step="Add garlic and onion, stir fry for 30 seconds."
            },
            new RecipeInstruction
            {
                RecipeId = 6,
                Step="Add prawns/shrimp and white part of green onion, stir fry for 1 minute."
            },
            new RecipeInstruction
            {
                RecipeId = 6,
                Step="Push everything to the side and pour the egg onto the other side. Scramble it quickly - about 20 seconds."
            },
            new RecipeInstruction
            {
                RecipeId = 6,
                Step="Add the rice and Sauce of choice. Stir fry for 2 minutes, adding green part of green onions halfway through, until all the rice is coated with the Sauce."
            },
            new RecipeInstruction
            {
                RecipeId = 6,
                Step="Serve immediately, garnished with fresh cilantro/coriander with tomato and cucumbers on the side. (See in post for more serving ideas)"
            },
            new RecipeInstruction
            {
                RecipeId = 7,
                Step="Combine the rapeseed oil, cider vinegar and oat milk in a jug. Heat the oven to 180C/160C fan/gas 4. Line a 12-hole cupcake or muffin tray with paper cases. Tip the flour, baking powder and sugar into a large bowl. Make a well in the middle and slowly pour in the wet mixture, whisking constantly. Add the vanilla paste and stir to combine. Divide evenly between the paper cases and bake for 15-20 mins until golden and a skewer inserted into the middle of a cupcake comes out clean. Transfer to a cooling rack to cool completely."
            },
            new RecipeInstruction
            {
                RecipeId = 7,
                Step="For the icing, beat the plant block and vanilla until soft and creamy, around 2 mins. Add the icing sugar in batches, beating well after each addition, and a large pinch of salt. Transfer to a piping bag fitted with a star nozzle. Pipe swirls on top of each cooled cupcake. Will keep, iced, for up to three days in an airtight container."
            },
            new RecipeInstruction
            {
                RecipeId = 8,
                Step="Whisk the olive oil, lemon juice, garlic, vinegar, oregano, and dill together until blended. Season to taste with salt and freshly ground black pepper."
            },
            new RecipeInstruction
            {
                RecipeId = 8,
                Step="Combine the tomatoes, cucumber, onion, bell pepper, olives in a bowl. Toss with dressing. Sprinkle with cheese and serve."
            }
        };
        public override void Up()
        {
            foreach (var instruction in recipeInstructions)
            {

                Insert.IntoTable("instruction")
                    .Row(new
                    {
                        recipe_id =instruction.RecipeId,
                        step =instruction.Step
                    });
            }
        }

        public override void Down()
        {
            //empty, not using
        }
    }
}
