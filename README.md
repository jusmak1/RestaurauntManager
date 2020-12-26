# RestaurauntManager

A restaraunt manager console c# app.

## Commands:

####  Available commands:

 * get products
 * add product [Name],[PortionCount],[Unit],[PortionSize] . Example: add product Chicken,10,kg,0.3
 * update product [Id],[Name],[PortionCount],[Unit],[PortionSize] . Example: update product 
                                    1,Chicken,20,kg,0.3
 * delete product [Id] .Example: delete product 1
 * get menu
 * add menu [Name],[ProductId1],[ProductId2] ... Example: add menu Kebab,1 2
 * update menu [Id],[Name],[ProductId1],[ProductId2] ... Eaxmple: update menu KebabBetter,1 2 3
 * delete menu [Id]. Example: delete menu 1
 * get orders
 * add order [MenuItemId1] [MenuItemId2]... .Example add order 1 2
 * /help
 
####  Creating new command:

New command class should implement **ICommand.cs** interface:

```c#
public interface ICommand
{
   bool Matches(string commandString);
   CommandResponse Execute(string commandString);
   string GetHelperText();
}
```

New command need to be registered in **CommandsFactory.cs** class:
```c#
List<ICommand> commands = new List<ICommand>();
commands.Add(new NewCreatedCommand());
```



## Data storage:

Currently data is stored in files:
* menu.csv
* order.csv
* products.csv

Make sure it exists in RestarauntManager/ folder. Names can be changed in CSVContext.cs file:

```c#
private const string PRODUCTS_FILE = "products.csv";
private const string MENU_ITEMS_FILE = "menu.csv";
private const string ORDERS_FILE = "orders.csv";
```


## Migrating to WEB

#### Migrating context

In order this app this app to migrate to web, you need to make new SQL context, which implements IContext.cs interface.

```c#
public interface IContext
{
    List<Product> GetProducts();
    bool SetProducts(IEnumerable<Product> products);
    List<MenuItem> GetMenuItems();
    bool SetMenuItems(IEnumerable<MenuItem> menuItems);
    List<Order> GetOrders();
    bool SetOrders(List<Order> orders);       
}
```

You need to inject new created implementation, to tell app to use it. 

```c#
services.AddTransient<IContext, NewCreatedSQLContext>();
```

#### Using services

API endpoints(controllers) should use service layer directly without commands.


## License
[MIT](https://choosealicense.com/licenses/mit/)
