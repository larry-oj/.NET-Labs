namespace RestaurantAPI.Services;

public class MenuService : IMenuService
{
    private IMyUnitOfWork _uow;

    public MenuService(IMyUnitOfWork uow)
    {
        this._uow = uow;
    }

    public List<MenuItem> GetMenu()
    {
        var meals = _uow.Meals.GetAll();

        var menu = new List<MenuItem>();

        if (meals.Count() < 1)
        {
            return menu;
        }

        foreach (var meal in meals)
        {
            var ingridients = _uow.MealIngridients
                .Find(i => i.Meal == meal, i => i.Ingridient)
                .Select(i => i.Ingridient.Name)
                .ToList();

            var menuItem = new MenuItem
            {
                Name = meal.Name,
                Ingridients = ingridients,
                NormalPrice = meal.NormalPrice,
                LargePrice = meal.LargePrice
            };

            menu.Add(menuItem);
        }

        return menu;
    }
    public MenuItem GetMeal(string name)
    {
        var meal = _uow.Meals.Find(m => m.Name == name).FirstOrDefault();

        if (meal == default(Meal))
            throw new ArgumentException("Meal not found");

        var ingridients = _uow.MealIngridients
            .Find(i => i.Meal == meal, i => i.Ingridient)
            .Select(i => i.Ingridient.Name)
            .ToList();

        return new MenuItem
        {
            Name = meal.Name,
            Ingridients = ingridients,
            NormalPrice = meal.NormalPrice,
            LargePrice = meal.LargePrice
        };
    }
    public async Task ComposeMealAsync(string name, List<string> ingridients, float normalPrice, float largePrice = 0.0f)
    {
        if (_uow.Meals.Find(m => m.Name.ToLower() == name.ToLower()).Any())
            throw new ArgumentException("Meal with this name already exists");

        var meal = new Meal
        {
            Name = name,
            NormalPrice = normalPrice,
            LargePrice = largePrice
        };

        await _uow.Meals.AddAsync(meal);

        foreach (var ingridientName in ingridients)
        {
            var ingridient = _uow.Ingridients.Find(i => i.Name.ToLower() == ingridientName.ToLower()).FirstOrDefault();

            if (ingridient == default(Ingridient))
            {
                ingridient = new Ingridient
                {
                    Name = ingridientName
                };

                await _uow.Ingridients.AddAsync(ingridient);
            }

            var mealIngridient = _uow.MealIngridients.Find(i => i.Meal == meal && i.Ingridient == ingridient).FirstOrDefault();

            if (mealIngridient == default(MealIngridient))
            {
                mealIngridient = new MealIngridient
                {
                    Meal = meal,
                    Ingridient = ingridient
                };

                await _uow.MealIngridients.AddAsync(mealIngridient);
            }
        }
    }
    public async Task DeleteMealAsync(string name)
    {
        var meal = _uow.Meals.Find(m => m.Name.ToLower() == name.ToLower()).FirstOrDefault();

        if (meal == default(Meal))
            throw new ArgumentException("Meal with this name doesn't exist");

        var mealIngridients = _uow.MealIngridients.Find(i => i.Meal == meal);
        
        await _uow.MealIngridients.RemoveRangeAsync(mealIngridients);
        await _uow.Meals.RemoveAsync(meal);
    }
}