using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStonks.SPA.Models
{
    public enum State
    {
        New, //Dodane, nieopłacone
        Active, //Dodane, opłacone
        Expired,
        Terminated
    }
    public enum Fuel
    {
        Diesel,
        Petrol,
        Electric,
        LPG
    }
    public enum Condition
    {
        Damaged,
        Undamaged
    }
    public enum DriveType
    {
        FWD,
        RWD,
        AWD
    }
    public enum TransmissionType
    {
        Sequence,
        Automatic,
        Manual
    }

    public class Advert
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime ExpiryDate { get; set; } //ustawiamy dopiero, gdy otrzymamy płatność
        public bool IsPromoted { get; set; }
        public string Description { get; set; }
        public string VIN { get; set; }
        public DateTime FirstRegistrationDate { get; set; }
        public string PlateNumber { get; set; }
        public State State { get; set; }
        public double Price { get; set; }
        public int Mileage { get; set; }
        public DateTime CarProductionDate { get; set; }
        public Fuel Fuel { get; set; }
        public Condition Condition { get; set; }
        public int Horsepower { get; set; }
        public int Displacement { get; set; }
        public string Location { get; set; }
        public bool HasBeenCrashed { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public DriveType DriveType { get; set; }
        public int VisitCount { get; set; }
        public int GenerationId { get; set; }
        public List<Photo> Photos { get; set; }
        public List<AdvertEquipment> AdvertEquipments { get; set; }
        public Generation Generation { get; set; }
        public User User { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
