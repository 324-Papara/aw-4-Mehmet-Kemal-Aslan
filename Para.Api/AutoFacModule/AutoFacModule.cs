using Autofac;
using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Para.Base.Session;
using Para.Base.Token;
using Para.Bussiness.Cqrs;
using Para.Bussiness.Mapper;
using Para.Bussiness.RabbitMQ;
using Para.Bussiness.Token;
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
            builder.RegisterType<HttpContextAccessor>()
               .As<IHttpContextAccessor>()
               .SingleInstance();

            builder.Register(context =>
            {
                var httpContextAccessor = context.Resolve<IHttpContextAccessor>();
                var sessionContext = new SessionContext
                {
                    Session = JwtManager.GetSession(httpContextAccessor.HttpContext),
                    HttpContext = httpContextAccessor.HttpContext
                };
                return sessionContext;
            }).As<ISessionContext>().InstancePerLifetimeScope();

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
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
            builder.RegisterType<MemoryCache>().As<IMemoryCache>().SingleInstance();
            builder.RegisterType<MemoryDistributedCache>().As<IDistributedCache>().SingleInstance();

            // AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });
            builder.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance();

            // MediatR
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.Register(context => new BackgroundJobClient()).As<IBackgroundJobClient>().SingleInstance();
            builder.Register(context => new RecurringJobManager()).As<IRecurringJobManager>().SingleInstance();

            // RabbitMQ Publisher ve Consumer'ı ekleyin
            builder.RegisterType<EmailQueuePublisher>().As<IEmailQueuePublisher>().SingleInstance();
            builder.RegisterType<EmailQueueConsumer>().As<IEmailQueueConsumer>().SingleInstance();

        }
    }
}
