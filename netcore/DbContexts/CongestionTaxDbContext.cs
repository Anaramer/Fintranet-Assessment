using congestion.calculator.Entities;
using congestion.calculator.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace congestion.calculator.DbContexts
{
    public class CongestionTaxDbContext : DbContext
    {
        public CongestionTaxDbContext(DbContextOptions<CongestionTaxDbContext> options) : base(options)
        {

        }

        public DbSet<CityEntity> Cities { get; set; } = null!;
        public DbSet<TollFeeEntity> TollFees { get; set; } = null!;
        public DbSet<TollFreeDateEntity> TollFreeDates { get; set; } = null!;
        public DbSet<TollFreeDayOfWeekEntity> TollFreeDayOfWeeks { get; set; } = null!;
        public DbSet<TollFreeMonthEntity> TollFreeMonths { get; set; } = null!;
        public DbSet<TollFreeVehicleEntity> TollFreeVehicles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityEntity>().HasData(
                new CityEntity()
                {
                    Id = 1,
                    Name = "Gothenburg"
                },
                new CityEntity()
                {
                    Id = 2,
                    Name = "Others"
                }
            );

            //| Time        | Amount |
            //| ----------- | :----: |
            //| 06:00–06:29 | SEK 8  |
            //| 06:30–06:59 | SEK 13 |
            //| 07:00–07:59 | SEK 18 |
            //| 08:00–08:29 | SEK 13 |
            //| 08:30–14:59 | SEK 8  |
            //| 15:00–15:29 | SEK 13 |
            //| 15:30–16:59 | SEK 18 |
            //| 17:00–17:59 | SEK 13 |
            //| 18:00–18:29 | SEK 8  |
            //| 18:30–05:59 | SEK 0  |

            modelBuilder.Entity<TollFeeEntity>().HasData(
                new TollFeeEntity()
                {
                    Id = 1,
                    CityId = 1,
                    StartTimeOfDay = 6 * 60, // = 6:00
                    Fee = 8
                },
                new TollFeeEntity()
                {
                    Id = 2,
                    CityId = 1,
                    StartTimeOfDay = 6 * 60 + 30, // = 6:30
                    Fee = 13
                },
                new TollFeeEntity()
                {
                    Id = 3,
                    CityId = 1,
                    StartTimeOfDay = 7 * 60, // = 7:00
                    Fee = 18
                },
                new TollFeeEntity()
                {
                    Id = 4,
                    CityId = 1,
                    StartTimeOfDay = 8 * 60, // = 8:00
                    Fee = 13
                },
                new TollFeeEntity()
                {
                    Id = 5,
                    CityId = 1,
                    StartTimeOfDay = 8 * 60 + 30, // = 8:30
                    Fee = 8
                },
                new TollFeeEntity()
                {
                    Id = 6,
                    CityId = 1,
                    StartTimeOfDay = 15 * 60, // = 15:00
                    Fee = 13
                },
                new TollFeeEntity()
                {
                    Id = 7,
                    CityId = 1,
                    StartTimeOfDay = 15 * 60 + 30, // = 15:30
                    Fee = 18
                },
                new TollFeeEntity()
                {
                    Id = 8,
                    CityId = 1,
                    StartTimeOfDay = 17 * 60, // = 17:00
                    Fee = 13
                },
                new TollFeeEntity()
                {
                    Id = 9,
                    CityId = 1,
                    StartTimeOfDay = 18 * 60, // = 18:00
                    Fee = 8
                },
                new TollFeeEntity()
                {
                    Id = 10,
                    CityId = 1,
                    StartTimeOfDay = 18 * 60 + 30, // = 18:30
                    Fee = 0
                }
            );


            modelBuilder.Entity<TollFreeDateEntity>().HasData(
                new TollFreeDateEntity()
                {
                    Id = 1,
                    HolidayDate = new DateTime(2013, 1, 1)
                },
                new TollFreeDateEntity()
                {
                    Id = 2,
                    HolidayDate = new DateTime(2013, 3, 28)
                },
                new TollFreeDateEntity()
                {
                    Id = 3,
                    HolidayDate = new DateTime(2013, 4, 1)
                },
                new TollFreeDateEntity()
                {
                    Id = 4,
                    HolidayDate = new DateTime(2013, 4, 30)
                },
                new TollFreeDateEntity()
                {
                    Id = 5,
                    HolidayDate = new DateTime(2013, 5, 1)
                },
                new TollFreeDateEntity()
                {
                    Id = 6,
                    HolidayDate = new DateTime(2013, 5, 8)
                },
                new TollFreeDateEntity()
                {
                    Id = 7,
                    HolidayDate = new DateTime(2013, 5, 9)
                },
                new TollFreeDateEntity()
                {
                    Id = 8,
                    HolidayDate = new DateTime(2013, 6, 5)
                },
                new TollFreeDateEntity()
                {
                    Id = 9,
                    HolidayDate = new DateTime(2013, 6, 6)
                },
                new TollFreeDateEntity()
                {
                    Id = 10,
                    HolidayDate = new DateTime(2013, 6, 21)
                },
                new TollFreeDateEntity()
                {
                    Id = 11,
                    HolidayDate = new DateTime(2013, 11, 1)
                },
                new TollFreeDateEntity()
                {
                    Id = 12,
                    HolidayDate = new DateTime(2013, 12, 24)
                },
                new TollFreeDateEntity()
                {
                    Id = 13,
                    HolidayDate = new DateTime(2013, 12, 25)
                },
                new TollFreeDateEntity()
                {
                    Id = 14,
                    HolidayDate = new DateTime(2013, 12, 26)
                },
                new TollFreeDateEntity()
                {
                    Id = 15,
                    HolidayDate = new DateTime(2013, 12, 31)
                }
            );


            modelBuilder.Entity<TollFreeDayOfWeekEntity>().HasData(
                new TollFreeDayOfWeekEntity()
                {
                    Id = 1,
                    CityId = 1,
                    DayOfWeek = DayOfWeekEnum.Saturday,
                },
                new TollFreeDayOfWeekEntity()
                {
                    Id = 2,
                    CityId = 1,
                    DayOfWeek = DayOfWeekEnum.Sunday,
                }
            );


            modelBuilder.Entity<TollFreeMonthEntity>().HasData(
                new TollFreeMonthEntity()
                {
                    Id = 1,
                    CityId = 1,
                    Month = MonthEnum.July
                }
            );

            modelBuilder.Entity<TollFreeVehicleEntity>().HasData(
                new TollFreeVehicleEntity()
                {
                    Id = 1,
                    CityId = 1,
                    Vehicle = VehicleEnum.Motorcycle
                },
                new TollFreeVehicleEntity()
                {
                    Id = 2,
                    CityId = 1,
                    Vehicle = VehicleEnum.Tractor
                },
                new TollFreeVehicleEntity()
                {
                    Id = 3,
                    CityId = 1,
                    Vehicle = VehicleEnum.Emergency
                },
                new TollFreeVehicleEntity()
                {
                    Id = 4,
                    CityId = 1,
                    Vehicle = VehicleEnum.Diplomat
                },
                new TollFreeVehicleEntity()
                {
                    Id = 5,
                    CityId = 1,
                    Vehicle = VehicleEnum.Foreign
                },
                new TollFreeVehicleEntity()
                {
                    Id = 6,
                    CityId = 1,
                    Vehicle = VehicleEnum.Military
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
