using Eagency.Dal.Entities;
using Eagency.Web.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Dal.EntityConfigurations
{
    public class PropertyEntityConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasData(
                new Property()
                {
                    Id = 1,
                    NumberOfParkingSpaces = 2,
                    NumberOfBathrooms = 1,
                    NumberOfBedrooms = 3,
                    NumberOfGarages = 1,
                    Area = 110,
                    AgentId = "admin",
                    HouseType = HouseType.Detached,
                    Description = "Situated on the first floor we offer a two bedroom apartment offering partial Marina Views. The apartment is within walking distance to Swansea Marina and all the local restaurants and coffee shops, close to the new music arena and town centre. The apartment offers a L shaped hallway leading to lounge with french doors and balcony, master bedroom, bathroom, kitchen and second bedroom and benifits from lift access, recently fitted UPVC windows. and allocated secure underground parking.",
                    ImageName = "alap1.jpg",
                    IsFurnished = true,
                    Price = 22000
                },
                new Property()
                {
                    Id = 2,
                    NumberOfParkingSpaces = 1,
                    NumberOfBathrooms = 2,
                    NumberOfBedrooms = 4,
                    NumberOfGarages = 2,
                    Area = 320,
                    AgentId = "admin",
                    HouseType = HouseType.Apartment,
                    Description = "Built around a traditional Gower cottage and situated within the welcoming Gower village of Horton you will find the spectacular family home that is Castle Hill Cottage. Sitting at the top of its private drive, this detached family home enjoys incredible views over the most southerly point of Gower with its rugged gorse covered cliffs and dunes; over Port Eynon Bay and its rocky point; over the Bristol Channel across to Ilfracombe in North Devon and, on a clear day, as far to the West as Lundy. In its elevated position it is just paces away from the sandy beach of Horton and within a short walk along the boardwalk to Port Eynon. This well-presented home sits in readiness for its new owners to create new memories.",
                    ImageName = "alap2.jpg",
                    IsFurnished = false,
                    Price = 35500
                },
                new Property()
                {
                    Id = 3,
                    NumberOfParkingSpaces = 2,
                    NumberOfBathrooms = 1,
                    NumberOfBedrooms = 2,
                    NumberOfGarages = 2,
                    Area = 35,
                    AgentId = "admin",
                    HouseType = HouseType.Mediterranean,
                    Description = "This very popular and pretty, coastal village of Horton is nestled between the glorious Oxwich Bay and Port Eynon Bay along the South coast of the Gower Peninsula. A popular beach side spot, Horton is in an area of outstanding natural beauty which is blessed with many award-winning sandy beaches and amazing coastal walks.",
                    ImageName = "alap3.jpg",
                    IsFurnished = true,
                    Price = 17400
                },
                new Property()
                {
                    Id = 4,
                    NumberOfParkingSpaces = 0,
                    NumberOfBathrooms = 2,
                    NumberOfBedrooms = 4,
                    NumberOfGarages = 0,
                    Area = 135,
                    AgentId = "admin",
                    HouseType = HouseType.Cottage,
                    Description = "A three bedroom mid-terrace property within the popular Swansea suburb of Brynhyfryd. Benefiting from being within close proximity to local amenities and approx. 4 miles from Swansea City Centre. The accommodation, benefiting from updating comprises:- Entrance hallway, three reception rooms, kitchen and shower room to the ground floor. To the first floor there are three bedrooms. Externally there is an enclosed rear garden.",
                    ImageName = "alap4.jpg",
                    IsFurnished = false,
                    Price = 15000
                },
                new Property()
                {
                    Id = 5,
                    NumberOfParkingSpaces = 0,
                    NumberOfBathrooms = 1,
                    NumberOfBedrooms = 3,
                    NumberOfGarages = 2,
                    Area = 64,
                    AgentId = "admin",
                    HouseType = HouseType.Colonial,
                    Description = "emi detached property comprising entrance hallway, lounge and fitted kitchen to the ground floor. To the first floor there are two bedrooms and a bathroom. Externally there is generously sized garden wrapping around the side and rear. Benefits include uPVC double glazing throughout and newly fitted Hive gas central heating with newly fitted radiators. The property is situated close to local amenities and transport links with easy access to the M4. Viewing is highly recommended to appreciate this well presented property. ",
                    ImageName = "alap5.jpg",
                    IsFurnished = false,
                    Price = 35000
                },
                new Property()
                {
                    Id = 6,
                    NumberOfParkingSpaces = 1,
                    NumberOfBathrooms = 3,
                    NumberOfBedrooms = 6,
                    NumberOfGarages = 2,
                    Area = 275,
                    AgentId = "admin",
                    HouseType = HouseType.Craftsman,
                    Description = "Light, airy and very nicely presented modern semi detached property situated in a quiet family friendly cul de sac location of Cockett. This well proportioned family home comprises: lounge/ dining room, fitted kitchen and cloakroom to ground floor with three bedrooms and family bathroom to first floor. Benefits: uPVC double glazing, gas central heating, built in storage facilities, two parking spaces and a pleasant enclosed rear garden offering decked seating area, astro turf grass, patio area and side access. Easy access to local amenities, Fforestfach Retail Park, the M4 and Swansea City Centre.",
                    ImageName = "alap6.jpg",
                    IsFurnished = true,
                    Price = 45000
                },
                new Property()
                {
                    Id = 7,
                    NumberOfParkingSpaces = 1,
                    NumberOfBathrooms = 1,
                    NumberOfBedrooms = 2,
                    NumberOfGarages = 2,
                    Area = 75,
                    AgentId = "admin",
                    HouseType = HouseType.Contemporary,
                    Description = "A lovely light and airy three bedroom flat within walking distance of local amenities and great school catchments. Briefly comprises: lounge/ dining room, fitted kitchen, three bedrooms, bathroom with separate w/c and communal entrance hallway. This property benefits: uPVC double glazing, gas central heating, small balcony and ample storage facilities. It would make a great home with easy access to Sketty Park, Tycoch Square, Sketty Cross, Singleton Hospital & Park, Killay including Olchfa & Parklands School catchments as well. ",
                    ImageName = "alap7.jpg",
                    IsFurnished = true,
                    Price = 32700
                },
                new Property()
                {
                    Id = 8,
                    NumberOfParkingSpaces = 1,
                    NumberOfBathrooms = 1,
                    NumberOfBedrooms = 2,
                    NumberOfGarages = 1,
                    Area = 110,
                    AgentId = "admin",
                    HouseType = HouseType.Ranch,
                    Description = "Extremely spacious and very well extended detached family home situated in a quiet cul-de-sac at the heart of Cockett. This beautiful property briefly comprises of sitting room, cloakroom/ utility, large open plan L shaped fitted kitchen, dining room and lounge great for modern family living to ground floor, four bedrooms, en-suite, family bathroom to the first floor plus an attic room. The many benefits to this property are uPVC double glazing, gas central heating, off road parking, single garage, enclosed and very well maintained tiered rear garden laid to lawn and patio area. ",
                    ImageName = "alap8.jpg",
                    IsFurnished = false,
                    Price = 22600
                }
            );

            builder.OwnsOne(e => e.Address).HasData(
                 new Address()
                 {
                     Id = 1,
                     PropertyId = 4,
                     City = "Honolulu",
                     Street = "Upalu St street 3",
                     ZipCode = 96705,
                     Country = "USA"
                 },
                new Address()
                {
                    Id = 2,
                    PropertyId = 3,
                    City = "Peking",
                    Street = "Chicaego street 45",
                    ZipCode = 11004,
                    Country = "China"
                },
                new Address()
                {
                    Id = 3,
                    PropertyId = 2,
                    City = "Madrid",
                    Street = "Bueno street 45",
                    ZipCode = 3424,
                    Country = "Spain"
                },
                new Address()
                {
                    Id = 4,
                    PropertyId = 1,
                    City = "New York",
                    Street = "Pearl street 72",
                    ZipCode = 5504,
                    Country = "USA"
                },
                new Address()
                {
                    Id = 5,
                    PropertyId = 5,
                    City = "Budapest",
                    Street = "Havanna St street 3",
                    ZipCode = 1181,
                    Country = "Hungary"
                },
                new Address()
                {
                    Id = 6,
                    PropertyId = 6,
                    City = "Tokyo",
                    Street = "Kaumu street 410",
                    ZipCode = 5620,
                    Country = "Japan"
                },
                new Address()
                {
                    Id = 7,
                    PropertyId = 7,
                    City = "Delhi",
                    Street = "Temple street 80",
                    ZipCode = 41023,
                    Country = "India"
                },
                new Address()
                {
                    Id = 8,
                    PropertyId = 8,
                    City = "Shanghai",
                    Street = "Ruraro street 91",
                    ZipCode = 632302,
                    Country = "China"
                }
            );
        }
    }
}
