using Autofac;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Para.Bussiness.Cqrs;
using Para.Bussiness.Mapper;
using Para.Data.Context;
using Para.Data.CustomerReportRepository;
using Para.Data.UnitOfWork;
using System.Reflection;

namespace Para.Api.AutoFacModule
{
    public class AutoFacModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public AutoFacModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // DbContext
            builder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ParaMsSqlDbContext>();
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MsSqlConnection"));
                return new ParaMsSqlDbContext(optionsBuilder.Options);
            }).InstancePerLifetimeScope();

            // UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            // CustomerReportRepository
            builder.RegisterType<CustomerReportRepository>().As<ICustomerReportRepository>().InstancePerLifetimeScope();

            // AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });
            builder.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance();

            // MediatR
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).GetTypeInfo().Assembly).AsImplementedInterfaces();
        }
    }
}
