using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieManagement.Domain.Enums;
using MovieManagement.Domain.Enums.UserEnums;
using MovieManagement.Domain.POCO;
using MovieManagement.PresistentDB.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.PresistentDB.Seed
{
    public static class MovieManagementSeed
    {
        public async static Task Initialize(MovieManagementContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                await MigrateAsync(context);
                await SeedEverything(context, userManager,roleManager);
            }
            catch (Exception)
            {
                throw;
            }

        }

        private async static Task SeedEverything(MovieManagementContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var seeded = false;

            SeedMovies(context, ref seeded);
            await SeedRolesAsync(roleManager);
            await SeedAdminASync(userManager);

            if (seeded)
                context.SaveChanges();
        }

        private async static Task MigrateAsync(MovieManagementContext context)
        {
            await context.Database.MigrateAsync();
        }

        private static void SeedMovies(MovieManagementContext context, ref bool seeded)
        {
            var movies = new List<Movie>(){


                new Movie()
                {
                    Tittle="Morbius",
                    ReleaseYear=2022,
                    Genre=Genres.Adventure,
                    IMDB ="7.2",
                    Details="Biochemist Michael Morbius tries to cure himself of a rare blood disease, but he inadvertently infects himself with a form of vampirism instead.",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(12),
                    DurationInMinutes=104,
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/morbius_qr2n41yx_480x.progressive.jpg?v=1644515385"
                },
                new Movie()
                {
                    Tittle="The Outfit",
                    ReleaseYear=2022,
                    Genre=Genres.Crime,
                    IMDB ="7.5",
                    Details="An expert tailor must outwit a dangerous group of mobsters in order to survive a fateful night.",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(23),
                    DurationInMinutes=105,
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/the-outfit_jrunjizf_480x.progressive.jpg?v=1645726889"
                },
                new Movie()
                {
                    Tittle="Lightyear",
                    ReleaseYear=2022,
                    Genre=Genres.Animation,
                    IMDB ="7.7",
                    Details="The story of Buzz Lightyear and his adventures to infinity and beyond.",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(21),
                    DurationInMinutes=107,
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/lightyear_h4engnxh_480x.progressive.jpg?v=1646409144"
                },
                new Movie()
                {
                    Tittle="Sonic the Hedgehog 2",
                    ReleaseYear=2022,
                    Genre=Genres.Animation,
                    IMDB ="7.9",
                    Country ="Japan",
                    Details="When the manic Dr Robotnik returns to Earth with a new ally, Knuckles the Echidna, Sonic and his new friend Tails is all that stands in their way.",
                    StartTime =DateTime.Now.AddHours(21),
                    DurationInMinutes=122,
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/sonic2.ar_480x.progressive.jpg?v=1647357123"
                },
                new Movie()
                {
                    Tittle="The Lost City",
                    ReleaseYear=2022,
                    Genre=Genres.Adventure,
                    IMDB ="6.8",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(5),
                    DurationInMinutes=112,
                    Details="A reclusive romance novelist on a book tour with her cover model gets swept up in a kidnapping attempt that lands them both in a cutthroat jungle adventure.",
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/the-lost-city_p8r34kub_480x.progressive.jpg?v=1640964267"
                },
                new Movie()
                {
                    Tittle="The Terminator",
                    ReleaseYear=1984,
                    Genre=Genres.Action,
                    IMDB ="8.1",
                    Country ="United Kingdom",
                    Details="A human soldier is sent from 2029 to 1984 to stop an almost indestructible cyborg killing machine, sent from the same year, which has been programmed to execute a young woman whose unborn son is the key to humanity's future salvation.",
                    StartTime =DateTime.Now.AddHours(3),
                    DurationInMinutes=107,
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/4b60cb64364d2442335ce15728362d46_6fb530ed-80c4-4df5-8496-db3ef2471e2d_500x749.jpg?v=1573616139"
                },
                new Movie()
                {
                    Tittle="Beverly Hills Cop",
                    ReleaseYear=1984,
                    Genre=Genres.Crime,
                    IMDB ="7.4",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(21),
                    DurationInMinutes=105,
                    Details="A freewheeling Detroit cop pursuing a murder investigation finds himself dealing with the very different culture of Beverly Hills.",
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/ee86fa7d828283ec1973b0cb7b6bca43_343f3841-e51a-4757-b4ae-fa5c03d74875_500x749.jpg?v=1573590134"
                },
                new Movie()
                {
                    Tittle="Ghostbusters: Afterlife",
                    ReleaseYear=2021,
                    Genre=Genres.Fantasy,
                    IMDB ="7.2",
                    Country ="Canada",
                    StartTime =DateTime.Now.AddHours(3),
                    DurationInMinutes=124,
                    Status=Statuses.Published,
                    Details="When a single mom and her two kids arrive in a small town, they begin to discover their connection to the original Ghostbusters and the secret legacy their grandfather left behind.",
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/dde9bbf45ae946f56d6d21dd8bc415fb_146b215d-5ece-4a5e-a787-a0807cd887f6_500x749.jpg?v=1573592552"
                },
                new Movie()
                {
                    Tittle="Pirates of the Caribbean",
                    ReleaseYear=2003,
                    Genre=Genres.Adventure,
                    IMDB ="8.1",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(4),
                    DurationInMinutes=143,
                    Status=Statuses.Published,
                    Details="Blacksmith Will Turner teams up with eccentric pirate \"Captain\" Jack Sparrow to save his love, the governor's daughter, from Jack's former pirate allies, who are now undead.",
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/c9d5fb11c495e731413c16f8ad6838e0_adc81f87-ccdf-4f98-8b7f-cdaa8884f515_480x.progressive.jpg?v=1573585499"                },
                new Movie()
                {
                    Tittle="Kill Bill: Vol. 1",
                    ReleaseYear=2003,
                    Genre=Genres.Action,
                    IMDB ="8.2",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(2),
                    DurationInMinutes=111,
                    Status=Statuses.Published,
                    Details="After awakening from a four-year coma, a former assassin wreaks vengeance on the team of assassins who betrayed her.",

                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/KILLBILL.VOL.1.PW_500x749.jpg?v=1574966219"
                },
                new Movie(){
                    Tittle="Heat",
                    ReleaseYear=1995,
                    Genre=Genres.Drama,
                    IMDB="8.3",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(5),
                    DurationInMinutes=170,
                    Status=Statuses.Published,
                    Details="A group of high-end professional thieves start to feel the heat from the LAPD when they unknowingly leave a clue at their latest heist.",

                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/8478c0a6a6e0651561bd9f729cbadd0c_3045e9b1-f956-45eb-ac31-398c4e7c8865_480x.progressive.jpg?v=1573593675"
                },
                new Movie()
                {
                    Tittle="Back to the Future",
                    ReleaseYear=1985,
                    Genre=Genres.Comedy,
                    IMDB ="8.6",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(5),
                    DurationInMinutes=146,
                    Status=Statuses.Published,
                    Details="Marty McFly, a 17-year-old high school student, is accidentally sent thirty years into the past in a time-traveling DeLorean invented by his close friend, the eccentric scientist Doc Brown.",

                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/0b2b67a1de6a06d1ce65e4ccc64047e3_a9f7318e-c5c4-4d2a-aed2-890bbfad883c_500x749.jpg?v=1573590273"
                },
                new Movie()
                {
                    Tittle="The Batman",
                    ReleaseYear=2022,
                    Genre=Genres.Action,
                    IMDB ="8.4",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(3),
                    DurationInMinutes=176,
                    Status=Statuses.Published,
                    Details="When the Riddler, a sadistic serial killer, begins murdering key political figures in Gotham, Batman is forced to investigate the city's hidden corruption and question his family's involvement.",

                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/the-batman_rl8gg0zf_480x.progressive.jpg?v=1645738459"
                },
                new Movie()
                {
                    Tittle="The Goonies",
                    ReleaseYear=1985,
                    Genre=Genres.Adventure,
                    IMDB ="7.7",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(4),
                    DurationInMinutes=114,
                    Status=Statuses.Published,
                    Details="A group of young misfits called The Goonies discover an ancient map and set out on an adventure to find a legendary pirate's long-lost treasure.",

                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/65c06c356427a553a4a50ed45cb57cc2_dfadaf63-bb64-4f5e-b1ca-47ddb691714c_500x749.jpg?v=1573587418"
                },
                new Movie()
                {
                    Tittle="The Matrix",
                    ReleaseYear=1999,
                    Genre=Genres.Action,
                    IMDB ="8.7",
                    Details="When a beautiful stranger leads computer hacker Neo to a forbidding underworld, he discovers the shocking truth--the life he knows is the elaborate deception of an evil cyber-intelligence.",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(2),
                    DurationInMinutes=136,
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/9fcc8387e9d47ab5af4318d7183f6d2b_19f7e1e1-3941-4c27-bad1-1f6dd70f35e0_500x749.jpg?v=1573587594"
                },
                new Movie()
                {
                    Tittle="The Forever Purge",
                    ReleaseYear=2021,
                    Genre=Genres.Horror,
                    IMDB ="6.4",
                    Details="All the rules are broken as a sect of lawless marauders decides that the annual Purge does not stop at daybreak and instead should never end.",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(2),
                    DurationInMinutes=103,
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/purgeforever_480x.progressive.jpg?v=1629381447"
                },
                new Movie()
                {
                    Tittle="Blacklight",
                    ReleaseYear=2022,
                    Genre=Genres.Action,
                    IMDB ="6.7",
                    Details="Travis Block is a government operative coming to terms with his shadowy past. When he discovers a plot targeting U.S. citizens, Block finds himself in the crosshairs of the FBI director he once helped protect.",
                    Country ="Australia",
                    StartTime =DateTime.Now.AddHours(5),
                    DurationInMinutes=104,
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/blacklight_arnncwn3_480x.progressive.jpg?v=1644515804"
                },
                new Movie()
                {
                    Tittle="The Northman",
                    ReleaseYear=2022,
                    Genre=Genres.Adventure,
                    IMDB ="8",
                    Details="From visionary director Robert Eggers comes The Northman, an action-filled epic that follows a young Viking prince on his quest to avenge his father's murder.",
                    Country ="Canada",
                    StartTime =DateTime.Now.AddHours(2),
                    DurationInMinutes=136,
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/the-northman_asdvqcek_480x.progressive.jpg?v=1644514475"
                },
                new Movie()
                {
                    Tittle="Top Gun: Maverick",
                    ReleaseYear=2022,
                    Genre=Genres.Action,
                    IMDB ="8",
                    Details="After more than thirty years of service as one of the Navy's top aviators, Pete Mitchell is where he belongs, pushing the envelope as a courageous test pilot and dodging the advancement in rank that would ground him.",
                    Country ="China",
                    StartTime =DateTime.Now.AddHours(22),
                    DurationInMinutes=139,
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/top_gun_maverick_ver2_480x.progressive.jpg?v=1578430896"
                },
                new Movie()
                {
                    Tittle="Jurassic World: Dominion",
                    ReleaseYear=2022,
                    Genre=Genres.Adventure,
                    IMDB ="7.4",
                    Details="Four years after the destruction of Isla Nublar, dinosaurs now live--and hunt--alongside humans all over the world. This fragile balance will reshape the future.",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(22),
                    DurationInMinutes=116,
                    Status=Statuses.Published,
                    URL="https://cdn.shopify.com/s/files/1/0057/3728/3618/products/dominion_480x.progressive.jpg?v=1629381371"
                },
                new Movie()
                {
                    Tittle="PUBLIC ENEMIES",
                    ReleaseYear=2009,
                    Genre=Genres.Crime,
                    IMDB ="7.0",
                    Details="The Feds try to take down notorious American gangsters John Dillinger, Baby Face Nelson and Pretty Boy Floyd during a booming crime wave in the 1930s.",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(4),
                    DurationInMinutes=140,
                    Status=Statuses.Published,
                    URL="https://i.pinimg.com/originals/ae/52/7f/ae527f675cd8f67a6e81240711ba8ab2.jpg"
                },
                new Movie()
                {
                    Tittle="SCENT OF A WOMAN",
                    ReleaseYear=1992,
                    Genre=Genres.Drama,
                    IMDB ="8.0",
                    Details="A prep school student needing money agrees to \"babysit\" a blind man, but the job is not at all what he anticipated.",
                    Country ="United states",
                    StartTime =DateTime.Now.AddHours(4),
                    DurationInMinutes=156,
                    Status=Statuses.Published,
                    URL="https://movierob.files.wordpress.com/2014/06/scent-of-a-woman.jpg"
                },
                new Movie()
                {
                    Tittle="INTERSTELLAR",
                    ReleaseYear=2014,
                    Genre=Genres.Action,
                    IMDB ="8.6",
                    Details="A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
                    Country ="Canada",
                    StartTime =DateTime.Now.AddHours(7),
                    DurationInMinutes=169 ,
                    Status=Statuses.Published,
                    URL="https://i.pinimg.com/564x/73/fb/94/73fb94c6e84ba8b21dfb653f10be4701.jpg"
                },
                new Movie()
                {
                    Tittle="GOOD WILL HUNTING",
                    ReleaseYear=1997,
                    Genre=Genres.Drama,
                    IMDB ="8.3",
                    Details="Will Hunting, a janitor at M.I.T., has a gift for mathematics, but needs help from a psychologist to find direction in his life.",
                    Country ="Canada",
                    StartTime =DateTime.Now.AddHours(2),
                    DurationInMinutes=127,
                    Status=Statuses.Published,
                    URL="https://wallpaperaccess.com/full/5721877.jpg"
                },
                new Movie()
                {
                    Tittle="THE SHAWSHANK REDEMPTION",
                    ReleaseYear=1994,
                    Genre=Genres.Drama,
                    IMDB ="9.3",
                    Details="Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(6),
                    DurationInMinutes=142,
                    Status=Statuses.Published,
                    URL="https://i.pinimg.com/originals/d6/d2/a8/d6d2a89d216307ed91c97c36616ea2c4.jpg"
                },
                new Movie()
                {
                    Tittle="LEON: THE PROFESSIONAL",
                    ReleaseYear=1994,
                    Genre=Genres.Detective,
                    IMDB ="8.5",
                    Details="12-year-old Mathilda is reluctantly taken in by Léon, a professional assassin, after her family is murdered. An unusual relationship forms as she becomes his protégée and learns the assassin's trade.",
                    Country ="France",
                    StartTime =DateTime.Now.AddHours(7),
                    DurationInMinutes=111 ,
                    Status=Statuses.Published,
                    URL="https://cdn.posteritati.com/posters/000/000/057/723/leon-the-professional-md-web.jpg"
                },
                new Movie()
                {
                    Tittle="THE REVENANT",
                    ReleaseYear=2015,
                    Genre=Genres.Drama,
                    IMDB ="8.0",
                    Details="A frontiersman on a fur trading expedition in the 1820s fights for survival after being mauled by a bear and left for dead by members of his own hunting team.",
                    Country ="Canada",
                    StartTime =DateTime.Now.AddHours(21),
                    DurationInMinutes=157 ,
                    Status=Statuses.Published,
                    URL="https://i.pinimg.com/564x/eb/19/c0/eb19c0ed4ae7f5ac8ce644f840dceb13.jpg"
                },
                new Movie()
                {
                    Tittle="PEARL HARBOR",
                    ReleaseYear=2001,
                    Genre=Genres.History,
                    IMDB ="6.2",
                    Details="A tale of war and romance mixed in with history. The story follows two lifelong friends and a beautiful nurse who are caught up in the horror of an infamous Sunday morning in 1941.",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(21),
                    DurationInMinutes=183,
                    Status=Statuses.Published,
                    URL="https://i.pinimg.com/564x/60/89/90/6089906ad5a98a76a6deab9195167244.jpg"
                },
                new Movie()
                {
                    Tittle="127 HOURS",
                    ReleaseYear=2010,
                    Genre=Genres.Adventure,
                    IMDB ="7.5",
                    Details="A mountain climber becomes trapped under a boulder while canyoneering alone near Moab, Utah and resorts to desperate measures in order to survive.",
                    Country ="United Kingdom",
                    StartTime =DateTime.Now.AddHours(2),
                    DurationInMinutes=94,
                    Status=Statuses.Published,
                    URL="https://i.pinimg.com/564x/62/df/fe/62dffe7102e764cc7f7ac90527bb0deb.jpg"
                },
                new Movie()
                {
                    Tittle="NO COUNTRY FOR OLD MEN",
                    ReleaseYear=2007,
                    Genre=Genres.Detective,
                    IMDB ="8.1",
                    Details="Violence and mayhem ensue after a hunter stumbles upon a drug deal gone wrong and more than two million dollars in cash near the Rio Grande.",
                    Country ="United States",
                    StartTime =DateTime.Now.AddHours(21),
                    DurationInMinutes=122,
                    Status=Statuses.Published,
                    URL="https://i.pinimg.com/564x/07/fb/ce/07fbceb16e804e9591cdfcb34ab41188.jpg"
                }

           };

            foreach (var movie in movies)
            {
                if (context.Movies.AnyAsync(x => x.Tittle == movie.Tittle).Result) continue;

                context.Movies.Add(movie);
                seeded = true;
            }
        }

        private async static Task SeedAdminASync(UserManager<User> userManager)
        {
            var defaultUser = new User
            {
                UserName = "Admin",
                Email = "Admin@gmail.com",
                FirstName = "davit",
                LastName = "jimshiashvili",
                Password= "Password123",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, defaultUser.Password);
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin);
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderator);
                    await userManager.AddToRoleAsync(defaultUser, Roles.User);
                    await userManager.AddToRoleAsync(defaultUser, Roles.Guest);
                  
                }
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator));
            await roleManager.CreateAsync(new IdentityRole(Roles.User));
            await roleManager.CreateAsync(new IdentityRole(Roles.Guest));
           
        }
    }
}
