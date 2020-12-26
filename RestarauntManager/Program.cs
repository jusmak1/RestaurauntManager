using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestarauntManager.Command;
using RestarauntManager.Command.CommandsFactory;
using RestarauntManager.Helpers;
using RestarauntManager.Models;
using RestarauntManager.Repositories.Classes;
using RestarauntManager.Repositories.Interfaces;
using RestarauntManager.Services.Classes;
using RestarauntManager.Services.Interfaces;
using System;

namespace RestarauntManager
{
    class Program
    {
        private static CommandsFactory _commandsFactory;
        private static ILogger<Program> _logger;

        static void Main(string[] args)
        {
            Init();

            Process();
        }

        private static void Process()
        {
            while (true)
            {
                var input = Console.ReadLine();

                bool commandFound = false;

                foreach (var command in _commandsFactory.GetAllCommands())
                {
                    if (command.Matches(input))
                    {
                        var result = command.Execute(input);
                        if (!string.IsNullOrEmpty(result.Message))
                        {
                            if (result.Success)
                            {
                                _logger.LogInformation(result.Message);
                            }
                            else
                            {
                                _logger.LogError(result.Message);
                            }
                        }
                        commandFound = true;
                    }
                }
                if (!commandFound)
                    _logger.LogError("Command was not found write /help for all available commands");
            }
        }

        private static void Init()
        {
            var ServiceProvider = GetServiceProvider();
            _commandsFactory = InitCommandsFactory(ServiceProvider);
            _logger = ServiceProvider.GetService<ILogger<Program>>();
            _logger.LogInformation(_commandsFactory.GetHelperText());
        }

        private static CommandsFactory InitCommandsFactory(ServiceProvider serviceProvider)
        {
            return new CommandsFactory(serviceProvider.GetService<IProductService>(),
                                       serviceProvider.GetService<IMenuService>(),
                                       serviceProvider.GetService<IOrderService>(),
                                       serviceProvider.GetService<IParser>());
                                       
        }

        private static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                .AddTransient<IParser, Parser>()
                .AddTransient<IContext, CSVContext>()
                .AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IProductService, ProductService>()
                .AddTransient<IMenuService, MenuService>()
                .AddTransient<IMenuRepository, MenuRepository>()
                .AddTransient<IOrderService, OrderService>()
                .AddTransient<IOrderRepository, OrderRepository>()
                .AddTransient<ICommandsFactory, CommandsFactory>()
                .AddTransient<Program>();

            // Auto mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
    }
}
