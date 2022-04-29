using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieManagement.Domain.POCO;
using MovieManagement.PresistentDB.Context;

[assembly: HostingStartup(typeof(Movies.ITAcademy.Ge.ControlPanel.Areas.Identity.IdentityHostingStartup))]
namespace Movies.ITAcademy.Ge.ControlPanel.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}